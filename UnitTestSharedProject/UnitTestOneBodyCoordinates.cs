using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlmicantaratSharedProject.Ephemeris;
using AlmicantaratSharedProject.MathTools;

using System.Collections;
using System.Collections.Generic;

namespace UnitTestSharedProject
{
    [TestClass]
    public class UnitTestOneBodyCoordinates
    {
        [TestMethod]
        public void TestMethodSuccessful()
        {  
            OneBodyCoordinates body = new OneBodyCoordinates(new System.DateTime(2020,5,8,14,0,0),Planets.Mars);
            Assert.AreEqual(7.495111, body.ParallaxDegrees * 3600.0, 0.000001, "Parallaxe Mars");
            Assert.AreEqual(-14.2519472, body.BodyCoordinates.DeclinationGHA.Lat, 0.00001, "Déclinaison Mars");
        }
    }
    [TestClass]
    public class UnitTestStarsCoordinates
    {
        [TestMethod]
        public void TestMethodSuccessful()
        {
            SolarSystem solarSystem = new SolarSystem(new System.DateTime(2020, 5, 8, 14, 0, 0));
            LatitudeLongitude declinationGHA = BodyCoordinates.CurrentCoordinates(new LatitudeLongitude(66.101462f, 88.567594f), solarSystem.CurrentData);
            Assert.AreEqual(89.263105126223, declinationGHA.Lat, 0.000001, "déclinaison Polaris");
            Assert.AreEqual(39.312794344624, declinationGHA.Lon, 0.00001, "GHA Polaris");
            LatitudeLongitude declinationRA = BodyCoordinates.CurrentDeclinationGHA(new LatitudeLongitude(66.101462f, 88.567594f), solarSystem.CurrentData);
            System.Diagnostics.Debug.WriteLine("déclinaison Polaris = " + declinationRA.Lat.ToString("F6") + " RA = "+declinationRA.Lon.ToString("F6"));
            //déclinaison Polaris = 89,2642550 RA Polaris = 321,7556219
            declinationRA = BodyCoordinates.CurrentDeclinationGHA(new LatitudeLongitude(72.987596f, 133.319476f), solarSystem.CurrentData);//KOCHAB
            System.Diagnostics.Debug.WriteLine("déclinaison Kochab = " + declinationRA.Lat.ToString("F6") + " RA = " + declinationRA.Lon.ToString("F6"));
            //déclinaison Kochab = 74,155377 RA = 137,050408
            declinationRA = BodyCoordinates.CurrentDeclinationGHA(new LatitudeLongitude(29.303462f, 301.776416f), solarSystem.CurrentData);//Altair
            System.Diagnostics.Debug.WriteLine("déclinaison Altair = " + declinationRA.Lat.ToString("F6") + " RA = " + declinationRA.Lon.ToString("F6"));
            //déclinaison Altair = 8,868497 RA = 62,030192
            declinationRA = BodyCoordinates.CurrentDeclinationGHA(new LatitudeLongitude(-2.054489f, 203.841358f), solarSystem.CurrentData);//Spica
            System.Diagnostics.Debug.WriteLine("déclinaison Spica = " + declinationRA.Lat.ToString("F6") + " RA = " + declinationRA.Lon.ToString("F6"));
            //déclinaison Spica = -11,161515 RA = 158,427728
            declinationRA = BodyCoordinates.CurrentDeclinationGHA(new LatitudeLongitude(68.913693f, 12.778110f), solarSystem.CurrentData);//Alderamin
            System.Diagnostics.Debug.WriteLine("déclinaison Alderamin = " + declinationRA.Lat.ToString("F6") + " RA = " + declinationRA.Lon.ToString("F6"));
            //déclinaison Alderamin = 62,585805 RA = 40,081317
            //+aberration, déclinaison Alderamin = 62,580290 RA = 40,081288
            LatitudeLongitude declinationGhaDubhe = BodyCoordinates.CurrentDeclinationGHA(new LatitudeLongitude(49.680301f, 135.197385f), solarSystem.CurrentData);//Dubhe
            System.Diagnostics.Debug.WriteLine("déclinaison Dubhe = " + declinationGhaDubhe.Lat.ToString("F6") + " GHA = " + declinationGhaDubhe.Lon.ToString("F6"));
            AltitudeAzimuth dubhe = new AltitudeAzimuth(new LatitudeLongitude(60,30), declinationGhaDubhe);
            System.Diagnostics.Debug.WriteLine("alt Dubhe = " + dubhe.Al.ToString("F6") + " Z = " + dubhe.Az.ToString("F6"));
            //alt Dubhe = 40,385227 Z = 32,897923
            AltitudeAzimuth dubhe2 = new AltitudeAzimuth(new LatitudeLongitude(-10, -45.5), declinationGhaDubhe);
            System.Diagnostics.Debug.WriteLine("-10N-45.5E, alt Dubhe = " + dubhe2.Al.ToString("F6") + " Z = " + dubhe2.Az.ToString("F6"));
            LatitudeLongitude declinationGhaPolaris = BodyCoordinates.CurrentCoordinates(new LatitudeLongitude(66.101462f, 88.567594f), solarSystem.CurrentData);
            AltitudeAzimuth polaris = new AltitudeAzimuth(new LatitudeLongitude(-10, -45.5), declinationGhaPolaris);
            System.Diagnostics.Debug.WriteLine("-10N-45.5E, alt Polaris = " + polaris.Al.ToString("F6") + " Z = " + polaris.Az.ToString("F6"));

        }
    }
}
