namespace Hermite
{
    using System.Collections.Generic;
    using System.Linq;

    public static class PolynominalIntegral
    {
        public static List<PolynominalElement> UnsignedIntegral(List<PolynominalElement> polynominal)
        {
            return polynominal.Select(element => element.Power == 0 ? NumberCase(element) : UnknownCase(element)).ToList();
        }

        public static Fraction SignedIntegral(List<PolynominalElement> polynominal, int a, int b)
        {
            var sumFracA = new Fraction { Counter = 0, Denominator = 1 };
            var sumFracB = new Fraction { Counter = 0, Denominator = 1 };

            foreach (var element in polynominal)
            {
                var fracA = new Fraction { Counter = a, Denominator = 1 };
                fracA.Power(element.Power);
                fracA.Multiply(element.Multiplier);
                sumFracA.Add(fracA);

                var fracB = new Fraction { Counter = b, Denominator = 1 };
                fracB.Power(element.Power);
                fracB.Multiply(element.Multiplier);
                sumFracB.Add(fracB);
            }

            sumFracA.Substract(sumFracB);
            return sumFracA.Clone();
        }

        private static PolynominalElement NumberCase(PolynominalElement element)
        {
            return new PolynominalElement { Multiplier = element.Multiplier.Clone(), Power = element.Power + 1 };
        }

        private static PolynominalElement UnknownCase(PolynominalElement element)
        {
            var multiplier = element.Multiplier.Clone();
            multiplier.Multiply(new Fraction { Counter = 1, Denominator = element.Power + 1 });
            return new PolynominalElement { Multiplier = multiplier, Power = element.Power + 1 };
        }
    }
}
