namespace Hermite
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Polynominal
    {
        private static List<PolynominalElement> Simplify(List<PolynominalElement> polynominal)
        {
            var result = new List<PolynominalElement>();
            foreach (var element in polynominal)
            {
                var temp = result.SingleOrDefault(x => x.Power == element.Power);
                if (temp != null)
                    temp.Multiplier.Add(element.Multiplier);
                else
                    result.Add(element.Clone());                 
            }

            return result.Where(x => !x.Multiplier.Equals(new Fraction {Counter = 0, Denominator = 1})).ToList();
        }

        public static List<PolynominalElement> Add(List<PolynominalElement> polynominal1, List<PolynominalElement> polynominal2)
        {
            var result = new List<PolynominalElement>();
            foreach (var element in polynominal1)
                result.Add(element.Clone());
            foreach (var element in polynominal2)
                result.Add(element.Clone());
            return Simplify(result);
        }

        public static List<PolynominalElement> Multiply(List<PolynominalElement> polynominal1, List<PolynominalElement> polynominal2)
        {
            var result = new List<PolynominalElement>();
            foreach(var element1 in polynominal1)
            {
                foreach(var element2 in polynominal2)
                {
                    var multiplier = element1.Multiplier.Clone();
                    multiplier.Multiply(element2.Multiplier);
                    result.Add(new PolynominalElement { Multiplier = multiplier, Power = element1.Power + element2.Power });
                }
            }
            return Simplify(result);
        }

        public static List<PolynominalElement> Multiply(List<PolynominalElement> polynominal, Fraction fraction)
        {
            var result = new List<PolynominalElement>();
            foreach (var element in polynominal)
            {
                var multiplier = element.Multiplier.Clone();
                multiplier.Multiply(fraction);
                result.Add(new PolynominalElement { Multiplier = multiplier, Power = element.Power });
            }

            return result;
        }
    }
}
