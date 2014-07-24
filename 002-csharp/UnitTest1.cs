using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
 
namespace Dojo_ach
{
    [TestClass]
    public class UnitTest1
    {
 
        /* Examples:
         * 1 - file header
         *  5 - batch header
         *      6 - entry detail record
         *          7 - entry detail nakej jinej record (musi nasledovat za 6)
         *  8 - batch control
         * 9 - file control
         * 
         * 1,5,6,6,7,8,9,9
         * 1,5,6,8,5,6,8,9
         */
 
        private int[] validFile = new[] {1, 5, 6, 7, 8, 9};
 
        [TestMethod]
        public void InvalidWhenFileDoesntStartWithOne()
        {
            var invalidFile = 
                Invalidate(validFile, prefixes => { prefixes[0] = 2; });
            
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void ValidForValidFile()
        {
            Assert.IsTrue(Validate(validFile));
        }
 
        [TestMethod]
        public void InvalidWhenFileDoesNotEndWithNine()
        {
            var invalidFile =
                Invalidate(validFile, prefixes => { prefixes[prefixes.Length - 1] = 8; });
            
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void InvalidWhenThereIsOtherCharacterThenNineAfterANine()
        {
            var invalidFile =
                Invalidate(validFile, prefixes => { prefixes[2] = 9; });
            
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void InvalidWithoutBatch()
        {
            Assert.IsFalse(Validate(new[]{1,9}));
        }
 
        [TestMethod]
        public void InvalidWhenBatchesAreNested()
        {
            var invalidFile = new[] { 1, 5, 6, 5, 6, 8, 8, 9 };
 
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void InvalidWhenContainsNotFinishedBatchHeader()
        {
            var invalidFile = new [] {1,5,6,8,5,6,9};
                
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void InvalidWhenSixOutsideBatch()
        {
            var invalidFile = new[] { 1, 5, 6, 8, 6, 9 };
 
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void InvalidWhenSevenOutsideBatch()
        {
            var invalidFile = new[] { 1, 5, 6, 8, 7, 9 };
 
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void InvalidWhenContainsEmptyBatch()
        {
            var invalidFile = new[] { 1, 5, 8, 9 };
 
            Assert.IsFalse(Validate(invalidFile));
        }
 
        [TestMethod]
        public void InvalidWhenContainsOnlyAddendaRecord()
        {
            var invalidFile = new[] { 1, 5, 7, 8, 9 };
 
            Assert.IsFalse(Validate(invalidFile));
        }
 
 
        private int[] Invalidate(int[] valid, Action<int[]> invalidationCallback)
        {
            var clone = (int[]) valid.Clone();
            invalidationCallback(clone);
            return clone;
        }
 
        private bool Validate(IEnumerable<int> prefixes)
        {
            var str = string.Join("", prefixes);
            var regexp = new Regex("1(5(6+7*)+8)+9+");
            return regexp.Match(str).Success;
        }
 
        private bool ValidateTooComplex(IEnumerable<int> prefixes)
        {
            bool hasBatch = false;
            var batchHasContent = false;
            var batchHasEntry = false;
            var inBatch = false;
            var inFile = false;
 
            foreach (var prefix in prefixes)
            {
                bool isEntry = (prefix == 6);
                bool content = (isEntry || prefix == 7);
 
                if (inFile == false && prefix != 1) return false;
                if (prefix == 1)
                {
                    inFile = true;
                    continue;
                }
 
                if (inFile)
                {
                    if (prefix == 5)
                    {
                        batchHasContent = false;
                        batchHasEntry = false;
                        if (inBatch) return false;
                        inBatch = true;
                        hasBatch = true;
                        continue;
                    }
 
                    if (inBatch)
                    {
                        if (isEntry) batchHasEntry = true;
                        if (content) batchHasContent = true;
 
                        if (prefix == 8)
                        {
                            inBatch = false;
                            if (!batchHasContent) return false;
                            if (!batchHasEntry) return false;
                            continue;
                        }
 
                        if (content) continue;
 
                        return false;
                    }
 
                    if (prefix == 9)
                    {
                        inFile = false;
                        continue;
                    }
                }
                else
                {
                    
                }
 
                if (prefix != 9) return false;
            }
 
            if (!hasBatch) return false;
            return true;
        }
    }
}