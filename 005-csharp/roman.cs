using System.Linq;
using Xunit;
using Xunit.Extensions;
 
namespace Dojo
{
    public class Romes
    {
        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "I")]
        [InlineData(3, "III")]
        [InlineData(2, "II")]
        [InlineData(4, "IV")]
        [InlineData(5, "V")]
        [InlineData(9, "IX")]
        [InlineData(10, "X")]
        [InlineData(20, "XX")]
        [InlineData(40, "XL")]
        [InlineData(50, "L")]
        [InlineData(90, "XC")]
        [InlineData(100, "C")]
        [InlineData(400, "CD")]
        [InlineData(500, "D")]
        [InlineData(900, "CM")]
        [InlineData(1000, "M")]
        [InlineData(1945, "MCMXLV")]
        public void GivenIntegeReturnsRomanNumber(int number, string roman)
        {
            Assert.Equal(roman, ToRomans(number));
        }
 
        private static readonly ArabicRomanPair[] romanNumberPairs = new[]
                                         {
                                             new ArabicRomanPair {ArabicNumber = 1000, RomanNumber = "M"},
                                             new ArabicRomanPair {ArabicNumber = 900, RomanNumber = "CM"},
                                             new ArabicRomanPair {ArabicNumber = 500, RomanNumber = "D"},
                                             new ArabicRomanPair {ArabicNumber = 400, RomanNumber = "CD"},
                                             new ArabicRomanPair {ArabicNumber = 100, RomanNumber = "C"},
                                             new ArabicRomanPair {ArabicNumber = 90, RomanNumber = "XC"},
                                             new ArabicRomanPair {ArabicNumber = 50, RomanNumber = "L"},
                                             new ArabicRomanPair {ArabicNumber = 40, RomanNumber = "XL"},
                                             new ArabicRomanPair {ArabicNumber = 10, RomanNumber = "X"},
                                             new ArabicRomanPair {ArabicNumber = 9, RomanNumber = "IX"},
                                             new ArabicRomanPair {ArabicNumber = 5, RomanNumber = "V"},
                                             new ArabicRomanPair {ArabicNumber = 4, RomanNumber = "IV"},
                                             new ArabicRomanPair {ArabicNumber = 1, RomanNumber = "I"}
                                         };
 
        private string ToRomans(int arabic)
        {
            var seed = ArabicRomanPair.Create(arabic, string.Empty);
            var result = romanNumberPairs.Aggregate(seed, ApplyMapping);
            return result.RomanNumber;
        }
 
        private static ArabicRomanPair ApplyMapping(ArabicRomanPair input, ArabicRomanPair pair)
        {
            while (input.ArabicNumber >= pair.ArabicNumber)
            {
                input.RomanNumber += pair.RomanNumber;
                input.ArabicNumber -= pair.ArabicNumber;
            }
 
            return ArabicRomanPair.Create(input.ArabicNumber, input.RomanNumber);
        }
    }   
}