namespace Casino.Models
{
    public class WheelNumber
    {

        public int Number { get; }

        public WheelNumColorEnum Color { get; }

        public bool Selected { get; set; }

        public WheelNumber(int number, WheelNumColorEnum color)
        {
            Number = number;
            Color = color;
        }
    }
}
