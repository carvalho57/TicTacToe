using TicTacToe.Enums;

namespace TicTacToe
{
    public class Game
    {
        private IDictionary<EMarker, Player> _players;
        private Board _board;

        public Game()
        {
            _board = new Board();
            _players = new Dictionary<EMarker, Player>();
        }

        public void Run()
        {
            Welcome();
            InitializePlayers();
            Start();
        }

        private void Start()
        {
            var currentPlayer = _players.Values.First();

            do
            {
                Console.WriteLine($"Vez do jogador {currentPlayer.Name} - {currentPlayer.Marker}");
                Console.Write("Seleciona a posição x,y: ");
                string[] position = Console.ReadLine().Split(',');

                if (position.Length != 2)
                {
                    Console.WriteLine("Posição inválida, por favor informe a posição no formato x,y");
                    continue;
                }

                if (!int.TryParse(position[0], out int x) || !int.TryParse(position[1], out int y))
                {
                    Console.WriteLine("Posição inválida, por favor informe a posição no formato x,y");
                    continue;
                }

                if (!_board.IsPositionValid(x, y))
                {
                    Console.WriteLine("Posição inválida, por favor informe uma posição válida");
                    continue;
                }

                _board.SetPosition(x, y, currentPlayer);
                _board.Print();

                (EGameStatus status, EMarker? winner) =  _board.ValidateEnd();

                if (status == EGameStatus.Winner)
                {
                    ShowWinner(_players[winner.GetValueOrDefault()]);
                    break;
                }

                if (status == EGameStatus.Draw)
                {
                    Draw();
                    break;
                }


                currentPlayer = currentPlayer == _players[EMarker.X] ? _players[EMarker.O] : _players[EMarker.X];

            } while (true);
        }

        private void InitializePlayers()
        {
            EMarker? marker = null;

            for (int i = 0; i < 2; i++)
            {
                Console.Write($"Informe o nome do jogador {i + 1} : ");
                string? name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Nome não pode ser vazio, por favor informe um nome valido");
                    i--;
                    continue;
                }

                if (marker is null)
                {
                    Console.Write("Selecione o marcador X ou O:");
                    string? selectedMaker = Console.ReadLine();

                    if (selectedMaker == "X")
                        marker = EMarker.X;
                    else if (selectedMaker == "O")
                        marker = EMarker.O;
                    else
                    {
                        Console.WriteLine("Marcador invalido, por favor selecione X ou O");
                        i--;
                        continue;
                    }
                }
                else
                    marker = marker == EMarker.X ? EMarker.O : EMarker.X;

                _players.Add(marker.GetValueOrDefault(), new Player(name, marker.GetValueOrDefault()));
            }

            Console.WriteLine("Jogadores registrados:");

        }

        private void Welcome()
        {
            Console.WriteLine("Bem-vindo ao Jogo TIC TAC TOE!");
            Console.WriteLine("Tabuleiro:");
            _board.Print();
            Console.WriteLine();
        }


        private void Draw()
        {
            Console.WriteLine(@"
                ┳┓┏┓┳┳  ┓┏┏┓┓ ┓┏┏┓
                ┃┃┣ ┃┃  ┃┃┣ ┃ ┣┫┣┫
                ┻┛┗┛┗┛  ┗┛┗┛┗┛┛┗┛┗
                ");
        }
        private void ShowWinner(Player player)
        {
            Console.WriteLine(@"
                  _____                _       __           
                 |  __ \              | |     /_/           
                 | |__) |_ _ _ __ __ _| |__   ___ _ __  ___ 
                 |  ___/ _` | '__/ _` | '_ \ / _ \ '_ \/ __|
                 | |  | (_| | | | (_| | |_) |  __/ | | \__ \
                 |_|   \__,_|_|  \__,_|_.__/ \___|_| |_|___/
            ");
            Console.WriteLine($"Parabéns {player.Name} você ganhou!");
        }
    }
}
