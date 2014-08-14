using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodingDojo2048
{
    [TestFixture]
    public class Class1
    {
        // 4x4 grid
        // initialy 2 cells with value 2 or 4
        // after each round add one cell with value 2 or 4
        // moves:
        // row to left
        // row to right
        // column to up
        // column to down
        // bunky se pricapi
        //   - two cells with same value merge and sums with direction from wall

        [Theory]
        [TestCase(new[] { 0, 0, 0, 0 }, new[] { 0, 0, 0, 0 })]
        [TestCase(new[] { 0, 0, 0, 2 }, new[] { 0, 0, 0, 2 })]
        [TestCase(new[] { 0, 2, 0, 0 }, new[] { 0, 0, 0, 2 })]
        [TestCase(new[] { 2, 0, 0, 0 }, new[] { 0, 0, 0, 2 })]
        [TestCase(new[] { 2, 0, 2, 0 }, new[] { 0, 0, 0, 4 })]
        [TestCase(new[] { 2, 0, 2, 2 }, new[] { 0, 0, 2, 4 })]
        [TestCase(new[] { 2, 2, 2, 2 }, new[] { 0, 0, 4, 4 })]
        [TestCase(new[] { 4, 4, 2, 2 }, new[] { 0, 0, 8, 4 })]
        public void WhenEvaluateVectorToVector(int[] input, int[] expected)
        {
            var result = Evaluate(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        private int[] Evaluate(int[] values)
        {
            var nonZeroValues = values.Where(value => value != 0).ToList();
            var merged = Merge(nonZeroValues);
            return CompleteWithZeros(merged).ToArray();
        }

        private IEnumerable<int> Merge(List<int> nonZeroValues)
        {
            var merged = new List<int>();

            for (int i = nonZeroValues.Count() - 1; i >= 0; i--)
            {
                var current = nonZeroValues[i];
                var previous = (i == 0) ? 0 : nonZeroValues[i - 1];
                if (current == previous)
                {
                    merged.Add(current + previous);
                    i--;
                }
                else
                {
                    merged.Add(current);
                }
            }

            merged.Reverse();
            return merged;
        }

        private static IEnumerable<int> CompleteWithZeros(IEnumerable<int> merged)
        {
            return Enumerable.Repeat(0, 4 - merged.Count()).Concat(merged);
        }

    }
}
