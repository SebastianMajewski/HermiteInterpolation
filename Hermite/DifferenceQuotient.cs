namespace Hermite
{
    using System.Linq;

    public class DifferenceQuotient : NotifyBase
    {
        private Fraction result;
        private Fraction[] points;

        public Fraction Result
        {
            get
            {
                return this.result;
            }

            set
            {
                this.result = value;
                this.OnPropertyChanged(() => this.Result);
            }
        }

        public Fraction[] Points
        {
            get
            {
                return this.points;
            }

            set
            {
                this.points = value;
                this.OnPropertyChanged(() => this.Points);
            }
        }  

        public DifferenceQuotient Clone()
        {
            if (this.points == null || this.points.Length == 0) return new DifferenceQuotient { Result = this.Result.Clone(), Points = null };
            var p = new Fraction[this.points.Length];
            for (var i = 0; i < this.points.Length; i++)
            {
                p[i] = this.points[i].Clone();
            }
            return new DifferenceQuotient { Result = this.Result?.Clone(), Points = p };
        }

        public new bool Equals(object obj)
        {
            var differenceQuotient = obj as DifferenceQuotient;
            if (differenceQuotient != null && differenceQuotient.Points.Length == this.Points.Length)
            {
                return !differenceQuotient.Points.Where((t, i) => !t.Equals(this.Points[i])).Any();
            }

            return false;
        }

        public new int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
