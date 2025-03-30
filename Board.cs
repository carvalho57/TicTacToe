using TicTacToe.Enums;

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
                Console.WriteLine($" {board[i,0]} | {board[i, 1]} | {board[i, 2]} ");

                if (i < 2)
                Console.WriteLine("---+---+---");
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

        public bool IsPositionValid(int x, int y)
        {            
            return x >= 0 && x < 3 && y >= 0 && y < 3 && board[x, y] == (char)EMarker.None;
        }

        public void SetPosition(int x, int y, Player player)
        {
            if (IsPositionValid(x, y) == false)
                throw new ArgumentException("Posição inválida");

            board[x, y] = (char)player.Marker;
        }       
        
        public (EGameStatus, EMarker?) ValidateEnd()
        {
            (bool winner, EMarker player) = CheckWinner();

            if (winner)
                return (EGameStatus.Winner,player);

            if (board.Cast<char>().All(c => c != (char)EMarker.None))
                return (EGameStatus.Draw, null);

            return (EGameStatus.Turn, null);
        }

        private (bool winner, EMarker player) CheckWinner()
        {
            //Verifica colunas e linhas
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2] && board[i,0] != (char)EMarker.None)
                {
                    if(!Enum.TryParse(board[i, 0].ToString(), out EMarker winner))
                        throw new Exception("Não foi possível determinar o vencedor");

                    return (true, winner);
                }

                if (board[0, i] == board[1, i] && board[0, i] == board[2, i] && board[0,i] != (char)EMarker.None)
                {
                    if (!Enum.TryParse(board[0, i].ToString(), out EMarker winner))
                        throw new Exception("Não foi possível determinar o vencedor");

                    return (true, winner);
                }                
            }

            //Verifica diagonal
            if ((board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] || board[0, 2] == board[1, 1] && board[0, 0] == board[2, 0]) && board[0, 0] != (char)EMarker.None)
            {
                if (!Enum.TryParse(board[0, 0].ToString(), out EMarker winner))
                    throw new Exception("Não foi possível determinar o vencedor");
                return (true, winner);
            }

             return (false, EMarker.None);
        }
    }
}
