using Microsoft.VisualStudio.TestTools.UnitTesting;
using Savory.StateMachine.Generic;

namespace Savory.StateMachine.MSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Machine<H2O, Temperature> machine = new Machine<H2O, Temperature>();

            //״̬
            machine.AddState(H2O.Ice);
            machine.AddState(H2O.Water);
            machine.AddState(H2O.Vapour);

            //״̬ + ��Ϊ = ״̬
            machine.AddStateAction(H2O.Ice, Temperature.Up, H2O.Water);
            machine.AddStateAction(H2O.Ice, Temperature.Upup, H2O.Vapour);
            machine.AddStateAction(H2O.Water, Temperature.Up, H2O.Vapour);
            machine.AddStateAction(H2O.Water, Temperature.Down, H2O.Ice);
            machine.AddStateAction(H2O.Vapour, Temperature.Down, H2O.Water);
            machine.AddStateAction(H2O.Vapour, Temperature.Downdown, H2O.Ice);

            Assert.AreEqual(H2O.Water, machine.ComputeState(H2O.Ice, Temperature.Up));
            Assert.AreEqual(H2O.Vapour, machine.ComputeState(H2O.Ice, Temperature.Upup));
            Assert.AreEqual(H2O.None, machine.ComputeState(H2O.Ice, Temperature.Down));
            Assert.AreEqual(H2O.None, machine.ComputeState(H2O.Ice, Temperature.Downdown));

            Assert.AreEqual(H2O.Vapour, machine.ComputeState(H2O.Water, Temperature.Up));
            Assert.AreEqual(H2O.None, machine.ComputeState(H2O.Water, Temperature.Upup));
            Assert.AreEqual(H2O.Ice, machine.ComputeState(H2O.Water, Temperature.Down));
            Assert.AreEqual(H2O.None, machine.ComputeState(H2O.Water, Temperature.Downdown));

            Assert.AreEqual(H2O.None, machine.ComputeState(H2O.Vapour, Temperature.Up));
            Assert.AreEqual(H2O.None, machine.ComputeState(H2O.Vapour, Temperature.Upup));
            Assert.AreEqual(H2O.Water, machine.ComputeState(H2O.Vapour, Temperature.Down));
            Assert.AreEqual(H2O.Ice, machine.ComputeState(H2O.Vapour, Temperature.Downdown));

            var iceNext = machine.NextStates(H2O.Ice);
            var waterNext = machine.NextStates(H2O.Water);
            var vapourNext = machine.NextStates(H2O.Vapour);

            Assert.IsNotNull(iceNext);
            Assert.IsNotNull(waterNext);
            Assert.IsNotNull(vapourNext);

            Assert.AreEqual(2, iceNext.Count);
            Assert.AreEqual(2, waterNext.Count);
            Assert.AreEqual(2, vapourNext.Count);

            Assert.IsTrue(iceNext.Contains(H2O.Water));
            Assert.IsTrue(iceNext.Contains(H2O.Vapour));
            Assert.IsTrue(waterNext.Contains(H2O.Ice));
            Assert.IsTrue(waterNext.Contains(H2O.Vapour));
            Assert.IsTrue(vapourNext.Contains(H2O.Ice));
            Assert.IsTrue(vapourNext.Contains(H2O.Water));
        }

        enum H2O
        {
            None,

            Ice,

            Water,

            Vapour
        }

        enum Temperature
        {
            Up,

            Down,

            Upup,

            Downdown
        }
    }
}
