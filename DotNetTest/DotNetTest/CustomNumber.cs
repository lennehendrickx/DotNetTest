namespace DotNetTest
{
    public class CustomNumber
    {
        public int Number { get; }

        public CustomNumber(int i)
        {
            Number = i;
        }

        public static CustomNumber operator +(CustomNumber a, CustomNumber b)
        {
            return new CustomNumber(a.Number + b.Number);
        }

        protected bool Equals(CustomNumber other)
        {
            return Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomNumber) obj);
        }

        public override int GetHashCode()
        {
            return Number;
        }

        public static bool operator ==(CustomNumber left, CustomNumber right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CustomNumber left, CustomNumber right)
        {
            return !Equals(left, right);
        }

        public static void AddWithRef(ref int number, int numberToAdd)
        {
            number = number + numberToAdd;
        }

        public static void Add(int number, int numberToAdd)
        {
            number = number + numberToAdd;
        }

        public static void AddWithRef(ref CustomNumber number, int numberToAdd)
        {
            number = new CustomNumber(number.Number + numberToAdd);
        }

        public static void Add(CustomNumber number, int numberToAdd)
        {
            number = new CustomNumber(number.Number + numberToAdd);
        }

        public static implicit operator int(CustomNumber number)
        {
            return number.Number;
        }

        public static int AddDefinedWithALamba(int number, int anotherNumber) => number + anotherNumber;
    }
}