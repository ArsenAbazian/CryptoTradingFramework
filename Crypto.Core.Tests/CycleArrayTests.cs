using Crypto.Core.Helpers;
using FluentAssertions;
using Xunit;

namespace Crypto.Core.Tests {
    public class CycleArrayTests {
        [Fact]
        public void TestDefaults() {
            CycleArray<int> arr = new CycleArray<int>(32);
            arr.Count.Should().Be(0);
        }

        [Fact]
        public void Add1() {
            CycleArray<int> arr = new CycleArray<int>(32);
            arr.Add(1);

            arr.Count.Should().Be(1);
            arr[0].Should().Be(1);
        }

        [Fact]
        public void AddFirst1() {
            CycleArray<int> arr = new CycleArray<int>(32);
            arr.AddFirst(1);

            arr.Count.Should().Be(1);
            arr[0].Should().Be(1);
        }

        [Fact]
        public void TestAdd() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }

            arr.Count.Should().Be(32);
            for(int i = 0; i < arr.Count; i++)
                arr[i].Should().Be(i);
        }

        [Fact]
        public void TestAddFirst() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }

            arr.Count.Should().Be(32);
            for(int i = arr.Count - 1; i >= 0; i--)
                arr[i].Should().Be(i);
        }

        [Fact]
        public void TestAdd2() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }
            arr.Add(32);

            arr.Count.Should().Be(32);
            for(int i = 0; i < arr.Count; i++)
                arr[i].Should().Be(i + 1);
        }

        [Fact]
        public void TestAddFirst2() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }
            arr.AddFirst(32);

            arr.Count.Should().Be(32);
            arr[0].Should().Be(32);
            for(int i = 1; i < arr.Count; i++)
                arr[i].Should().Be(i - 1);
        }

        [Fact]
        public void TestAdd3() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(i);
            }
            
            for(int i = 32; i < 100; i++) {
                arr.Add(i);
                arr.Count.Should().Be(32);

                for(int j = 0; j < arr.Count; j++)
                    arr[j].Should().Be(i - 31 + j);
            }
        }

        [Fact]
        public void TestAddFirst3() {
            CycleArray<int> arr = new CycleArray<int>(32);
            
            for(int i = 0; i < 32; i++) {
                arr.Add(31 - i);
            }
            
            for(int i = 32; i < 100; i++) {
                arr.AddFirst(i);
                arr.Count.Should().Be(32);

                for(int j = 0; j < arr.Count; j++)
                    arr[j].Should().Be(i - j);
            }
        }
    }
}
