using System;
using Xunit;

namespace Elsys.Decoder.Test
{
    public class DecoderTest
    {
        [Fact]
        public void TestDecoder()
        {
            var input = "0100e202290400270506060308070d62";

            var o = Decoder.Decode(input);
            Assert.Equal(22.6, o.Temperature);
            Assert.Equal(41, o.Humidity);
            Assert.Equal(39, o.Light);
            Assert.Equal(6, o.Motion);
            Assert.Equal(776, o.Co2);
            Assert.Equal(3426, o.Vdd);
        }
    }
}
