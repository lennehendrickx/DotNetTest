namespace DotNetTest.Birds
{
    public class Bird
    {
        public const string Birdsound = "tsjilp";
        public const string BirdColor = "a typical bird color";

        public string MakeSound()
        {
            return Birdsound;
        }

        public virtual string GetColor()
        {
            return BirdColor;
        }
    }
}