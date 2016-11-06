namespace Hermite
{
    using System;

    public class Fraction : NotifyBase
    {
        private int denominator;
        public int Denominator
        {
            get
            {
                return this.denominator;
            }

            set
            {
                this.denominator = value;
                this.OnPropertyChanged(() => this.Denominator);
            }
        }

        private int counter;
        public int Counter
        {
            get
            {
                return this.counter;
            }

            set
            {
                this.counter = value;
                this.OnPropertyChanged(() => this.Counter);
            }
        }

        public double ToDouble()
        {
            return this.Counter / (double)this.Denominator;
        }

        public Fraction Clone()
        {
            return new Fraction { Denominator = this.Denominator, Counter = this.Counter };
        }

        public void Multiply(Fraction fraction)
        {
            this.Denominator *= fraction.Denominator;
            this.Counter *= fraction.Counter;
            this.Simplify();
        }

        public void Add(Fraction fraction)
        {
            if (this.Denominator == fraction.Denominator)
            {
                this.Counter += fraction.Counter;
            }
            else
            {
                this.Counter = (this.Counter * fraction.Denominator) + (fraction.Counter * this.Denominator);
                this.Denominator *= fraction.Denominator;
            }

            this.Simplify();
        }

        public void Substract(Fraction fraction)
        {
            if (this.Denominator == fraction.Denominator)
            {
                this.Counter -= fraction.Counter;
                this.Simplify();
            }
            else
            {
                this.Counter = (this.Counter * fraction.Denominator) - (fraction.Counter * this.Denominator);
                this.Denominator *= fraction.Denominator;
            }

            this.Simplify();
        }

        public void Divide(Fraction fraction)
        {
            this.Multiply(new Fraction { Counter = fraction.Denominator, Denominator = fraction.Counter });
        }

        public void Power(int power)
        {
            var temp = this.Clone();
            for (int i = 0; i < power - 1; i++)
                this.Multiply(temp);
        }

        public new string ToString()
        {
            if (this.Denominator == 1)
                return this.Counter.ToString();
            else
                return this.Counter.ToString() + "/" + this.Denominator.ToString();
        }

        public new bool Equals(object obj)
        {
            var fraction = obj as Fraction;
            return fraction != null && fraction.Counter == this.Counter && fraction.Denominator == this.Denominator;
        }

        public new int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void Simplify()
        {
            var nwd = this.NWD();
            this.Counter /= nwd;
            this.Denominator /= nwd;
            if (this.Denominator >= 0) return;
            this.Counter *= -1;
            this.Denominator *= -1;
        }

        private int NWD()
        {
            var a = Math.Abs(this.Counter);
            var b = Math.Abs(this.Denominator);
            while (a != 0 && b != 0 && a != b)
            {
                if (a > b)
                    a -= b;
                else
                    b -= a;
            }

            return a == 0 ? 1 : a;
        }
    }
}
