namespace BattleField
{
    using System;
    using System.Collections.Generic;
    public static class GameServices
    {
        #region Variables
        private static readonly Random Rand = new Random();
        private const double LowerBoundMines = 0.15;
        private const double UpperBoundMines = 0.3;
        #endregion

        #region Generation Algorithm
        /// <summary>
        /// Returns the number of mines that need to be set on the field
        /// </summary>
        /// <param name="size">This sets the field's X and Y lengths</param>
        /// <returns>int</returns>
        private static int DetermineMineCount(int size)
        {
            double fields = (double)size * size;
            int lowBound = (int)(LowerBoundMines * fields);
            int upperBound = (int)(UpperBoundMines * fields);

            return Rand.Next(lowBound, upperBound);
        }

        /// <summary>
        /// Generates a 2D char array to be used as a game field.
        /// The Generated array is a equilateral(square) array.
        /// </summary>
        /// <param name="size">the size of the array</param>
        /// <returns>char[,]</returns>
        public static char[,] GenerateField(int size)
        {
            char[,] field = new char[size, size];
            int minesCount = DetermineMineCount(size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    field[i, j] = '-';
                }
            }

            for (int i = 0; i < minesCount; i++)
            {
                int mineX = Rand.Next(0, size);
                int mineY = Rand.Next(0, size);
                int mineType = Rand.Next('1', '6');

                field[mineX, mineY] = Convert.ToChar(mineType);
            }

            return field;
        }
        #endregion

