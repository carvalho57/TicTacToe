namespace TicTacToe
{
    public enum Marker
    {
        X = 'X',
        O = 'O',
        None = '-'
    }

    public class Player
    {
        public string Name { get; set; }
        public Marker Marker { get; set; }

        public Player(string name, Marker marker)
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
