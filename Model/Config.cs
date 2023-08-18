namespace EverythingSearch.Model
{
    public enum ThemeColor
    {
        White,
        Black,
    }

    public class Config
    {
        public double Scale { get; set; }               = 1;
        public ThemeColor ThemeColor { get; set; }      = ThemeColor.Black;
        public double Transparency { get; set; }        = 0.8;
    }
}
