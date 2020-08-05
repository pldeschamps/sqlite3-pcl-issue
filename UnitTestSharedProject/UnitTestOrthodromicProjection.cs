using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections;
using System.Collections.Generic;
using System.Text;

using AlmicantaratSharedProject.MathTools;


namespace UnitTestSharedProject
{
    [TestClass]
    public class UnitTestOrthodromicProjection
    {
        [TestMethod]
        public void TestOrthodromicProjection()
        {
            LatitudeLongitude testLL = new LatitudeLongitude(45, 30);
            LatitudeLongitude testLLV = new LatitudeLongitude(testLL);
            Assert.AreEqual(testLL.Lon, testLLV.Lon, 0.0000001, "test constructeur LatitudeLongitude(Vector)");
            Assert.AreEqual(testLL.Lat, testLLV.Lat, 0.0000001, "test constructeur LatitudeLongitude(Vector)");

            LatitudeLongitude observer = new LatitudeLongitude(30, -50);

            AltitudeAzimuth aa1 = new AltitudeAzimuth(80.0, 0.0); //10° to the North
            LatitudeLongitude test1 = observer.OrthodromicProjection(aa1);
            Assert.AreEqual(40.0, test1.Lat, 0.00001, "Lat 30° + 10° = 40°");

            AltitudeAzimuth aa2 = new AltitudeAzimuth(80.0, 180); //10° to the South
            LatitudeLongitude test2 = observer.OrthodromicProjection(aa2);
            Assert.AreEqual(20.0, test2.Lat, 0.00001, "Lat 30° - 10° = 20°");

            LatitudeLongitude observer2 = new LatitudeLongitude(-30, -50);

            LatitudeLongitude test3 = observer2.OrthodromicProjection(aa2);
            Assert.AreEqual(-40.0, test3.Lat, 0.00001, "Lat -30° - 10° = -40°");
            Assert.AreEqual(observer2.Lon, test3.Lon, 0.00001, "10° to the South does not modify Longitude");

            LatitudeLongitude star = new LatitudeLongitude(30, -100);
            AltitudeAzimuth aa = new AltitudeAzimuth(observer, star);
            //Assert.AreEqual(90.0, aa.Az, 0.000001, "Orthodromic azimuth");
            LatitudeLongitude result = observer.OrthodromicProjection(aa);
            Assert.AreEqual(result.Lat, star.Lat, 0.0001, "Latitudes");
            Assert.AreEqual(result.Lon, star.Lon, 0.0001, "Longitudes");
        }

}
}
