using Crypto.Core.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto.Core.Tests {
    [TestFixture]
    public class CycleArrayTests {
        [Test]
        public void TestDefaults() {
            CycleArray<int> arr = new CycleArray<int>(32);
            Assert.AreEqual(0, arr.Count);
        }

        [Test]
        public void Add1() {
            CycleArray<int> arr = new CycleArray<int>(32);
            arr.Add(1);

            Assert.AreEqual(1, arr.Count);
            Assert.AreEqual(1, arr[0]);
        }

        [Test]
        public void AddFirst1() {
            CycleArray<int> arr = new CycleArray<int>(32);
            arr.AddFirst(1);

            Assert.AreEqual(1, arr.Count);
            Assert.AreEqual(1, arr[0]);
        }

        [Test]
        public void TestAdd() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }

            Assert.AreEqual(32, arr.Count);
            for(int i = 0; i < arr.Count; i++)
                Assert.AreEqual(i, arr[i]);
        }

        [Test]
        public void TestAddFirst() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }

            Assert.AreEqual(32, arr.Count);
            for(int i = arr.Count - 1; i >= 0; i--)
                Assert.AreEqual(i, arr[i]);
        }

        [Test]
        public void TestAdd2() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }
            arr.Add(32);

            Assert.AreEqual(32, arr.Count);
            for(int i = 0; i < arr.Count; i++)
                Assert.AreEqual(i + 1, arr[i]);
        }

        [Test]
        public void TestAddFirst2() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }
            arr.AddFirst(32);

            Assert.AreEqual(32, arr.Count);
            Assert.AreEqual(32, arr[0]);
            for(int i = 1; i < arr.Count; i++)
                Assert.AreEqual(i - 1, arr[i]);
        }

        [Test]
        public void TestAdd3() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }
            
            for(int i = 32; i < 100; i++) {
                arr.Add(i);
                Assert.AreEqual(32, arr.Count);

                for(int j = 0; j < arr.Count; j++)
                    Assert.AreEqual(i - 31 + j, arr[j]);
            }
        }

        [Test]
        public void TestAddFirst3() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(31 - i);
            }
            
            for(int i = 32; i < 100; i++) {
                arr.AddFirst(i);
                Assert.AreEqual(32, arr.Count);

                for(int j = 0; j < arr.Count; j++)
                    Assert.AreEqual(i - j, arr[j]);
            }
        }
    }
}
