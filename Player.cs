using TicTacToe.Enums;

namespace TicTacToe
{

    public class Player
    {
        public string Name { get; set; }
        public EMarker Marker { get; set; }

        public Player(string name, EMarker marker)
        {
            Name = name;
            Marker = marker;
        }

        public override string ToString()
        {
            return Marker.ToString();
        }
    }
}
