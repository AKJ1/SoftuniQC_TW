namespace BattlefieldTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BattleField;

    [TestClass]
    public class BattlefieldTests
    {
        [TestMethod]
        public void GeneratePlayFieldWithSizeTwo()
        {
            int fieldSize = 2;
            char[,] expectedOutput =
            {
                {'-', '-'},
                {'-', '-'}
            };

            char[,] actualOutput = GameServices.GenerateField(fieldSize);
            CollectionAssert.Equals(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void CheckIfMoveWithNegativeInputCoordinatesIsInvalidMove()
        {
            int coordinateX = -1;
            int coordinateY = -10;
            char[,] inputField =
            {
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '2', '-'},
                {'-', '-', 'X'}
            };

            bool moveCheckResult = GameServices.IsValidMove(inputField, coordinateX, coordinateY);
            Assert.IsFalse(moveCheckResult, "The input coordinates are on valid input position.");
        }

        [TestMethod]
        public void CheckIfMoveWithCoordinatesOutsideTheFieldIsInvalidMove()
        {
            int coordinateX = 50;
            int coordinateY = 50;
            char[,] inputField =
            {
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '2', '-'},
                {'-', '-', 'X'}
            };

            bool moveCheckResult = GameServices.IsValidMove(inputField, coordinateX, coordinateY);
            Assert.IsFalse(moveCheckResult, "The input coordinates are on valid input position.");
        }

        [TestMethod]
        public void CheckIfMoveWithCoordinatesOnBlownBombIsInvalidMove()
        {
            int coordinateX = 3;
            int coordinateY = 2;
            char[,] inputField =
            {
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '2', '-'},
                {'-', '-', 'X'}
            };

            bool moveCheckResult = GameServices.IsValidMove(inputField, coordinateX, coordinateY);
            Assert.IsFalse(moveCheckResult, "The input coordinates are on valid input position.");
        }

        [TestMethod]
        public void CheckIfMoveWithCoordinatesOnActiveBombIsValidMove()
        {
            int coordinateX = 2;
            int coordinateY = 1;
            char[,] inputField =
            {
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '2', '-'},
                {'-', '-', 'X'}
            };

            bool moveCheckResult = GameServices.IsValidMove(inputField, coordinateX, coordinateY);
            Assert.IsTrue(moveCheckResult, "The input coordinates are on valid input position.");
        }

        [TestMethod]
        public void CheckIfEmptyFieldContainsMinesAndMustReturnFalse()
        {
            char[,] inputField =
            {
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '-', '-'}
            };

            bool fieldCheckResult = GameServices.ContainsMines(inputField);
            Assert.IsFalse(fieldCheckResult, "The input field still has mines left.");
        }

        [TestMethod]
        public void CheckIfFieldWithMinesContainMinesAndMustReturnTrue()
        {
            char[,] inputField =
            {
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '2', '-'},
                {'-', '-', '-'}
            };

            bool fieldCheckResult = GameServices.ContainsMines(inputField);
            Assert.IsTrue(fieldCheckResult, "The input field has no mines left.");
        }

        [TestMethod]
        public void ExpolodeMineWithSizeOneShouldCoverAppropriateField()
        {
            char[,] initialFieldState =
            {
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '1', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'}
            };

            char[,] expectedFieldResult =
            {
                {'-', '-', '-', '-', '-'},
                {'-', 'X', '-', 'X', '-'},
                {'-', '-', 'X', '-', '-'},
                {'-', 'X', '-', 'X', '-'},
                {'-', '-', '-', '-', '-'}
            };

            Position2D inputPosition = new Position2D(2, 2);
            GameServices.Explode(initialFieldState, inputPosition);

            CollectionAssert.Equals(initialFieldState, expectedFieldResult);
        }

        [TestMethod]
        public void ExpolodeMineWithSizeTwoShouldCoverAppropriateField()
        {
            char[,] initialFieldState =
            {
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '2', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'}
            };

            char[,] expectedFieldResult =
            {
                {'-', '-', '-', '-', '-'},
                {'-', 'X', 'X', 'X', '-'},
                {'-', 'X', 'X', 'X', '-'},
                {'-', 'X', 'X', 'X', '-'},
                {'-', '-', '-', '-', '-'}
            };

            Position2D inputPosition = new Position2D(2, 2);
            GameServices.Explode(initialFieldState, inputPosition);

            CollectionAssert.Equals(initialFieldState, expectedFieldResult);
        }

        [TestMethod]
        public void ExpolodeMineWithSizeThreeShouldCoverAppropriateField()
        {
            char[,] initialFieldState =
            {
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '3', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'}
            };

            char[,] expectedFieldResult =
            {
                {'-', '-', 'X', '-', '-'},
                {'-', 'X', 'X', 'X', '-'},
                {'X', 'X', 'X', 'X', 'X'},
                {'-', 'X', 'X', 'X', '-'},
                {'-', '-', 'X', '-', '-'}
            };

            Position2D inputPosition = new Position2D(2, 2);
            GameServices.Explode(initialFieldState, inputPosition);

            CollectionAssert.Equals(initialFieldState, expectedFieldResult);
        }

        [TestMethod]
        public void ExpolodeMineWithSizeFourShouldCoverAppropriateField()
        {
            char[,] initialFieldState =
            {
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '4', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'}
            };

            char[,] expectedFieldResult =
            {
                {'-', 'X', 'X', 'X', '-'},
                {'X', 'X', 'X', 'X', 'X'},
                {'X', 'X', 'X', 'X', 'X'},
                {'X', 'X', 'X', 'X', 'X'},
                {'-', 'X', 'X', 'X', '-'}
            };

            Position2D inputPosition = new Position2D(2, 2);
            GameServices.Explode(initialFieldState, inputPosition);

            CollectionAssert.Equals(initialFieldState, expectedFieldResult);
        }

        [TestMethod]
        public void ExpolodeMineWithSizeFiveShouldCoverAppropriateField()
        {
            char[,] initialFieldState =
            {
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '5', '-', '-'},
                {'-', '-', '-', '-', '-'},
                {'-', '-', '-', '-', '-'}
            };

            char[,] expectedFieldResult =
            {
                {'X', 'X', 'X', 'X', 'X'},
                {'X', 'X', 'X', 'X', 'X'},
                {'X', 'X', 'X', 'X', 'X'},
                {'X', 'X', 'X', 'X', 'X'},
                {'X', 'X', 'X', 'X', 'X'}
            };

            Position2D inputPosition = new Position2D(2, 2);
            GameServices.Explode(initialFieldState, inputPosition);

            CollectionAssert.Equals(initialFieldState, expectedFieldResult);
        }

        [TestMethod]
        public void CheckIfFieldWithSizeTwoIsPrintedCorrectly()
        {
            //string asd = "Invalid index!";

//            using (var consoleOutput = new ConsoleOutput())
//            {
//                string asd = @"Invalid index!
//";
//                GameServices.ExtractMineFromString("1");
//                Assert.AreEqual(asd, consoleOutput.GetOuput());
//            }

            char[,] inputField =
            {
                {'-', '-'},
                {'-', '-'}
            };
            string expectedOutput =
@"   0 1 
   ----
0 |- - 
1 |- - 
";

            using (var consoleOutput = new ConsoleOutput())
            {
                GameServices.PrintResults(inputField);
                Assert.AreEqual(expectedOutput, consoleOutput.GetOuput());
            }
        }

        [TestMethod]
        public void CheckIfFieldWithBlownMineIsPrintedCorrectly()
        {
            char[,] inputField =
            {
                {'-', '-', '-', '-', '-'},
                {'-', 'X', '-', 'X', '-'},
                {'-', '-', 'X', '-', '-'},
                {'-', 'X', '-', 'X', '-'},
                {'-', '-', '-', '-', '-'}
            };
            string expectedOutput =
@"   0 1 2 3 4 
   ----------
0 |- - - - - 
1 |- X - X - 
2 |- - X - - 
3 |- X - X - 
4 |- - - - - 
";

            using (var consoleOutput = new ConsoleOutput())
            {
                GameServices.PrintResults(inputField);
                Assert.AreEqual(expectedOutput, consoleOutput.GetOuput());
            }
        }

        [TestMethod]
        public void ExtractPositionFromStringWithNullParameterShouldPrintAppropriateOutput()
        {
            string positionInput = null;
            string expectedOutput = "Invalid index!\r\n";
            using (var consoleOutput = new ConsoleOutput())
            {
                GameServices.ExtractMineFromString(positionInput);
                Assert.AreEqual(expectedOutput, consoleOutput.GetOuput());
            }
        }

        [TestMethod]
        public void ExtractPositionFromStringWithLenghtBelowThreeCharactersShouldPrintAppropriateOutput()
        {
            string positionInput = "2 ";
            string expectedOutput = "Invalid index!\r\n";
            using (var consoleOutput = new ConsoleOutput())
            {
                GameServices.ExtractMineFromString(positionInput);
                Assert.AreEqual(expectedOutput, consoleOutput.GetOuput());
            }
        }

        [TestMethod]
        public void ExtractPositionFromStringWithoutEmptySpaceShouldPrintAppropriateOutput()
        {
            string positionInput = "245";
            string expectedOutput = "Invalid index!\r\n";
            using (var consoleOutput = new ConsoleOutput())
            {
                GameServices.ExtractMineFromString(positionInput);
                Assert.AreEqual(expectedOutput, consoleOutput.GetOuput());
            }
        }

        [TestMethod]
        public void ExtractPositionFromStringContainingLettersShouldPrintAppropriateOutput()
        {
            string positionInput = "245";
            string expectedOutput = "Invalid index!\r\n";
            using (var consoleOutput = new ConsoleOutput())
            {
                GameServices.ExtractMineFromString(positionInput);
                Assert.AreEqual(expectedOutput, consoleOutput.GetOuput());
            }
        }

        [TestMethod]
        public void ExtractPositionFromInvalidStringParameterShouldReturnNull()
        {
            string positionInput = "245";
            Assert.AreEqual(null, GameServices.ExtractMineFromString(positionInput));
        }

        [TestMethod]
        public void ExtractPositionFromValidStringParameterShouldReturnNewInputPosition()
        {
            string positionInput = "2 5";
            Position2D expectedPosition = new Position2D(2, 5);
            Position2D returnedPosition = GameServices.ExtractMineFromString(positionInput);
            CollectionAssert.Equals(expectedPosition, returnedPosition);
        }
    }
}
