namespace Hermite
{
    public class PolynominalElement : NotifyBase
    {
        private Fraction multiplier;
        public Fraction Multiplier
        {
            get
            {
                return this.multiplier;
            }

            set
            {
                this.multiplier = value;
                this.OnPropertyChanged(() => this.Multiplier);
            }
        }

        private int power;
        public int Power
        {
            get
            {
                return this.power;
            }

            set
            {
                this.power = value;
                this.OnPropertyChanged(() => this.Power);
            }
        }

        public override string ToString()
        {
            switch (this.Power)
            {
                case 0:
                    return this.Multiplier.ToString();
                case 1:
                    return (this.Multiplier.Counter == 1 && this.Multiplier.Denominator == 1 ? string.Empty : this.Multiplier.ToString()) + "x";
                default:
                    return (this.Multiplier.Counter == 1 && this.Multiplier.Denominator == 1 ? string.Empty : this.Multiplier.ToString()) + "x^" + this.Power;
            }
        }

        public PolynominalElement Clone()
        {
            return new PolynominalElement { Multiplier = this.Multiplier.Clone(), Power = this.Power };
        }

        public new bool Equals(object obj)
        {
            var polynominalElement = obj as PolynominalElement;
            if (polynominalElement != null && polynominalElement.Power == this.Power && polynominalElement.Multiplier.Equals(this.Multiplier))
                return true;
            return false;
        }

        public new int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
