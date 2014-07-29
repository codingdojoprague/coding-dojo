using System;
using System.Collections.Generic;
using System.Linq;
 
using Microsoft.VisualStudio.TestTools.UnitTesting;
 
 
namespace StringCalculator
{
 
    public class Seed
    {
        public List<int> Negatives { get; private set; }
        public int Sum { get; private set; }
 
        public Seed()
        {
            this.Negatives = new List<int>();
        }
 
        public Seed(int sum, List<int> negatives)
        {
            this.Sum = sum;
            this.Negatives = negatives;
        }
 
 
        public Seed AddNegative(int num)
        {
            this.Negatives.Add(num);
            return this;
        }
    }
 
    [TestClass]
    public class UnitTest1
    {
 
        private const string PositiveNumber = "1";
        private const int PositiveNumberSum = 1;
 
        private const string SumOfTwoPositiveStringNumbers = "1,2";
        private const int SumOfTwoPositiveStringNumbersSum = 3;
 
        private const string TwoPositiveStringNumbersWithNlSeparator = "12,3";
        private const int SumOfTwoPositiveStringNumbersWithNlSeparator = 6;
 
        private const string InputWithNegativeNumber = "1,-2, -3";
        private const string StringContainsNegativeNumber = "String contains: -2,-3";
 
 
 
        public int Calculate(String input)
        {
 
            string[] list = input.Split(new[] {',',' '}, StringSplitOptions.RemoveEmptyEntries);
            var numbers = list.Select(int.Parse).ToList();
 
            var seed = numbers.Aggregate(
                new Seed(), 
                (s, num) => num > 0 
                    ? new Seed(s.Sum + num, s.Negatives) 
                    : s.AddNegative(num));
            
            if (seed.Negatives.Any())
            {
                var negativeNumbersString = string.Join(",", seed.Negatives);
                throw new Exception(String.Format("String contains: {0}", negativeNumbersString));    
            }
 
            return seed.Sum;
        }
 
        [TestMethod]
        public void ReturnsZeroForEmptyString()
        {
            int result = Calculate(string.Empty);
            Assert.AreEqual(0, result);
        }
 
        [TestMethod]
        public void ReturnNumberForPositiveStringNumber()
        {
            int result = Calculate(PositiveNumber);
            Assert.AreEqual(PositiveNumberSum, result);
        }
 
        [TestMethod]
        public void ReturnsSumOfPositiveStringNumbersWithCommaSeparator()
        {
            int result = Calculate(SumOfTwoPositiveStringNumbers);
            Assert.AreEqual(SumOfTwoPositiveStringNumbersSum, result);
        }
 
        [TestMethod]
        public void ReturnsSumOfPositiveStringNumbersWithNlSeparator()
        {
            int result = Calculate(TwoPositiveStringNumbersWithNlSeparator);
            Assert.AreEqual(SumOfTwoPositiveStringNumbersWithNlSeparator, result);
        }
 
        [TestMethod]
        public void ThrowsExceptionWithNegativesIfInputContainsNegativeNumbers()
        {
            try
            {
                Calculate(InputWithNegativeNumber);
            }
            catch (Exception e)
            {
                Assert.AreEqual(StringContainsNegativeNumber, e.Message);
            }
        }
    }
}
