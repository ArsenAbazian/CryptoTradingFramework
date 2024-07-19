using Crypto.Core.Helpers;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Crypto.Core.Tests {
    public class JsonDeserializerTests {
        [Fact]
        public void TestSpaces() {
            string msg = "[ {\n  \"date\" : \"1662786000000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662789600000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662793200000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662796800000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662800400000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662804000000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662807600000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662811200000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662814800000\",\n  \"high\" : \"0.0000005242\",\n  \"low\" : \"0.0000005051\",\n  \"open\" : \"0.0000005051\",\n  \"close\" : \"0.0000005242\",\n  \"volume\" : \"1784\",\n  \"quoteVolume\" : \"0.000918175200000000\",\n  \"weightedAverage\" : \"0.000000514751728057\"\n}, {\n  \"date\" : \"1662818400000\",\n  \"high\" : \"0.0000005463\",\n  \"low\" : \"0.0000005292\",\n  \"open\" : \"0.0000005292\",\n  \"close\" : \"0.0000005463\",\n  \"volume\" : \"1612\",\n  \"quoteVolume\" : \"0.000867310400000000\",\n  \"weightedAverage\" : \"0.000000538105198852\"\n} ]";
            var root = JsonHelper.Default.Deserialize(msg);

            root.Items[0].Properties[0].Name.Should().Be("date");
            root.Items[0].Properties[0].Value.Should().Be("1662786000000");

            root.Items[0].Properties[1].Name.Should().Be("high");
            root.Items[0].Properties[1].Value.Should().Be("0.0000005036");
            
            root.Items[0].Properties[2].Name.Should().Be("low");
            root.Items[0].Properties[2].Value.Should().Be("0.0000005036");
            
            root.Items[0].Properties[3].Name.Should().Be("open");
            root.Items[0].Properties[3].Value.Should().Be("0.0000005036");
            
            root.Items[0].Properties[4].Name.Should().Be("close");
            root.Items[0].Properties[4].Value.Should().Be("0.0000005036");

            root.Items[0].Properties[7].Name.Should().Be("weightedAverage");
            root.Items[0].Properties[7].Value.Should().Be("0.000000502644558595");
        }

        [Fact]
        public void TestSpacesBytes() {
            string msg = "[ {\n  \"date\" : \"1662786000000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662789600000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662793200000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662796800000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662800400000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662804000000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662807600000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662811200000\",\n  \"high\" : \"0.0000005036\",\n  \"low\" : \"0.0000005036\",\n  \"open\" : \"0.0000005036\",\n  \"close\" : \"0.0000005036\",\n  \"volume\" : \"0\",\n  \"quoteVolume\" : \"0\",\n  \"weightedAverage\" : \"0.000000502644558595\"\n}, {\n  \"date\" : \"1662814800000\",\n  \"high\" : \"0.0000005242\",\n  \"low\" : \"0.0000005051\",\n  \"open\" : \"0.0000005051\",\n  \"close\" : \"0.0000005242\",\n  \"volume\" : \"1784\",\n  \"quoteVolume\" : \"0.000918175200000000\",\n  \"weightedAverage\" : \"0.000000514751728057\"\n}, {\n  \"date\" : \"1662818400000\",\n  \"high\" : \"0.0000005463\",\n  \"low\" : \"0.0000005292\",\n  \"open\" : \"0.0000005292\",\n  \"close\" : \"0.0000005463\",\n  \"volume\" : \"1612\",\n  \"quoteVolume\" : \"0.000867310400000000\",\n  \"weightedAverage\" : \"0.000000538105198852\"\n} ]";
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            var root = JsonHelper.Default.Deserialize(bytes);

            root.Items[0].Properties[0].Name.Should().Be("date");
            root.Items[0].Properties[0].Value.Should().Be("1662786000000");
            
            root.Items[0].Properties[1].Name.Should().Be("high");
            root.Items[0].Properties[1].Value.Should().Be("0.0000005036");
            
            root.Items[0].Properties[2].Name.Should().Be("low");
            root.Items[0].Properties[2].Value.Should().Be("0.0000005036");
            
            root.Items[0].Properties[3].Name.Should().Be("open");
            root.Items[0].Properties[3].Value.Should().Be("0.0000005036");
            
            root.Items[0].Properties[4].Name.Should().Be("close");
            root.Items[0].Properties[4].Value.Should().Be("0.0000005036");
            
            root.Items[0].Properties[7].Name.Should().Be("weightedAverage");
            root.Items[0].Properties[7].Value.Should().Be("0.000000502644558595");
        }
    }
}
