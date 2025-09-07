using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SMS.Test
{
    [TestClass]
    public class DummyTest
    {
        [TestMethod]
        public void AlwaysPasses()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
