using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FindAlphabetOrder
{
    [TestClass]
    public class FindAlphabetOrderUnitTests
    {
        [TestMethod]
        public void GivenValidWordListOfWordsWithOneChar_FindAlphabetOrder_ShouldReturnExactSequence()
        {
            var wordList = new List<string> { "e", "d" };

            var result = new Alphabet().FindAlphabetOrder(wordList);

            Assert.IsTrue(result.SequenceEqual(new List<char> { 'e', 'd' }));
        }

        [TestMethod]
        public void GivenWordListOfDifferentLength_FindAlphabetOrder_ShouldReturnValidSequence()
        {
            var wordList = new List<string> { "a", "aa" };

            var result = new Alphabet().FindAlphabetOrder(wordList);

            Assert.IsTrue(result.SequenceEqual(new List<char> { 'a' }));
        }

        [TestMethod]
        public void GivenValidWordListOfSameLength_FindAlphabetOrder_ShouldReturnValidSequence()
        {
            var wordList = new List<string> { "bca", "aaa", "acb" };

            var result = new Alphabet().FindAlphabetOrder(wordList);

            Assert.IsTrue(result.SequenceEqual(new List<char> { 'b', 'a', 'c' }));
        }

        [TestMethod]
        public void GivenInvalidWordListWithDanglingHead_FindAlphabetOrder_ShouldThrowException()
        {
            var wordList = new List<string> { "aa", "ab", "abc" };

            Assert.ThrowsException<ArgumentException>(() => new Alphabet().FindAlphabetOrder(wordList));
        }

        [TestMethod]
        public void GivenNullOrEmptyString_FindAlphabetOrder_ShouldThrowException()
        {
            var wordList = new List<string> { null, "ab", "abc" };

            Assert.ThrowsException<NullReferenceException>(() => new Alphabet().FindAlphabetOrder(wordList));
        }

        [TestMethod]
        public void GivenValidComplexWordList_FindAlphabetOrder_ShouldReturnValidSequence()
        {
            var wordList = new List<string> { "abcdefg", "bcdefg", "cdefg", "defg", "efg", "fg", "g" };

            var result = new Alphabet().FindAlphabetOrder(wordList);

            Assert.IsTrue(result.SequenceEqual(new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' }));
        }
    }
}
