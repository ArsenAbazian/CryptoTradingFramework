using Crypto.Core.Helpers;
using Crypto.Core.Indicators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Tests {
    [TestFixture]
    public class IndicatorsTest {
        ResizeableArray<CandleStickData> GetCandleSticks(int count) {
            ResizeableArray<CandleStickData> list = new ResizeableArray<CandleStickData>();
            DateTime now = DateTime.UtcNow;
            DateTime start = now.AddMinutes(-count);
            Random r = new Random();
            for(int i = 0; i < count; i++) {
                list.Add(new CandleStickData() {
                    Time = start,
                    Open = 4000 + 100 * r.NextDouble(),
                    Close = 4000 + 100 * r.NextDouble(),
                    High = 4200 + 100 * r.NextDouble(),
                    Low = 3700 + 100 * r.NextDouble()
                });
                start = start.AddMinutes(1);
            }
            return list;
        }

        CandleStickData GetCandleStick(DateTime time) {
            Random r = new Random();
            return new CandleStickData() {
                Time = time,
                Open = 4000 + 100 * r.NextDouble(),
                Close = 4000 + 100 * r.NextDouble(),
                High = 4200 + 100 * r.NextDouble(),
                Low = 3700 + 100 * r.NextDouble()
            };
        }
        [Test]
        public void TestCalculate() {
            Ticker ticker = new Binance.BinanceTicker(Binance.BinanceExchange.Default);
            ticker.CandleStickData = GetCandleSticks(100);

            MacdIndicator macd = new MacdIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };
            StochasticIndicator stoch = new StochasticIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };

            macd.Calculate();
            stoch.Calculate();

            Assert.AreEqual(100, macd.Result.Count);
            Assert.AreEqual(100, macd.FastEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SlowEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SignalMaIndicator.Result.Count);

            Assert.AreEqual(100, stoch.MinIndicator.Result.Count);
            Assert.AreEqual(100, stoch.MaxIndicator.Result.Count);
            Assert.AreEqual(100, stoch.MaIndicator.Result.Count);
            Assert.AreEqual(100, stoch.Result.Count);

            macd.Calculate();
            stoch.Calculate();

            Assert.AreEqual(100, macd.Result.Count);
            Assert.AreEqual(100, macd.FastEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SlowEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SignalMaIndicator.Result.Count);

            Assert.AreEqual(100, stoch.MinIndicator.Result.Count);
            Assert.AreEqual(100, stoch.MaxIndicator.Result.Count);
            Assert.AreEqual(100, stoch.Result.Count);
        }

        [Test]
        public void TestItemAdded() {
            Ticker ticker = new Binance.BinanceTicker(Binance.BinanceExchange.Default);
            ticker.CandleStickData = GetCandleSticks(100);

            MacdIndicator macd = new MacdIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };
            StochasticIndicator stoch = new StochasticIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };

            macd.Calculate();
            stoch.Calculate();

            for(int i = 0; i < 10; i++) {
                CandleStickData date = GetCandleStick(DateTime.Now.AddMinutes(1));
                macd.OnAddValue();
                stoch.OnAddValue();
            }

            Assert.AreEqual(110, macd.Result.Count);
            Assert.AreEqual(110, macd.FastEmaIndicator.Result.Count);
            Assert.AreEqual(110, macd.SlowEmaIndicator.Result.Count);
            Assert.AreEqual(110, macd.SignalMaIndicator.Result.Count);

            Assert.AreEqual(110, stoch.MinIndicator.Result.Count);
            Assert.AreEqual(110, stoch.MaxIndicator.Result.Count);
            Assert.AreEqual(110, stoch.Result.Count);
        }

        [Test]
        public void TestItemAdded2() {
            Ticker ticker = new Binance.BinanceTicker(Binance.BinanceExchange.Default);
            ticker.CandleStickData = GetCandleSticks(100);

            MacdIndicator macd = new MacdIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };
            StochasticIndicator stoch = new StochasticIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };

            macd.Calculate();
            stoch.Calculate();

            for(int i = 0; i < 10; i++) {
                ticker.CandleStickData.Add(GetCandleStick(DateTime.Now.AddMinutes(1)));
            }

            Assert.AreEqual(110, macd.Result.Count);
            Assert.AreEqual(110, macd.FastEmaIndicator.Result.Count);
            Assert.AreEqual(110, macd.SlowEmaIndicator.Result.Count);
            Assert.AreEqual(110, macd.SignalMaIndicator.Result.Count);

            Assert.AreEqual(110, stoch.MinIndicator.Result.Count);
            Assert.AreEqual(110, stoch.MaxIndicator.Result.Count);
            Assert.AreEqual(110, stoch.Result.Count);
        }

        [Test]
        public void TestItemUpdated() {
            Ticker ticker = new Binance.BinanceTicker(Binance.BinanceExchange.Default);
            ticker.CandleStickData = GetCandleSticks(100);

            MacdIndicator macd = new MacdIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };
            StochasticIndicator stoch = new StochasticIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };

            macd.Calculate();
            stoch.Calculate();

            for(int i = 0; i < 10; i++) {
                CandleStickData date = GetCandleStick(DateTime.Now);
                macd.OnUpdateValue(99);
                stoch.OnUpdateValue(99);
            }

            Assert.AreEqual(100, macd.Result.Count);
            Assert.AreEqual(100, macd.FastEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SlowEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SignalMaIndicator.Result.Count);

            Assert.AreEqual(100, stoch.MinIndicator.Result.Count);
            Assert.AreEqual(100, stoch.MaxIndicator.Result.Count);
            Assert.AreEqual(100, stoch.Result.Count);
        }

        [Test]
        public void TestItemUpdated2() {
            Ticker ticker = new Binance.BinanceTicker(Binance.BinanceExchange.Default);
            ticker.CandleStickData = GetCandleSticks(100);

            MacdIndicator macd = new MacdIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };
            StochasticIndicator stoch = new StochasticIndicator() { Ticker = ticker, Source = IndicatorSource.Close, Length = 9 };

            macd.Calculate();
            stoch.Calculate();

            for(int i = 0; i < 10; i++) {
                CandleStickData date = GetCandleStick(DateTime.Now);
                ticker.CandleStickData[ticker.CandleStickData.Count - 1] = date;
            }

            Assert.AreEqual(100, macd.Result.Count);
            Assert.AreEqual(100, macd.FastEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SlowEmaIndicator.Result.Count);
            Assert.AreEqual(100, macd.SignalMaIndicator.Result.Count);

            Assert.AreEqual(100, stoch.MinIndicator.Result.Count);
            Assert.AreEqual(100, stoch.MaxIndicator.Result.Count);
            Assert.AreEqual(100, stoch.Result.Count);
        }
    }
}
