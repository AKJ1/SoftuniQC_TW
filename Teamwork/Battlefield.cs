using System;
using System.Globalization;

namespace BattleField
{
    class Battlefield
    {
        private char[,] gameField;
        public Battlefield()
        {
            gameField = null;
        }
        public void Start()
        {
            Console.WriteLine(@"Welcome to ""Battle Field"" game. ");
            Console.Write("Input battlefield size: [Range: 1 to 10] n = ");
            int size = GetInput();
            gameField = GameServices.GenerateField(size);
            StartInteraction();
        }
        #region Input
        private int GetInput()
        {
            string readBuffer = Console.ReadLine();
            int size = 0;
            while (!int.TryParse(readBuffer, out size))
            {
                Console.WriteLine("Wrong Format or number out of range.");
                Console.Write("Input battlefield size: [Range: 1 to 10] n = ");
                readBuffer = Console.ReadLine();
            }
            if ((size > 10 || size <= 0))
            {
                Console.Write("Number out of bounds. Enter a new one [from 1 to 10] n = ");
                size = GetInput();
            }
            return size;
        }
        #endregion

        private void StartInteraction()
        {
            string readBuffer = null;
            int blownMines = 0;
            while (GameServices.ContainsMines(gameField))
            {
                GameServices.ShowResults(gameField);
                Console.Write("Please enter coordinates: ");
                readBuffer = Console.ReadLine();
                Mine mineToBlow = GameServices.ExtractMineFromString(readBuffer);

                while (mineToBlow == null)
                {
                    Console.Write("Please enter coordinates: ");
                    readBuffer = Console.ReadLine();
                    mineToBlow = GameServices.ExtractMineFromString(readBuffer);
                }

                if (!GameServices.IsValidMove(gameField, mineToBlow.X, mineToBlow.Y))
                {
                    Console.WriteLine("Invalid move!");
                    continue;
                }

                GameServices.Explode(gameField, mineToBlow);
                blownMines++;
            }

            GameServices.ShowResults(gameField);
            Console.WriteLine("Game over. Detonated mines: {0}", blownMines);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
