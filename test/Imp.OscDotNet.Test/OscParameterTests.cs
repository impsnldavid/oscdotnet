using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imp.OscDotNet.Test
{
    [TestClass]
    public class OscParameterTests
    {
        [TestMethod]
        public void AreParametersEqualToDataType()
        {
            (new OscParameterInt(0) == 0).Should().BeTrue();
            (new OscParameterFloat(0f) == 0f).Should().BeTrue();
            (new OscParameterString("foo") == "foo").Should().BeTrue();
            (new OscParameterBlob(new byte[] { 0, 1, 2}) == new byte[] { 0, 1, 2}).Should().BeTrue();
        }
    }
}
