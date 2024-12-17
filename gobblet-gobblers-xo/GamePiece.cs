namespace gobblet_gobblers_xo
{
    public class GamePiece
    {
        public string ImageSource { get; set; }
        public int Weight { get; set; }
        public bool IsBlue { get; set; }

        public GamePiece(string imageSource, int weight, bool isBlue)
        {
            ImageSource = imageSource;
            Weight = weight;
            IsBlue = isBlue;
        }
    }
}