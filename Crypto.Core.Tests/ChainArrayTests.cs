using Crypto.Core.Helpers;
using FluentAssertions;
using Xunit;

namespace Crypto.Core.Tests {
    public class ChainArrayTests {
        [Fact]
        public void TestChainArray() {
            ChainArray<int> arr = new ChainArray<int>();
            for(int i = 0; i < 2000000; i++) {
                arr.Add(i);
            }

            arr.Count.Should().Be(2000000);
            for(int i = 0; i < 2000000; i++)
            {
                arr[i].Should().Be(i);
            }
        }
    }
}
