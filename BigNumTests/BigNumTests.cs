using BigNumLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BigNumTests
{
    [TestClass]
    public class BigNumTests
    {
        #region Constructor Tests
        [TestMethod]
        public void Constructor_Empty_BigNumberZero()
        {
            var num = new BigNumber();
            var expected = new int[1];
            expected[0] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Constructor_PositiveNumberInt_HasCorrectNumber()
        {
            var num = new BigNumber(1);
            var expected = new int[1];
            expected[0] = 1;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Constructor_NegativeNumberInt_HasCorrectNumber()
        {
            var num = new BigNumber(-1);
            var expected = new int[1];
            expected[0] = 1;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Constructor_ZeroInt_HasCorrectNumber()
        {
            var num = new BigNumber(0);
            var expected = new int[1];
            expected[0] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Constructor_EmptyString_HasCorrectNumber()
        {
            var num = new BigNumber("");
            var expected = new int[1];
            expected[0] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Constructor_PositiveSmallString_HasCorrectNumber()
        {
            var num = new BigNumber("1");
            var expected = new int[1];
            expected[0] = 1;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Constructor_NegativeSmallString_HasCorrectNumber()
        {
            var num = new BigNumber("-1");
            var expected = new int[1];
            expected[0] = 1;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Constructor_PositiveLargeStringNoExtra_HasCorrectNumber()
        {
            var num = new BigNumber("111111111111111111");
            var expected = new int[2];
            expected[0] = 111111111;
            expected[1] = 111111111;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Constructor_NegativeLargeStringNoExtra_HasCorrectNumber()
        {
            var num = new BigNumber("-111111111111111111");
            var expected = new int[2];
            expected[0] = 111111111;
            expected[1] = 111111111;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Constructor_PositiveLargeStringHasExtra_HasCorrectNumber()
        {
            var num = new BigNumber("1111111111");
            var expected = new int[2];
            expected[0] = 1;
            expected[1] = 111111111;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Constructor_NegativeLargeStringHasExtra_HasCorrectNumber()
        {
            var num = new BigNumber("-1111111111");
            var expected = new int[2];
            expected[0] = 1;
            expected[1] = 111111111;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }
        #endregion

        #region ToString Tests
        [TestMethod]
        public void ToString_PositiveSmallNumber_ReturnsCorrectString()
        {
            var num = new BigNumber(1);
            Assert.AreEqual("1", num.ToString());
        }

        [TestMethod]
        public void ToString_NegativeSmallNumber_ReturnsCorrectString()
        {
            var num = new BigNumber(-1);
            Assert.AreEqual("-1", num.ToString());
        }

        [TestMethod]
        public void ToString_PositiveLargeNumber_ReturnsCorrectString()
        {
            var num = new BigNumber("12345678901234567890");
            Assert.AreEqual("12345678901234567890", num.ToString());
        }

        [TestMethod]
        public void ToString_NegativeLargeNumber_ReturnsCorrectString()
        {
            var num = new BigNumber("-12345678901234567890");
            Assert.AreEqual("-12345678901234567890", num.ToString());
        }
        #endregion

        #region Addition Tests
        [TestMethod]
        public void Addition_TwoSmallPositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(1);
            var n2 = new BigNumber(2);
            var num = n1 + n2;
            var expected = new int[1];
            expected[0] = 3;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }
        
        [TestMethod]
        public void Addition_TwoLargePositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("5500000000");
            var n2 = new BigNumber("5500000000");
            var num = n1 + n2;
            var expected = new int[2];
            expected[0] = 11;
            expected[1] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Addition_TwoSmallNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(-1);
            var n2 = new BigNumber(-2);
            var num = n1 + n2;
            var expected = new int[1];
            expected[0] = 3;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Addition_TwoLargeNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-5500000000");
            var n2 = new BigNumber("-5500000000");
            var num = n1 + n2;
            var expected = new int[2];
            expected[0] = 11;
            expected[1] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Addition_TwoSmallDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(-1);
            var n2 = new BigNumber(2);
            var num = n1 + n2;
            var expected = new int[1];
            expected[0] = 1;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Addition_TwoLargeDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-4500000000");
            var n2 = new BigNumber("5500000000");
            var num = n1 + n2;
            var expected = new int[2];
            expected[0] = 1;
            expected[1] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Addition_TwoLargeDifferentSizeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("9999999999");
            var n2 = new BigNumber("999999999999999999");
            var num = n1 + n2;
            var expected = new int[3];
            expected[0] = 1;
            expected[1] = 000000009;
            expected[2] = 999999998;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }
        #endregion

        #region Subtraction Tests
        [TestMethod]
        public void Subtraction_TwoSmallPositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(2);
            var n2 = new BigNumber(1);
            var num = n1 - n2;
            var expected = new int[1];
            expected[0] = 1;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Subtraction_TwoLargePositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("7500000000");
            var n2 = new BigNumber("5500000000");
            var num = n1 - n2;
            var expected = new int[2];
            expected[0] = 2;
            expected[1] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Subtraction_TwoSmallNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(-2);
            var n2 = new BigNumber(-1);
            var num = n1 - n2;
            var expected = new int[1];
            expected[0] = 1;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Subtraction_TwoLargeNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-7500000000");
            var n2 = new BigNumber("-5500000000");
            var num = n1 - n2;
            var expected = new int[2];
            expected[0] = 2;
            expected[1] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Subtraction_TwoSmallDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(-1);
            var n2 = new BigNumber(2);
            var num = n1 - n2;
            var expected = new int[1];
            expected[0] = 3;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Subtraction_TwoLargeDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-4500000000");
            var n2 = new BigNumber("5500000000");
            var num = n1 - n2;
            var expected = new int[2];
            expected[0] = 10;
            expected[1] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Subtraction_TwoLargeDifferentSizeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("9999999999");
            var n2 = new BigNumber("999999999999999999");
            var num = n1 - n2;
            var expected = new int[2];
            expected[0] = 999999990;
            expected[1] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }
        #endregion

        #region Comparison Tests
        [TestMethod]
        public void GreaterThan_SmallPositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("2");
            var n2 = new BigNumber("1");
            Assert.IsTrue(n1 > n2);
        }

        [TestMethod]
        public void GreaterThan_SmallNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-1");
            var n2 = new BigNumber("-2");
            Assert.IsTrue(n1 > n2);
        }

        [TestMethod]
        public void GreaterThan_SmallDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("2");
            var n2 = new BigNumber("-1");
            Assert.IsTrue(n1 > n2);
        }

        [TestMethod]
        public void GreaterThan_LargePositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("2222222222222222");
            var n2 = new BigNumber("1111111111111111");
            Assert.IsTrue(n1 > n2);
        }

        [TestMethod]
        public void GreaterThan_LargeNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-1111111111111111");
            var n2 = new BigNumber("-2222222222222222");
            Assert.IsTrue(n1 > n2);
        }

        [TestMethod]
        public void GreaterThan_LargeDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("2222222222222222");
            var n2 = new BigNumber("-1111111111111111");
            Assert.IsTrue(n1 > n2);
        }

        [TestMethod]
        public void LessThan_SmallPositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("1");
            var n2 = new BigNumber("2");
            Assert.IsTrue(n1 < n2);
        }

        [TestMethod]
        public void LessThan_SmallNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-2");
            var n2 = new BigNumber("-1");
            Assert.IsTrue(n1 < n2);
        }

        [TestMethod]
        public void LessThan_SmallDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-1");
            var n2 = new BigNumber("2");
            Assert.IsTrue(n1 < n2);
        }

        [TestMethod]
        public void LessThan_LargePositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("1111111111111111");
            var n2 = new BigNumber("2222222222222222");
            Assert.IsTrue(n1 < n2);
        }

        [TestMethod]
        public void LessThan_LargeNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-2222222222222222");
            var n2 = new BigNumber("-1111111111111111");
            Assert.IsTrue(n1 < n2);
        }

        [TestMethod]
        public void LessThan_LargeDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-1111111111111111");
            var n2 = new BigNumber("2222222222222222");
            Assert.IsTrue(n1 < n2);
        }
        #endregion

        #region Multiplication Tests
        [TestMethod]
        public void Multiplication_TwoSmallPositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(1);
            var n2 = new BigNumber(2);
            var num = n1 * n2;
            var expected = new int[1];
            expected[0] = 2;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Multiplication_TwoLargePositiveNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("5500000000");
            var n2 = new BigNumber("5500000000");
            var num = n1 * n2;
            var expected = new int[2];
            expected[0] = 30;
            expected[1] = 250000000;
            expected[2] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Multiplication_TwoSmallNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(-1);
            var n2 = new BigNumber(-2);
            var num = n1 * n2;
            var expected = new int[1];
            expected[0] = 2;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Multiplication_TwoLargeNegativeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-5500000000");
            var n2 = new BigNumber("-5500000000");
            var num = n1 * n2;
            var expected = new int[3];
            expected[0] = 30;
            expected[1] = 250000000;
            expected[2] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }

        [TestMethod]
        public void Multiplication_TwoSmallDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber(-1);
            var n2 = new BigNumber(2);
            var num = n1 * n2;
            var expected = new int[1];
            expected[0] = 2;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Multiplication_TwoLargeDifferentSignNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("-5500000000");
            var n2 = new BigNumber("5500000000");
            var num = n1 + n2;
            var expected = new int[3];
            expected[0] = 30;
            expected[1] = 250000000;
            expected[2] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsFalse(num.Positive);
        }

        [TestMethod]
        public void Multiplication_TwoLargeDifferentSizeNumbers_ReturnsCorrectResult()
        {
            var n1 = new BigNumber("100000000");
            var n2 = new BigNumber("100000000000000000");
            var num = n1 * n2;
            var expected = new int[3];
            expected[0] = 100000000;
            expected[1] = 0;
            expected[2] = 0;
            CollectionAssert.AreEqual(expected, num.NumData);
            Assert.IsTrue(num.Positive);
        }
        #endregion
    }
}  
