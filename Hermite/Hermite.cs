namespace Hermite
{
    using System.Collections.Generic;
    using System.Linq;

    public class Hermite
    {
        private readonly Fraction[,] data;
        private readonly List<DifferenceQuotient> differenceQuotients = new List<DifferenceQuotient>();

        public Hermite(Fraction[,] data)
        {
            this.data = data;
        }      

        public List<PolynominalElement> Calculate()
        {
            var pointsList = this.GeneratePoints();
            foreach (var points in pointsList)
            {
                this.CalculateDifferenceQuotients(points);
            }

            var polynominalParts = new List<List<PolynominalElement>>
            {
                new List<PolynominalElement>
                {
                    new PolynominalElement
                    {
                        Multiplier = this.LookForDifferenceQuotient(new[] { this.data[0, 0] }).Result,
                        Power = 0
                    }
                }
            };
            var xarray = pointsList.Last();
            for (var i = 1; i < xarray.Length; i++)
            {
                var polynominal = new List<PolynominalElement>
                {
                    new PolynominalElement
                    {
                        Multiplier = new Fraction { Counter = 1, Denominator = 1 },
                        Power = 0
                    }
                };
                var quotient = new Fraction[i + 1];
                for (var j = 0; j <= i; j++)
                {
                    quotient[j] = xarray[j].Clone();
                }

                for (var j = 0; j < i; j++)
                {                  
                    var sec = xarray[j].Clone();
                    sec.Multiply(new Fraction { Counter = -1, Denominator = 1 });
                    var multiplier = new List<PolynominalElement>
                    {
                        new PolynominalElement
                        {
                            Multiplier = new Fraction { Counter = 1, Denominator = 1 },
                            Power = 1
                        },
                        new PolynominalElement
                        {
                            Multiplier = sec,
                            Power = 0
                        }
                    };
                    polynominal = Polynominal.Multiply(polynominal, multiplier); 
                }

                polynominalParts.Add(Polynominal.Multiply(polynominal, this.LookForDifferenceQuotient(quotient).Result));
            }

            var result = new List<PolynominalElement>();
            return polynominalParts.Aggregate(result, Polynominal.Add);
        }

        private List<Fraction[]> GeneratePoints()
        {
            var result = new List<Fraction[]>();
            for (var i = 1; i <= this.data.GetLength(1) * (this.data.GetLength(0) - 1); i++)
            {
                var points = new Fraction[i];
                var index = 0;
                var actualelement = 0;
                while (index != i)
                {
                    for (var j = 0; index < i && j < (this.data.GetLength(0) - 1); j++, index++)
                    {
                        points[index] = this.data[0, actualelement].Clone();
                    }

                    actualelement++;
                }
                result.Add(points);
            }

            return result;
        } 

        private DifferenceQuotient LookForDifferenceQuotient(Fraction[] points)
        {
            return (from dq in this.differenceQuotients where dq.Equals(new DifferenceQuotient { Points = points.CloneArray() }) select dq.Clone()).FirstOrDefault();
        }

        private DifferenceQuotient CalculateDifferenceQuotients(Fraction[] points)
        {
            var founded = this.LookForDifferenceQuotient(points);
            if (founded != null)
                return founded;

            if (points.Length == 1)
            {
                var index = -1;
                for (var i = 0; i < this.data.GetLength(1); i++)
                {
                    if (!this.data[0, i].Equals(points[0])) continue;
                    index = i;
                    break;
                }

                this.differenceQuotients.Add(new DifferenceQuotient { Result = this.data[1, index].Clone(), Points = points.CloneArray() });
                return new DifferenceQuotient { Result = this.data[1, index].Clone(), Points = points.CloneArray() };
            }

            if (points[0].Equals(points[points.Length - 1]))
            {
                var index = -1;
                for (var i = 0; i < this.data.GetLength(1); i++)
                {
                    if (!this.data[0, i].Equals(points[0])) continue;
                    index = i;
                    break;
                }

                var result = this.data[points.Length, index].Clone();
                result.Divide(new Fraction { Counter = this.Factorial(points.Length - 1), Denominator = 1 });
                this.differenceQuotients.Add(new DifferenceQuotient { Result = result.Clone(), Points = points.CloneArray() });
                return new DifferenceQuotient { Result = result, Points = points.CloneArray() };
            }

            var points1 = points.CloneArray(1, points.Length - 1);
            var points2 = points.CloneArray(0, points.Length - 1);

            var result1 = this.CalculateDifferenceQuotients(points1).Result;
            var result2 = this.CalculateDifferenceQuotients(points2).Result;

            var res = result1.Clone();
            res.Substract(result2);
            var divider = points[points.Length - 1].Clone();
            divider.Substract(points[0]);
            res.Divide(divider);

            this.differenceQuotients.Add(new DifferenceQuotient { Result = res, Points = points.CloneArray() });
            return new DifferenceQuotient { Result = res, Points = points.CloneArray() };
        }

        private int Factorial(int n)
        {
            var result = 1;
            for (var i = 1; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}