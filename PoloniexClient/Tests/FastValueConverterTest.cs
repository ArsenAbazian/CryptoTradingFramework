using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoMarketClient.Tests {
    [TestFixture]
    public class FastValueConverterTest {
        [Test]
        public void Test1() {
            double val = FastValueConverter.Convert("4085187338");
            Assert.AreEqual(Convert.ToDouble("4085187338"), val);
        }
    }
}
