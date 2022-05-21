using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RNGManager.Test
{
    /// <summary>
    /// Testing random generation is pretty unrealiable. If any test from the Random generator category fails, it may be still working okay.
    /// </summary>
    [TestClass]
    public class RNGManagerTest
    {
        private string instanceTitle = "Test";
        private int instanceSeed = 69;
        private int testLoops = 10000;
        private float rngBuffer = 0.02f;

        private enum TestEnum
        {
            Value1,
            Value2,
            Value3 = 1,
            Value4
        }

        #region Manager

        [TestMethod]
        [TestCategory("Manager")]
        public void GetManagerTest()
        {
            var manager = RNGManager.Manager;
            Assert.IsNotNull(manager);
        }

        [TestMethod]
        [TestCategory("Manager")]
        public void GetInstanceByTitleTest()
        {
            var instance = RNGManager.Manager[instanceTitle];

            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Title, instanceTitle);
        }

        [TestMethod]
        [TestCategory("Manager")]
        public void GetInstanceBySeedTest()
        {
            var instance = RNGManager.Manager[instanceSeed];

            Assert.IsNotNull(instance);
            Assert.AreEqual(instance.Seed, instanceSeed);
        }

        [TestMethod]
        [TestCategory("Manager")]
        public void AddInstanceTest()
        {
            var instance = new RNGInstance(instanceSeed, instanceTitle);
            RNGManager.Manager.AddInstance(instance);

            Assert.IsNotNull(RNGManager.Manager[instanceSeed]);
            Assert.IsNotNull(RNGManager.Manager[instanceTitle]);
        }

        #endregion

        #region Random generator

        #region NextBool

        [TestMethod]
        [TestCategory("Random generator")]
        [TestCategory("NextBool")]
        public void NextBoolTest()
        {
            int trueCount = 0;
            for(int i = 0; i < testLoops; i++)
            {
                if (RNGManager.Manager[instanceTitle].NextBool())
                    trueCount++;
            }

            System.Diagnostics.Trace.WriteLine($"NextBoolTest: True: {trueCount} / {testLoops}");

            Assert.IsTrue(trueCount < testLoops / 2 + testLoops * rngBuffer);
            Assert.IsTrue(trueCount > testLoops / 2 - testLoops * rngBuffer);
        }

        [TestMethod]
        [TestCategory("Random generator")]
        [TestCategory("NextBool")]
        public void NextBoolProbabilityTest()
        {
            int trueCount = 0;
            for (int i = 0; i < testLoops; i++)
            {
                if (RNGManager.Manager[instanceTitle].NextBool(0.25))
                    trueCount++;
            }

            System.Diagnostics.Trace.WriteLine($"NextBoolProbabilityTest: True: {trueCount} / {testLoops}");

            Assert.IsTrue(trueCount < testLoops / 4 + testLoops * rngBuffer);
            Assert.IsTrue(trueCount > testLoops / 4 - testLoops * rngBuffer);
        }

        #endregion

        [TestMethod]
        [TestCategory("Random generator")]
        [TestCategory("NextFloat")]
        public void NextFloatTest()
        {
            float randomFloat;
            for (int i = 0; i < testLoops; i++)
            {
                randomFloat = RNGManager.Manager[instanceTitle].NextFloat();
                Assert.IsTrue(randomFloat >= 0 && randomFloat < 1);

                randomFloat = RNGManager.Manager[instanceTitle].NextFloat(100);
                Assert.IsTrue(randomFloat >= 0 && randomFloat < 100);

                randomFloat = RNGManager.Manager[instanceTitle].NextFloat(10, 100);
                Assert.IsTrue(randomFloat >= 10 && randomFloat < 100);
            }
        }

        [TestMethod]
        [TestCategory("Random generator")]
        [TestCategory("NextFloat")]
        public void NextIntTest()
        {
            int randomInt;
            for (int i = 0; i < testLoops; i++)
            {
                randomInt = RNGManager.Manager[instanceTitle].NextInt(100);
                Assert.IsTrue(randomInt >= 0 && randomInt < 100);

                randomInt = RNGManager.Manager[instanceTitle].NextInt(10, 100);
                Assert.IsTrue(randomInt >= 10 && randomInt < 100);
            }
        }

        [TestMethod]
        [TestCategory("Random generator")]
        [TestCategory("NextEnum")]
        public void NextEnumValue()
        {
            for (int i = 0; i < testLoops; i++)
            {
                RNGManager.Manager[instanceTitle].NextEnumValue<TestEnum>();
            }
        }

        [TestMethod]
        [TestCategory("Random generator")]
        [TestCategory("NextElement")]
        public void NextElementTest()
        {
            int count = 10;

            IEnumerable<int> elements = Enumerable.Range(0, count);
            int[] results = new int[count];

            for (int i = 0; i < testLoops; i++)
            {
                results[RNGManager.Manager[instanceTitle].NextElement(elements)]++;
            }

            for(int i = 0; i < count; i++)
            {
                Assert.IsTrue(results[i] < testLoops / count + testLoops * rngBuffer);
                Assert.IsTrue(results[i] > testLoops / count - testLoops * rngBuffer);
            }
        }

        #endregion
    }
}
