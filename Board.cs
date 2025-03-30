namespace TicTacToe
{
    public class Board
    {
        private char[,] board = new char[3,3];

        public Board()
        {
            Reset();
        }

        public void Print()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j == 2)
                        Console.WriteLine();
                }
            }
        }

        public void Reset()
        {
            board = new char[3, 3]
            {
                {'-', '-', '-'},
                {'-', '-', '-'},
                {'-', '-', '-'},
            };
        }

        public void SetPosition(int x, int y, Player player)
        {
            if (x < 0 || x > 2 || y < 0 || y > 2)
                throw new ArgumentException("Posição inválida");

            if (board[x, y] != (char)Marker.None)
                throw new InvalidOperationException("Esta posição já esta preenchida");

            board[x, y] = (char)player.Marker;

        }       
        
        public void ValidateBoard()
        {
            (bool winner, Marker player) = CheckWinner();

            // Verificar se alguém ganhou
            if (winner)
            {
                Console.WriteLine($"O jogador {player} ganhou!");
                return;
            }            

            // Verificar se deu velha
            if (board.Cast<char>().All(c => c != (char)Marker.None))
            {
                Console.WriteLine("Deu velha!");
                return;
            }
        }

        public (bool winner, Marker player) CheckWinner()
        {
            //Verifica colunas e linhas
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2] ||
                        board[0, i] == board[1, i] && board[0, i] == board[2, i])
                {
                    if(!Enum.TryParse(board[i, 0].ToString(), out Marker winner))
                        throw new Exception("Não foi possível determinar o vencedor");

                    return (true, winner);
                }
            }

            //Verifica diagonal
            if (board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] ||
                   board[0, 2] == board[1, 1] && board[0, 0] == board[2, 0])
            {
                if (!Enum.TryParse(board[0, 0].ToString(), out Marker winner))
                    throw new Exception("Não foi possível determinar o vencedor");
            }

             return (false, Marker.None);
        }
    }
}
