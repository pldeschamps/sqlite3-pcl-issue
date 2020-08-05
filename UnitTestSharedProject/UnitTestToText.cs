using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlmicantaratSharedProject.TextTools;


namespace UnitTestSharedProject
{
    [TestClass]
    public class UnitTestToText
    {
        [TestMethod]
        public void TestMethodAltitude()
        {
            double altitude = 90.9;
            string result = ToText.ConvertDouble(altitude, Categories.altitude, Formats.DegreesMinutesDecimal, 1);
            Assert.AreEqual("90°54.0'", result);
            double heading = 0.0;
            string resultHeading = ToText.ConvertDouble(heading, Categories.azimuth);
            Assert.AreEqual("000.0", resultHeading);
            heading = 345.65;
            resultHeading = ToText.ConvertDouble(heading, Categories.azimuth);
            Assert.AreEqual("345.6", resultHeading);

        }
    }
}
