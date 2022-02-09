using Crypto.Core.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Tests {
    [TestFixture]
    public class ChainArrayTests {
        [Test]
        public void TestChainArray() {
            ChainArray<int> arr = new ChainArray<int>();
            for(int i = 0; i < 2000000; i++) {
                arr.Add(i);
            }
            Assert.AreEqual(2000000, arr.Count);
            for(int i = 0; i < 2000000; i++) {
                Assert.AreEqual(i, arr[i]);
            }
        }
    }
}
