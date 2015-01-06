namespace BattleField
{
    using System;
    class Battlefield
    {
        #region Variables
        private char[,] gameField;
        #endregion

        #region Consturctor(s)
        public Battlefield()
        {
            gameField = null;
        }
        #endregion
       
        #region Input
        private int GetInitialInput()
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
                size = GetInitialInput();
            }
            return size;
        }

        private MineMove GetMoveInput()
        {
            Console.Write("Please enter coordinates: ");
            MineMove mine = GameServices.ExtractMineFromString(Console.ReadLine());
            mine = mine ?? (mine = GetMoveInput());
            return mine;
        }
        #endregion

        #region Start
        public void Start()
        {
            Console.WriteLine(@"Welcome to ""Battle Field"" game. ");
            Console.Write("Input battlefield size: [Range: 1 to 10] n = ");
            int size = GetInitialInput();
            gameField = GameServices.GenerateField(size);
            GameLoop();
        }
        #endregion  

        #region Gameloop
        private void GameLoop()
        {
            int blownMines = 0;
            while (GameServices.ContainsMines(gameField))
            {
                GameServices.PrintResults(gameField);
                MineMove inputMine = GetMoveInput();
                if (GameServices.IsValidMove(gameField, inputMine.X, inputMine.Y))
                {
                    GameServices.Explode(gameField, inputMine);
                    blownMines++;
                }
                else
                {
                    Console.WriteLine("Invalid move!");
                }
            }
            EndGame(blownMines);
        }
        #endregion

        #region End Game
        private void EndGame(int score)
        {
            GameServices.PrintResults(gameField);
            Console.WriteLine("Game over. Detonated mines: {0}", score);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        #endregion
    }
}
