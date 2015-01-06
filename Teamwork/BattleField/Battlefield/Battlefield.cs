namespace BattleField
{
    using System;

    public class Battlefield
    {
        private char[,] gameField;

        public Battlefield()
        {

        }

        #region Input
        private int GetInitialInput()
        {
            string userInput = Console.ReadLine();
            int size = 0;
            while (!int.TryParse(userInput, out size))
            {
                Console.WriteLine("Wrong Format or number out of range.");
                Console.Write("Input battlefield size: [Range: 1 to 10] n = ");
                userInput = Console.ReadLine();
            }
            
            if ((size > 10 || size <= 0))
            {
                Console.Write("Number out of bounds. Enter a new one [from 1 to 10] n = ");
                size = GetInitialInput();
            }
            
            return size;
        }

        private Position2D GetMoveInput()
        {
            Console.Write("Please enter coordinates: ");
            string userInput = Console.ReadLine();
            Position2D mine = GameServices.ExtractMineFromString(userInput);
            if (mine == null)
            {
                mine = GetMoveInput();
            }

            return mine;
        }
        #endregion

        public void Start()
        {
            Console.WriteLine(@"Welcome to ""Battle Field"" game. ");
            Console.Write("Input battlefield size: [Range: 1 to 10] n = ");
            int size = GetInitialInput();
            this.gameField = GameServices.GenerateField(size);
            GameLoop();
        }

        private void GameLoop()
        {
            int blownMines = 0;
            while (GameServices.ContainsMines(this.gameField))
            {
                GameServices.PrintResults(this.gameField);
                Position2D inputPosition = GetMoveInput();
                if (GameServices.IsValidMove(this.gameField, inputPosition.X, inputPosition.Y))
                {
                    GameServices.Explode(this.gameField, inputPosition);
                    blownMines++;
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }
            }

            EndGame(blownMines);
        }

        private void EndGame(int score)
        {
            GameServices.PrintResults(this.gameField);
            Console.WriteLine("Game over. Detonated mines: {0}", score);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
