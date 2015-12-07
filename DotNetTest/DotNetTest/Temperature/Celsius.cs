using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTest.Temperature
{
    public class Celsius
    {
        public float Degrees { get; }

        public Celsius(float degrees)
        {
            this.Degrees = degrees;
        }

        public static explicit operator Celsius(Fahrenheit fahr)
        {
            return new Celsius((5.0f / 9.0f) * (fahr.Degrees - 32));
        }

    }
}
