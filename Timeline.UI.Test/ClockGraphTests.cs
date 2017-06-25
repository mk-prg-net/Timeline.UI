using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace Timeline.UI.Test
{
    [TestClass]
    public class ClockGraphTests
    {

        IEnumerable<Vector2> SecoundHand;
        IEnumerable<Vector2> MinuteHand;
        IEnumerable<Vector2> HourHand;

        [TestInitialize]
        public void Init()
        {
            SecoundHand = new Vector2[] {
                new Vector2(),
                new Vector2(1.5f, 0)
            };

            MinuteHand = new Vector2[] {
                new Vector2(),
                new Vector2(1.0f, 0)
            };


            HourHand = new Vector2[] {
                new Vector2(),
                new Vector2(0.7f, 0)
            };

        }

        [TestMethod]
        public void ClockGraphTests_TimePointers()
        {
            // Ohne Translation
            var cg = new ClockGraph(new Vector2(), 2.0f);


            Assert.AreEqual(0.95f, cg.SecoundHand.Skip(1).First().X, 0.001f);


        }



        [TestMethod]
        public void ClockGraphTests_TimePointersPos()
        {
            // Ohne Translation
            var cg = new ClockGraph(new Vector2(), 2.0f);

            var MinuteHandTransf = cg.SetTimePointer60(0, MinuteHand);

            Assert.AreEqual(0.0f, MinuteHandTransf[0].X, 0.001f);
            Assert.AreEqual(0.0f, MinuteHandTransf[0].Y, 0.001f);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(-1.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(15, MinuteHand);
            Assert.AreEqual(1.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(30, MinuteHand);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(1.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(45, MinuteHand);
            Assert.AreEqual(-1.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(60, MinuteHand);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(-1.0f, MinuteHandTransf[1].Y, 0.001f);

            // Mit Translation
            cg = new ClockGraph(new Vector2(1, 1), 2.0f);

            MinuteHandTransf = cg.SetTimePointer60(0, MinuteHand);

            Assert.AreEqual(1.0f, MinuteHandTransf[0].X, 0.001f);
            Assert.AreEqual(1.0f, MinuteHandTransf[0].Y, 0.001f);
            Assert.AreEqual(1.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(15, MinuteHand);
            Assert.AreEqual(2.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(1.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(30, MinuteHand);
            Assert.AreEqual(1.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(2.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(45, MinuteHand);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(1.0f, MinuteHandTransf[1].Y, 0.001f);

            MinuteHandTransf = cg.SetTimePointer60(60, MinuteHand);
            Assert.AreEqual(1.0f, MinuteHandTransf[1].X, 0.001f);
            Assert.AreEqual(0.0f, MinuteHandTransf[1].Y, 0.001f);

        }
    }
}
