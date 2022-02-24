using Crypto.Core.Common;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Exchanges.Bittrex {
    public sealed class SignalWebSocket {
        public delegate void SignalCallback(SignalSocketCommand command, string marketName, string info);

        private string _url;
        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;

        //public SignalCallback UpdateExchangeState { get; set; }
        //public SignalCallback UpdateOrderState { get; set; }
        //public SignalCallback UpdateBalanceState { get; set; }
        //public SignalCallback UpdateSummaryState { get; set; }
        
        public DateTime LastActiveTime { get; set; }

        public SignalWebSocket(string connectionUrl) {
            LastActiveTime = DateTime.Now;
            this._hubConnection = new HubConnection(connectionUrl);
            this._hubProxy = _hubConnection.CreateHubProxy("c3");
            this._url = connectionUrl;
            
            Received += OnReceived;
        }

        private void OnReceived(string obj) {
            LastActiveTime = DateTime.Now;
        }

        IDisposable OnExchangeStateDelta { get; set; }
        IDisposable OnOrderStateDelta { get; set; }
        IDisposable OnBalanceStateDelta { get; set; }
        IDisposable OnSummaryDelta { get; set; }

        public IDisposable AddMessageHandler<Tmessage>(string messageName, Action<string, Tmessage> handler) {
            if(this.handlers.ContainsKey(messageName))
                return this.handlers[messageName];
            IDisposable res = _hubProxy.On(messageName, message => {
                handler(messageName, message);
            });
            this.handlers.Add(messageName, res);
            return res;
        }

        Dictionary<string, IDisposable> handlers = new Dictionary<string, IDisposable>();

        public async void Shutdown() {
            await Task.Run(() => {
                _hubConnection.Stop();
            });
        }
        public void Connect() {
            //if(UpdateExchangeState != null) {
            //    OnExchangeStateDelta = _hubProxy.On("uE", exchangeStateDelta => {
            //        LastActiveTime = DateTime.Now;
            //        UpdateExchangeState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, exchangeStateDelta);
            //    });
            //}

            //if(UpdateOrderState != null) {
            //    OnOrderStateDelta = _hubProxy.On("uO", orderStateDelta => {
            //        LastActiveTime = DateTime.Now;
            //        UpdateOrderState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, orderStateDelta);
            //    });
            //}

            //if(UpdateBalanceState != null) {
            //    OnBalanceStateDelta = _hubProxy.On("uB", balanceStateDelta => {
            //        LastActiveTime = DateTime.Now;
            //        UpdateBalanceState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, balanceStateDelta);
            //    });
            //}

            //if(UpdateSummaryState != null) {
            //    OnSummaryDelta = _hubProxy.On("uS", summaryDelta => {
            //        LastActiveTime = DateTime.Now;
            //        UpdateSummaryState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, summaryDelta);
            //    });
            //}
            _hubConnection.Start().Wait();
        }

        public event Action<StateChange> StateChanged {
            add { this._hubConnection.StateChanged += value; }
            remove { this._hubConnection.StateChanged -= value; }
        }
        public event Action<Exception> Error {
            add { this._hubConnection.Error += value; }
            remove { this._hubConnection.Error -= value; }
        }
        public event Action Closed {
            add { this._hubConnection.Closed += value; }
            remove { this._hubConnection.Closed -= value; }
        }
        public event Action<string> Received {
            add { this._hubConnection.Received += value; }
            remove { this._hubConnection.Received -= value; }
        }

        public void SetHeartbeatHandler(Action handler) {
            if(this.handlers.ContainsKey("heartbeat"))
                return;
            this.handlers.Add("heartbeat", _hubProxy.On("heartbeat", handler));
        }

        public void SetAuthExpiringHandler(Action handler) {
            if(this.handlers.ContainsKey("authenticationExpiring"))
                return;
            this.handlers.Add("authenticationExpiring", _hubProxy.On("authenticationExpiring", handler));
        }

        private static string CreateSignature(string apiSecret, string data) {
            var hmacSha512 = new HMACSHA512(Encoding.ASCII.GetBytes(apiSecret));
            var hash = hmacSha512.ComputeHash(Encoding.ASCII.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }

        public async Task<List<SocketResponse>> Subscribe(string[] channels) {
            return await _hubProxy.Invoke<List<SocketResponse>>("Subscribe", (object)channels);
        }

        public async Task<List<SocketResponse>> Unsubscribe(string[] channels) {
            return await _hubProxy.Invoke<List<SocketResponse>>("Unsubscribe", (object)channels);
        }

        public async Task<SocketResponse> Authenticate(string apiKey, string apiKeySecret) {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var randomContent = $"{ Guid.NewGuid() }";
            var content = string.Join("", timestamp, randomContent);
            var signedContent = CreateSignature(apiKeySecret, content);
            var result = await _hubProxy.Invoke<SocketResponse>(
                "Authenticate",
                apiKey,
                timestamp,
                randomContent,
                signedContent);
            return result;
        }

        // marketName example: "BTC-LTC"
        public async Task<List<SocketResponse>> SubscribeToExchangeDeltas(string marketName) { 
            return await _hubProxy.Invoke<List<SocketResponse>>("SubscribeToExchangeDeltas", marketName);
        }
        public async Task<List<SocketResponse>> SubscribeToMarketsState() {
            return await _hubProxy.Invoke<List<SocketResponse>>("market_summaries");
        }
        //public void QueryExchangeState(string marketName) {
        //    _hubProxy.Invoke<string>("QueryExchangeState", marketName).ContinueWith(s => {
        //        LastActiveTime = DateTime.Now;
        //        if(UpdateExchangeState != null)
        //            UpdateExchangeState(SignalSocketCommand.QueryExchangeState, marketName, s.Result);
        //        else
        //            Telemetry.Default.TrackEvent(LogType.Warning, marketName, "signal socket", "UpdateExchangeState == null");
        //    });
        //}

        // Decode converts Bittrex CoreHub2 socket wire protocol data into JSON.
        // Data goes from base64 encoded to gzip (byte[]) to minifed JSON.
        public static string Decode(string wireData) {
            // Step 1: Base64 decode the wire data into a gzip blob
            byte[] gzipData = Convert.FromBase64String(wireData);

            // Step 2: Decompress gzip blob into minified JSON
            using(var decompressedStream = new MemoryStream())
            using(var compressedStream = new MemoryStream(gzipData))
            using(var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress)) {
                deflateStream.CopyTo(decompressedStream);
                decompressedStream.Position = 0;

                using(var streamReader = new StreamReader(decompressedStream)) {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static byte[] DecodeBytes(string wireData) {
            // Step 1: Base64 decode the wire data into a gzip blob
            byte[] gzipData = Convert.FromBase64String(wireData);

            // Step 2: Decompress gzip blob into minified JSON
            using(var decompressedStream = new MemoryStream())
            using(var compressedStream = new MemoryStream(gzipData))
            using(var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress)) {
                deflateStream.CopyTo(decompressedStream);
                decompressedStream.Position = 0;
                return decompressedStream.GetBuffer();
            }
        }
    }

    public enum SignalSocketCommand {
        Undefined,
        IncrementalUpdate,
        QueryExchangeState
    }

    //public static class DataConverter {
    //    private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings {
    //        ContractResolver = new CamelCasePropertyNamesContractResolver(),
    //        DateFormatHandling = DateFormatHandling.IsoDateFormat,
    //        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
    //        FloatParseHandling = FloatParseHandling.Decimal,
    //        MissingMemberHandling = MissingMemberHandling.Ignore,
    //        NullValueHandling = NullValueHandling.Ignore,
    //        Converters = new List<JsonConverter>
    //        {
    //            new StringEnumConverter(),
    //        }
    //    };

    //    public static T Decode<T>(string wireData) {
    //        // Step 1: Base64 decode the wire data into a gzip blob
    //        byte[] gzipData = Convert.FromBase64String(wireData);

    //        // Step 2: Decompress gzip blob into JSON
    //        string json = null;

    //        using(var decompressedStream = new MemoryStream())
    //        using(var compressedStream = new MemoryStream(gzipData))
    //        using(var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress)) {
    //            deflateStream.CopyTo(decompressedStream);
    //            decompressedStream.Position = 0;
    //            using(var streamReader = new StreamReader(decompressedStream)) {
    //                json = streamReader.ReadToEnd();
    //            }
    //        }

    //        // Step 3: Deserialize the JSON string into a strongly-typed object
    //        return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
    //    }
    //}

    public class SocketResponse {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
    }
}
