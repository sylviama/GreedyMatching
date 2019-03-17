using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreedyMatching2;
using System.Collections.Generic;

namespace GreedyMatchingUnitTest
{
    [TestClass]
    public class GreedyMatchClassUnitTest
    {
        [TestMethod]
        //give input from example
        public void TestMethod1()
        {
            //arrange
            string s1 = "all is well";
            string s2 = "ell that en";
            string s3 = "hat end";
            string s4 = "t ends well";

            List<string> inputStringList = new List<string> { s1, s2, s3, s4 };

            //act
            GreedyMatchClass greedyMatch = new GreedyMatchClass();
            string actual = greedyMatch.GreedyMatching(inputStringList);

            string expected = "all is well that ends well";

            //assert
            Assert.AreEqual(actual.ToUpper(), expected.ToUpper());
        }

        //give input with one string contains another string
        [TestMethod]
        public void StringContainTest()
        {
            //arrange
            string s1 = "all is well";
            string s2 = "is w";
            List<string> inputStringList = new List<string> { s1, s2 };

            //act
            GreedyMatchClass greedyMatch = new GreedyMatchClass();
            string actual = greedyMatch.GreedyMatching(inputStringList);

            string expected = s1;

            //assert
            Assert.AreEqual(actual.ToUpper(), expected.ToUpper());
        }

        //give strings list with no matching with each other
        [TestMethod]
        public void NoMatchingTest()
        {
            //arrange
            string s1 = "all";
            string s2 = "is";
            string s3 = "well";

            List<string> inputStringList = new List<string> { s1, s2, s3};

            //act
            GreedyMatchClass greedyMatch = new GreedyMatchClass();
            string actual = greedyMatch.GreedyMatching(inputStringList).ToUpper();

            string expected1 = "alliswell".ToUpper();
            string expected2 = "allwellis".ToUpper();
            string expected3 = "isallwell".ToUpper();
            string expected4 = "iswellall".ToUpper();
            string expected5 = "wellallis".ToUpper();
            string expected6 = "wellisall".ToUpper();

            //assert
            Assert.IsTrue(actual == expected1 || actual == expected2 ||
                          actual == expected3 || actual == expected4 ||
                          actual == expected5 || actual == expected6); 
        }
    }
}
