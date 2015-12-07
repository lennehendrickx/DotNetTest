namespace DotNetTest.Birds
{
    public class Bird
    {
        public const string Birdsound = "tsjilp";
        public const string BirdColor = "a typical bird color";

        public Bird()
        {
        }

        private string Name { get; } = "Rudy";

        public Bird(string name)
        {
            this.Name = name;
        }

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