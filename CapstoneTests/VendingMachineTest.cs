using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void FeedAndDispenseTest()
        {
            VendingMachine testMachine = new VendingMachine();

            //Feed Money Test
            testMachine.FeedMoney(4);
            Assert.AreEqual("4.00", testMachine.CheckBalance());
            //Dispense Item Test
            testMachine.DispenseItem("A1");

            //Bool methods tests
            Assert.IsTrue(testMachine.DoesExist("A1"));
            Assert.IsTrue(testMachine.IsAvailable("A1"));
            Assert.IsTrue(testMachine.IsEnoughMoney("A1", 400));

            //Check balance, display item, reduce inventory tests
            Assert.AreEqual("0.95", testMachine.CheckBalance());
            string[] testArray = testMachine.DisplayItems();
            Assert.AreEqual("A1 Potato Crisps $3.05| 4", testArray[0]);

            //Gve Change test
            int[] test = new int[] { 3, 2, 0 };
            CollectionAssert.AreEquivalent(test, testMachine.GiveChange());
            Assert.AreEqual("0.00", testMachine.CheckBalance());

            //Dispense item message test
            Assert.AreEqual("A1: Potato Crisps - Crunch Crunch, Yum!", testMachine.DispenseItem("A1"));
        }
    }
}