        #region Logic Checks
        /// <summary>
        /// Checks a field to see whether the designated X Y coordinates exist.
        /// </summary>
        /// <param name="field"> 2D Char array</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>bool</returns>
        private static bool CheckField(char[,] field, int x, int y)
        {
            if (x < 0 || y < 0 || x >= field.GetLength(0) || y >= field.GetLength(1))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Checks whether a move is valid or not.
        /// </summary>
        /// <param name="field">A 2D char array.</param>
        /// <param name="x">X cooridnate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>bool</returns>
        public static bool IsValidMove(char[,] field, int x, int y)
        {
            if (!CheckField(field, x, y))
            {
                return false;
            }

            if (field[x, y] == 'X' || field[x, y] == '-')
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether there is anything interactable left on the field
        /// </summary>
        /// <param name="field">A 2D char array.</param>
        /// <returns>bool</returns>
        public static bool ContainsMines(char[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] != '-' && field[i, j] != 'X')
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region Explosion Actions

        // What the fuck is even going on here?

        /// <summary>
        /// Chooses which Algorithm to use for the explosion.
        /// </summary>
        /// <param name="field">the field</param>
        /// <param name="mine">the mine to explode</param>
        public static void Explode(char[,] field, Position2D mine)
        {
            char mineType = field[mine.X, mine.Y];

            switch (mineType)
            {
                case '1':
                    {
                        ExplodeOne(field, mine);
                    }
                    break;
                case '2':
                    {
                        ExplodeTwo(field, mine);
                    }
                    break;
                case '3':
                    {
                        ExplodeThree(field, mine);
                    }
                    break;
                case '4':
                    {
                        ExplodeFour(field, mine);
                    }
                    break;
                case '5':
                    {
                        ExplodeFive(field, mine);
                    }
                    break;
            }
        }

        private static void ExplodeOne(char[,] field, Position2D mine)
        {
            Position2D upperRightCorner = new Position2D(mine.X - 1, mine.Y - 1);
            Position2D upperLeftCorner = new Position2D(mine.X - 1, mine.Y + 1);
            Position2D downRightCorner = new Position2D(mine.X + 1, mine.Y - 1);
            Position2D downLeftCorner = new Position2D(mine.X + 1, mine.Y + 1);

            if (CheckField(field, mine.X, mine.Y))
            {
                field[mine.X, mine.Y] = 'X';
            }

            if (CheckField(field, upperRightCorner.X, upperRightCorner.Y))
            {
                field[upperRightCorner.X, upperRightCorner.Y] = 'X';
            }

            if (CheckField(field, upperLeftCorner.X, upperLeftCorner.Y))
            {
                field[upperLeftCorner.X, upperLeftCorner.Y] = 'X';
            }

            if (CheckField(field, downRightCorner.X, downRightCorner.Y))
            {
                field[downRightCorner.X, downRightCorner.Y] = 'X';
            }

            if (CheckField(field, downLeftCorner.X, downLeftCorner.Y))
            {
                field[downLeftCorner.X, downLeftCorner.Y] = 'X';
            }
        }

        private static void ExplodeTwo(char[,] field, Position2D mine)
        {
            for (int i = mine.X - 1; i <= mine.X+1; i++)
            {
                for (int j = mine.Y - 1; j <= mine.Y+1; j++)
                {
                    if(CheckField(field, i,j))
                    {
                        field[i, j] = 'X';
                    }
                }
            }
        }

        private static void ExplodeThree(char[,] field, Position2D mine)
        {
            ExplodeTwo(field, mine);
            Position2D up = new Position2D(mine.X - 2, mine.Y);
            Position2D down = new Position2D(mine.X + 2, mine.Y);
            Position2D left = new Position2D(mine.X, mine.Y - 2);
            Position2D right = new Position2D(mine.X, mine.Y + 2);

            if (CheckField(field, up.X, up.Y))
            {
                field[up.X, up.Y] = 'X';
            }

            if (CheckField(field, down.X, down.Y))
            {
                field[down.X, down.Y] = 'X';
            }

            if (CheckField(field, left.X, left.Y))
            {
                field[left.X, left.Y] = 'X';
            }

            if (CheckField(field, right.X, right.Y))
            {
                field[right.X, right.Y] = 'X';
            }
        }

        private static void ExplodeFour(char[,] field, Position2D mine)
        {
            for (int i = mine.X - 2; i <= mine.X + 2; i++)
            {
                for (int j = mine.Y - 2; j <= mine.Y + 2; j++)
                {
                    bool upRight = i == mine.X - 2 && j == mine.Y - 2;
                    bool upLeft = i == mine.X - 2 && j == mine.Y + 2;
                    bool downRight = i == mine.X + 2 && j == mine.Y - 2;
                    bool downLeft = i == mine.X + 2 && j == mine.Y + 2;

                    if (upRight) continue;
                    if (upLeft) continue;
                    if (downRight) continue;
                    if (downLeft) continue;

                    if (CheckField(field, i, j))
                    {
                        field[i, j] = 'X';
                    }
                }
            }

        }

        private static void ExplodeFive(char[,] field, Position2D mine)
        {
            for (int i = mine.X - 2; i <= mine.X + 2; i++)
            {
                for (int j = mine.Y - 2; j <= mine.Y + 2; j++)
                {
                    if (CheckField(field, i, j))
                    {
                        field[i, j] = 'X';
                    }
                }
            }
        }

        #endregion

        #region Extra actions
        /// <summary>
        /// Prints the field to the console.
        /// This is the method used to show player the gameboard.
        /// </summary>
        /// <param name="field">The field to show</param>
        public static void PrintResults(char[,] field)
        {
            Console.Write("   ");
            int size = field.GetLength(0);
            
            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} ", i);
            }

            Console.WriteLine();
            Console.Write("   ");
            
            for (int i = 0; i < size*2; i++)
            {
                Console.Write("-");
            }
            
            Console.WriteLine();

            for (int i = 0; i < size; i++)
            {
                Console.Write("{0} |", i);
            
                for (int j = 0; j < size; j++)
                {
                    Console.Write("{0} ", field[i,j]);
                }
                
                Console.WriteLine();
            }
        }

        public static Position2D ExtractMineFromString(string line)
        {
            if (line == null || line.Length < 3 || !line.Contains(" "))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            string[] splited = line.Split(' ');

            int x = 0;
            int y = 0;

            if (!int.TryParse(splited[0], out x))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            if (!int.TryParse(splited[1], out y))
            {
                Console.WriteLine("Invalid index!");
                return null;
            }

            return new Position2D(x, y);
        }
        #endregion
    }
}
