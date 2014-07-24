using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Extensions;
 
namespace Dojo
{
    public struct Input
    {
        public readonly char[] Separators;
        public readonly string Numbers;
 
        Input(char[] separators, string numbers)
        {
            Separators = separators;
            Numbers = numbers;
        }
 
        public static Input Parse(string input)
        {
            var separators = new[] { ',', '\n' };
 
            var header = new Regex(@"//(.)\n");
            var match = header.Match(input);
 
            if (match.Success)
            {
                separators = match.Groups[0].Value.ToArray();
            }
 
            var numbers = header.Replace(input, string.Empty);
 
            return new Input(separators, numbers);
        }
 
    }
 
    public class StringCalculatorTest
    {
        private const string NegativeNumbers = "negative numbers: {0}";
 
        private int Add(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            if (text.Contains("-"))
            {
                text = text.Split(',').First();
                throw new Exception(String.Format(NegativeNumbers, text));
            }
 
            var input = Input.Parse(text);
 
            return input.Numbers.Split(input.Separators)
                .Select(Int32.Parse)
                .Sum();
        }
 
        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        [InlineData("1,2", 3)]
        [InlineData("1,3", 4)]
        [InlineData("1,2,3,4,5", 15)]
        [InlineData("1\n2", 3)]
        [InlineData("1\n2,3", 6)]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//;\n1;3", 4)]
        [InlineData("//[\n1[3", 4)]
        [InlineData("//]\n1]3", 4)]
        [InlineData("//|\n1|3", 4)]
        public void GivenInputShouldReturnExpected(string input, int expected)
        {
            Assert.Equal(expected, Add(input));
        }
 
        [Fact]
        public void GivenNegativeNumberShouldThrowException()
        {
            Assert.Throws(typeof(Exception), () => Add("-1"));
        }
 
        [Theory]
        [InlineData("-1", "-1")]
        [InlineData("-2", "-2")]
        [InlineData("-1,2", "-1")]
        public void GivenNegativeNumbersShouldThrowException(string input, string expected)
        {
            var ex = Assert.Throws(typeof(Exception), () => Add(input));
            Assert.Equal("negative numbers: " + expected, ex.Message);
        }
 
       
    }
}