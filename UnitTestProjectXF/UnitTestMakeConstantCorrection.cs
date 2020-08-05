using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlmicantaratXF.Model;
using System.Collections;
using System.Collections.Generic;

namespace UnitTestProjectXF
{
    [TestClass]
    public class UnitTestMakeConstantCorrection
    {
        [TestMethod]
        public void TestMethodSuccessful()
        {
            List<ConstantCorrection> ccs = new List<ConstantCorrection>();
            ccs.Add(new ConstantCorrection(0, 20));
            ccs.Add(new ConstantCorrection(10, 10));
            ccs.Add(new ConstantCorrection(20, 30));
            ccs.Add(new ConstantCorrection(30, 20));
            ccs.Add(new ConstantCorrection(40, 40));
            float cor1 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 0); //20
            Assert.AreEqual(20.0, cor1, 0.001, "20 sec");
            float cor2 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 5); //15
            Assert.AreEqual(15.0, cor2, 0.001, "15 sec");

            float cor3 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 20); //30
            Assert.AreEqual(30.0, cor3, 0.001, "20 sec");
            float cor4 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 26); //24
            Assert.AreEqual(24.0, cor4, 0.001, "20 sec");
            float cor5 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 34); //28
            Assert.AreEqual(28.0, cor5, 0.001, "20 sec");
            float cor6 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 45); //40
            Assert.AreEqual(40.0, cor6, 0.001, "20 sec");

        }
        [TestMethod]
        public void TestMethodNegativeAltitude()
        {
            List<ConstantCorrection> ccs = new List<ConstantCorrection>();
            ccs.Add(new ConstantCorrection(0, 20));
            ccs.Add(new ConstantCorrection(10, 10));
            ccs.Add(new ConstantCorrection(20, 30));
            ccs.Add(new ConstantCorrection(30, 20));
            ccs.Add(new ConstantCorrection(40, 40));
            float cor1 = 60 * MakeCorrection.MakeConstantCorrection(ccs, -20); //20
            Assert.AreEqual(0.0, cor1, 0.001, "0 sec");
        }
        [TestMethod]
        public void TestMethodEmptyList()
        {
            List<ConstantCorrection> ccs = new List<ConstantCorrection>();
            float cor1 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 20); //20
            Assert.AreEqual(0.0, cor1, 0.001, "0 sec");
        }
        [TestMethod]
        public void TestMethodOneCorrectionList()
        {
            List<ConstantCorrection> ccs = new List<ConstantCorrection>();
            ccs.Add(new ConstantCorrection(0, 20));
            float cor1 = 60 * MakeCorrection.MakeConstantCorrection(ccs, 15); //20
            Assert.AreEqual(0.0, cor1, 0.001, "0 sec");
        }
    }
}
