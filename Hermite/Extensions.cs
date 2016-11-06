namespace Hermite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static Fraction[] CloneArray(this Fraction[] fractions)
        {
            if (fractions == null || fractions.Length == 0) return null;
            var p = new Fraction[fractions.Length];
            for (var i = 0; i < fractions.Length; i++)
            {
                p[i] = fractions[i].Clone();
            }

            return p;
        }

        public static Fraction[] CloneArray(this Fraction[] fractions, int start, int count)
        {
            if (fractions == null || fractions.Length == 0) return null;
            var p = new Fraction[count];
            for (var i = start; i < start + count; i++)
            {
                p[i - start] = fractions[i].Clone();
            }

            return p;
        }

        public static List<DifferenceQuotient> ThrowDuplicates(this List<DifferenceQuotient> differenceQuotientList)
        {
            var result = new List<DifferenceQuotient>();
            foreach (var dq in differenceQuotientList)
            {
                var found = result.Any(temp => temp.Equals(dq));
                if (!found) result.Add(dq.Clone());
            }

            return result;
        }

        public static string PolynominalToString(this List<PolynominalElement> polynominal)
        {
            var poly = polynominal.Select(elem => elem.Clone()).OrderByDescending(x => x.Power).ToList();
            var result = string.Empty;
            foreach (var element in poly)
            {
                if (element.Multiplier.Counter < 0)
                {
                    result += " - ";
                    element.Multiplier.Counter *= -1;
                    result += element.ToString();
                }
                else
                {
                    result += " + ";
                    result += element.ToString();
                }
            }
            result = result.Remove(0, 3);
            return result;
        }
    }
}
