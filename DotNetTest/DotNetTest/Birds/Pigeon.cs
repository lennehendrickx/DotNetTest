namespace DotNetTest.Birds
{
    public class Pigeon : Bird
    {
        public const string PigeonSound = "roekoe";
        public const string PigeonColor = "Gray";

        public new string MakeSound()
        {
            return PigeonSound;
        }


        public string MakeBirdSound()
        {
            return base.MakeSound();
        }

        public override string GetColor()
        {
            return PigeonColor;
        }

        public string GetBirdColor()
        {
            return base.GetColor();
        }
    }
}