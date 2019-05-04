using CryptoMarketClient.Common;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Exchanges.Bittrex {
    public sealed class SignalWebSocket {
        public delegate void SignalCallback(SignalSocketCommand command, string marketName, string info);

        private HubConnection _hubConnection { get; }
        private IHubProxy _hubProxy { get; }

        public SignalCallback UpdateExchangeState { get; set; }
        public SignalCallback UpdateOrderState { get; set; }
        public SignalCallback UpdateBalanceState { get; set; }
        public SignalCallback UpdateSummaryState { get; set; }
        public DateTime LastActiveTime { get; set; }

        public SignalWebSocket(string connectionUrl) {
            // Set delegates

            // Create connection to c2 SignalR hub
            LastActiveTime = DateTime.Now;
            _hubConnection = new HubConnection(connectionUrl);
            _hubProxy = _hubConnection.CreateHubProxy("c2");
            Received += OnReceived;
        }

        private void OnReceived(string obj) {
            LastActiveTime = DateTime.Now;
        }

        IDisposable OnExchangeStateDelta { get; set; }
        IDisposable OnOrderStateDelta { get; set; }
        IDisposable OnBalanceStateDelta { get; set; }
        IDisposable OnSummaryDelta { get; set; }

        public void Shutdown() => _hubConnection.Stop();
        public void Connect() {
            if(UpdateExchangeState != null) {
                // Register callback for uE (exchange state delta) events
                OnExchangeStateDelta = _hubProxy.On("uE", exchangeStateDelta => {
                    LastActiveTime = DateTime.Now;
                    UpdateExchangeState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, exchangeStateDelta);
                });
            }

            if(UpdateOrderState != null) {
                // Register callback for uO (order status change) events
                OnOrderStateDelta = _hubProxy.On("uO", orderStateDelta => {
                    LastActiveTime = DateTime.Now;
                    UpdateOrderState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, orderStateDelta);
                });
            }

            if(UpdateBalanceState != null) {
                // Register callback for uB (balance status change) events
                OnBalanceStateDelta = _hubProxy.On("uB", balanceStateDelta => {
                    LastActiveTime = DateTime.Now;
                    UpdateBalanceState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, balanceStateDelta);
                });
            }

            if(UpdateSummaryState != null) {
                // Register callback for uB (balance status change) events
                OnSummaryDelta = _hubProxy.On("uS", summaryDelta => {
                    LastActiveTime = DateTime.Now;
                    UpdateSummaryState?.Invoke(SignalSocketCommand.IncrementalUpdate, null, summaryDelta);
                });
            }
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

        // marketName example: "BTC-LTC"
        public async Task<bool> SubscribeToExchangeDeltas(string marketName) { 
            return await _hubProxy.Invoke<bool>("SubscribeToExchangeDeltas", marketName);
        }
        public async Task<bool> SubscribeToMarketsState() {
            return await _hubProxy.Invoke<bool>("SubscribeToSummaryDeltas");
        }
        public void QueryExchangeState(string marketName) {
            _hubProxy.Invoke<string>("QueryExchangeState", marketName).ContinueWith(s => {
                LastActiveTime = DateTime.Now;
                if(UpdateExchangeState != null)
                    UpdateExchangeState(SignalSocketCommand.QueryExchangeState, marketName, s.Result);
                else
                    Telemetry.Default.TrackEvent(LogType.Warning, marketName, "signal socket", "UpdateExchangeState == null");
            });
        }

        // The return of GetAuthContext is a challenge string. Call CreateSignature(apiSecret, challenge)
        // for the response to the challenge, and pass it to Authenticate().
        public async Task<string> GetAuthContext(string apiKey) => await _hubProxy.Invoke<string>("GetAuthContext", apiKey);

        public async Task<bool> Authenticate(string apiKey, string signedChallenge) => await _hubProxy.Invoke<bool>("Authenticate", apiKey, signedChallenge);

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

        public static string CreateSignature(string apiSecret, string challenge) {
            // Get hash by using apiSecret as key, and challenge as data
            var hmacSha512 = new HMACSHA512(Encoding.ASCII.GetBytes(apiSecret));
            var hash = hmacSha512.ComputeHash(Encoding.ASCII.GetBytes(challenge));
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }

    public enum SignalSocketCommand {
        Undefined,
        IncrementalUpdate,
        QueryExchangeState
    }
}
