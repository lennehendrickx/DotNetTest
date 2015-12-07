namespace DotNetTest.Temperature
{
    public class Fahrenheit
    {
        public float Degrees { get; }

        public Fahrenheit(float degrees)
        {
            Degrees = degrees;
        }

        public static implicit operator Fahrenheit(Celsius cel)
        {
            return new Fahrenheit((cel.Degrees * 9.0f / 5.0f) + 32);
        }
    }
}