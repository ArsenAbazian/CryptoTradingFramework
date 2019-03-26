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
    public sealed class BittrexWebSocket {
        public delegate void BittrexCallback(BittrexSocketCommand command, string marketName, string info);

        private HubConnection _hubConnection { get; }
        private IHubProxy _hubProxy { get; }

        public BittrexCallback UpdateExchangeState { get; set; }
        public BittrexCallback UpdateOrderState { get; set; }
        public BittrexCallback UpdateBalanceState { get; set; }
        public BittrexCallback UpdateSummaryState { get; set; }

        public BittrexWebSocket(string connectionUrl) {
            // Set delegates

            // Create connection to c2 SignalR hub
            _hubConnection = new HubConnection(connectionUrl);
            _hubProxy = _hubConnection.CreateHubProxy("c2");
        }

        public void Shutdown() => _hubConnection.Stop();
        public void Connect() {
            if(UpdateExchangeState != null) {
                // Register callback for uE (exchange state delta) events
                _hubProxy.On("uE", exchangeStateDelta => UpdateExchangeState?.Invoke(BittrexSocketCommand.IncrementalUpdate, null, exchangeStateDelta));
            }

            if(UpdateOrderState != null) {
                // Register callback for uO (order status change) events
                _hubProxy.On("uO", orderStateDelta => UpdateOrderState?.Invoke(BittrexSocketCommand.IncrementalUpdate, null, orderStateDelta));
            }

            if(UpdateBalanceState != null) {
                // Register callback for uB (balance status change) events
                _hubProxy.On("uB", balanceStateDelta => UpdateBalanceState?.Invoke(BittrexSocketCommand.IncrementalUpdate, null, balanceStateDelta));
            }

            if(UpdateSummaryState != null) {
                // Register callback for uB (balance status change) events
                _hubProxy.On("uS", summaryDelta => UpdateSummaryState?.Invoke(BittrexSocketCommand.IncrementalUpdate, null, summaryDelta));
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
        public async Task<bool> SubscribeToExchangeDeltas(string marketName) => await _hubProxy.Invoke<bool>("SubscribeToExchangeDeltas", marketName);
        public void QueryExchangeState(string marketName) {
            _hubProxy.Invoke<string>("QueryExchangeState", marketName).ContinueWith(s => UpdateExchangeState(BittrexSocketCommand.QueryExchangeState, marketName, s.Result));
        }

        public async Task<bool> SubscribeToMarketsState() => await _hubProxy.Invoke<bool>("SubscribeToSummaryDeltas");

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

    public enum BittrexSocketCommand {
        Undefined,
        IncrementalUpdate,
        QueryExchangeState
    }
}
