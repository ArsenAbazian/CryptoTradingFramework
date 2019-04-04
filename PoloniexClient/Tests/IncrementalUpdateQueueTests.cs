using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Tests {
    [TestFixture]
    public class IncrementalUpdateQueueTests {
        [Test]
        public void TestDefaults() {
            IncrementalUpdateQueue q = new IncrementalUpdateQueue(new TestIncrementalUpdateDataProvider());
            Assert.AreEqual(0, q.Count);
            Assert.AreEqual(0, q.SeqNumber);
            for(int i = 0; i < q.Capacity; i++) {
                Assert.AreEqual(true, q[i].Empty);
                Assert.AreEqual(false, q[i].Applied);
            }
            Assert.AreEqual(true, q.CanApply);
        }
        [Test]
        public void TestPush() {
            IncrementalUpdateQueue q = new IncrementalUpdateQueue(new TestIncrementalUpdateDataProvider());
            Assert.AreEqual(false, q.Push(q.Capacity, null, null));
            Assert.AreEqual(true, q.Push(q.SeqNumber - 1, null, null));

            List<string[]>[] items = new List<string[]>[q.Capacity];
            for(int i = 0; i < q.Capacity; i++) {
                items[i] = new List<string[]>();
                Assert.AreEqual(true, q.Push(i, null, items[i]));
                Assert.AreEqual(items[i], q[i].Updates);
                Assert.AreEqual(false, q[i].Empty);
                Assert.AreEqual(false, q[i].Applied);
                Assert.AreEqual(i + 1, q.Count);
                Assert.AreEqual(true, q.CanApply);
            }
        }
        [Test]
        public void TestPush2() {
            IncrementalUpdateQueue q = new IncrementalUpdateQueue(new TestIncrementalUpdateDataProvider());
            List<string[]>[] items = new List<string[]>[q.Capacity];
            for(int i = 0; i < q.Capacity; i++) {
                if(i % 2 == 0)
                    continue;
                items[i] = new List<string[]>();
                Assert.AreEqual(true, q.Push(i, null, items[i]));
                Assert.AreEqual(items[i], q[i].Updates);
                Assert.AreEqual(false, q[i].Empty);
                Assert.AreEqual(false, q[i].Applied);
                Assert.AreEqual(i + 1, q.Count);
                Assert.AreEqual(false, q.CanApply);
            }
        }
        [Test]
        public void TestCanApply() {
            IncrementalUpdateQueue q = new IncrementalUpdateQueue(new TestIncrementalUpdateDataProvider());

            List<string[]>[] items = new List<string[]>[q.Capacity];
            for(int i = 0; i < q.Capacity; i++) {
                items[i] = new List<string[]>();
                Assert.AreEqual(true, q.Push(i, null, items[i]));
            }
            q[100].Clear();
            Assert.AreEqual(false, q.CanApply);
            q[100].Fill(22, null, new List<string[]>());
            Assert.AreEqual(true, q.CanApply);
        }
        [Test]
        public void TestClearUpdate() {
            IncrementalUpdateQueue q = new IncrementalUpdateQueue(new TestIncrementalUpdateDataProvider());
            q.Push(0, null, new List<string[]>());
            q[0].Clear();
            Assert.AreEqual(true, q[0].Empty);
            Assert.AreEqual(false, q[0].Applied);
        }
        [Test]
        public void TestUpdateOne() {
            IncrementalUpdateQueue q = new IncrementalUpdateQueue(new TestIncrementalUpdateDataProvider());
            q.Push(0, null, new List<string[]>());
            Assert.AreEqual(true, q.CanApply);
            q.Apply();
            Assert.AreEqual(0, q.Count);
            Assert.AreEqual(true, q.CanApply);
            Assert.AreEqual(1, q.SeqNumber);
        }
    }

    public class TestIncrementalUpdateDataProvider : IIncrementalUpdateDataProvider {
        public void Update(Ticker ticker, IncrementalUpdateInfo info) {
            throw new NotImplementedException();
        }
        public void ApplySnapshot(JObject jObject, Ticker ticker) {
            throw new NotImplementedException();
        }
    }
}
