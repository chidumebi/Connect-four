using System;

namespace Connect_four
{


    class Board
    {
        private const int Rows = 6;
        private const int Cols = 7;
        private readonly char[,] grid = new char[Rows, Cols];

        public Board()
        {
            // Initialize the board with empty spaces
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    grid[row, col] = ' ';
                }
            }
        }

        public void Display()
        {
            // Display the current state of the board
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Console.Write($"|{grid[row, col]}");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(new string('-', Cols * 2 + 1));
        }


        public bool PlacePiece(int col, char piece)
        {
            // Check if the column index is valid
            if (col < 0 || col >= Cols)
            {
                Console.WriteLine("Invalid column index.");
                return false;
            }

            // Check if the column is full
            if (grid[0, col] != ' ')
            {
                Console.WriteLine("Column is full. Please choose another column.");
                return false;
            }

            // Place a piece on the board
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (grid[row, col] == ' ')
                {
                    grid[row, col] = piece;
                    return true;
                }
            }

            return false; // This should not be reached under normal circumstances
        }

        // Adding methods for checking win and full board
        public bool CheckWin(char piece)
        {
            //Checking for horizontal win
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols - 3; col++)
                {
                    if (grid[row, col] == piece &&
                        grid[row, col + 1] == piece &&
                        grid[row, col + 2] == piece)
                    {
                        return true;
                    }
                }
            }


            //Checking for vertical win
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (grid[row, col] == piece &&
                       grid[row + 1, col] == piece &&
                      grid[row + 2, col] == piece)
                    {
                        return true;
                    }
                }
            }

            //Checking for diagonal win (left to right)
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 0; col < Cols - 3; col++)
                {
                    if (grid[row, col] == piece &&
                      grid[row + 1, col + 1] == piece &&
                      grid[row + 2, col + 2] == piece)
                    {
                        return true;
                    }
                }
            }

            //Checking for diagonal win (right to left)
            for (int row = 0; row < Rows - 3; row++)
            {
                for (int col = 3; col < Cols; col++)
                {
                    if (grid[row, col] == piece &&
                      grid[row + 1, col - 1] == piece &&
                      grid[row + 2, col - 2] == piece)
                    {
                        return true;
                    }
                }
            }

            //if there is no win
            return false;
        }
        public bool CheckFullBoard(char piece)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if (grid[row, col] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    class Player
    {
        public string Name { get; }
        public char Piece { get; }

        public Player(string name, char piece)
        {
            Name = name;
            Piece = piece;
        }
    }


    class Connect4Game
    {
        private readonly Board board = new Board();
        private readonly Player player1;
        private readonly Player player2;
        private Player currentPlayer;

        public Connect4Game(string player1Name, string player2Name)
        {
            player1 = new Player(player1Name, '1');
            player2 = new Player(player2Name, '0');
            currentPlayer = player1;
        }

        public void Play()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                board.Display();
                Console.WriteLine($"{currentPlayer.Name}'s turn ({currentPlayer.Piece}):");
                int col = int.Parse(Console.ReadLine()) - 1; // Subtract 1 to make it 0-based
                if (board.PlacePiece(col, currentPlayer.Piece))
                {

                    // Checking for a win
                    if (board.CheckWin(currentPlayer.Piece))
                    {
                        board.Display();
                        Console.WriteLine(currentPlayer.Name + " wins!");
                        gameOver = true;
                    }
                    // Checking for a full board
                    else if (board.CheckFullBoard(currentPlayer.Piece))
                    {
                        board.Display();
                        Console.WriteLine("It's a tie!");
                        gameOver = true;
                    }

                    // Switching player turn

                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                }
                else
                {
                    Console.WriteLine("Column is full. Please choose another column.");
                }
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("A connect four game");

            Console.WriteLine("Welcome to Connect 4!");
            Console.WriteLine("Enter Player 1's name:");
            string player1Name = Console.ReadLine();
            Console.WriteLine("Enter Player 2's name:");
            string player2Name = Console.ReadLine();

            Connect4Game game = new Connect4Game(player1Name, player2Name);
            game.Play();
        }
    }

}

