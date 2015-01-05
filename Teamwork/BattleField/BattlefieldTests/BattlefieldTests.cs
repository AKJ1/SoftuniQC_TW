namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleField;

    [TestClass]
    public class BattlefieldTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            //string asd = "Invalid index!";

            using (var consoleOutput = new ConsoleOutput())
            {
                string asd = @"Invalid index!
";
                GameServices.ExtractMineFromString("1");
                Assert.AreEqual(asd, consoleOutput.GetOuput());
            }


            //            string asd =
            //@"   0 1 
            //   ----
            //0 |- - 
            //1 |- - 
            //";

            //            char[,] charsArr =
            //            {
            //                {'-', '-'},
            //                {'-', '-'}
            //            };

            //            using (var consoleOutput = new ConsoleOutput())
            //            {
            //                GameServices.PrintResults(charsArr);
            //                Assert.AreEqual(asd, consoleOutput.GetOuput());
            //            }
        }
    }
}
