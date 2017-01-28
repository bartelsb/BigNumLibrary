using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BigNumLibrary
{
    public class BigNumber
    {
        private const int UpperLimitPerBlock = 1000000000;
        private const int MaxDigits = 9;
        private enum OperationTypes
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        };

        internal bool Positive { get; set; }
        internal int[] NumData { get; set; }

        public BigNumber(int num)
        {
            Positive = num >= 0;
            NumData = new [] {Math.Abs(num)};
        }

        public BigNumber() : this(0) {}

        public BigNumber(string num)
        {
            Positive = !num.StartsWith("-");
            var numSize = Positive ? num.Length : num.Length - 1;
            var extra = numSize % MaxDigits;
            var numBlocks = (numSize / MaxDigits > 0
                ? numSize / MaxDigits + (extra > 0 ? 1 : 0) 
                : 1);
            var numStartIndex = num.Length - numSize;
            NumData = new int[numBlocks];

            if (numSize <= 0) return;
            for (var insertIndex = numBlocks - 1; insertIndex > -1; insertIndex--)
            {
                var digitsToRead = (insertIndex > 0 || extra == 0 
                    ? MaxDigits
                    : extra);
                var startIndex = (insertIndex > 0 && extra > 0
                    ? numStartIndex + extra + (insertIndex - 1) * MaxDigits
                    : numStartIndex + insertIndex * MaxDigits) ;
                NumData[insertIndex] = Convert.ToInt32(num.Substring(startIndex, digitsToRead));
            }
        }

        public static bool operator >(BigNumber n1, BigNumber n2)
        {
            if ((n1.Positive && !n2.Positive) || (!n1.Positive && n2.Positive))
            {
                return n1.Positive;
            }
            if (n1.NumData.Length != n2.NumData.Length)
            {
                return n1.NumData.Length > n2.NumData.Length;
            }
            return (n1.NumData[0] > n2.NumData[0] && n1.Positive) || (n1.NumData[0] < n2.NumData[0] && !n1.Positive);
        }

        public static bool operator <(BigNumber n1, BigNumber n2)
        {
            return !(n1 >= n2);
        }

        public static bool operator >=(BigNumber n1, BigNumber n2)
        {
            return n1 == n2 || n1 > n2;
        }

        public static bool operator <=(BigNumber n1, BigNumber n2)
        {
            return !(n1 > n2);
        }

        public static BigNumber operator +(BigNumber n1, BigNumber n2)
        {
            if (n1.Positive && !n2.Positive)
            {
                n2.Positive = true;
                return n1 - n2;
            }
            if (!n1.Positive && n2.Positive)
            {
                n1.Positive = true;
                return n2 - n1;
            }

            var retNum = new BigNumber
            {
                NumData = PerformUnsignedAdd(n1.NumData, n2.NumData),
                Positive = DetermineSign(n1, n2, OperationTypes.Addition)
            };
            return retNum;
        }

        public static BigNumber operator -(BigNumber n1, BigNumber n2)
        {
            if (n1.Positive && !n2.Positive)
            {
                n2.Positive = true;
                return n1 + n2;
            }
            if (!n1.Positive && n2.Positive)
            {
                n2.Positive = false;
                return n2 + n1;
            }

            var difference = new BigNumber
            {
                NumData = new int[DetermineMaxArraySize(n1.NumData, n2.NumData, OperationTypes.Subtraction)],
                Positive = DetermineSign(n1, n2, OperationTypes.Subtraction)
            };

            var subtrahend = (n1 > n2) && n1.Positive || (n1 < n2) && !n1.Positive 
                ? n1 
                : n2;
            var minuend = (n1 == subtrahend) 
                ? n2 
                : n1;
            var carry = 0;
            for (var i = difference.NumData.Length - 1; i > -1; i--)
            {
                var tempSubtrahend = 0;
                var tempMinuend = 0;
                if (i - (difference.NumData.Length - n1.NumData.Length) > -1)
                {
                    tempSubtrahend = subtrahend.NumData[i - (difference.NumData.Length - n1.NumData.Length)];
                }
                if (i - (difference.NumData.Length - n2.NumData.Length) > -1)
                {
                    tempMinuend = minuend.NumData[i - (difference.NumData.Length - n2.NumData.Length)];
                }
                var tempResult = tempSubtrahend - tempMinuend - carry;
                carry = tempResult < 0 
                    ? -1 
                    : 0;
                tempResult = tempResult < 0 
                    ? tempResult + UpperLimitPerBlock 
                    : tempResult;
                
                difference.NumData[i] = tempResult;
            }
            return difference;
        }

        public static BigNumber operator ++(BigNumber n1)
        {
            return n1 + new BigNumber(1);
        }

        public static BigNumber operator --(BigNumber n1)
        {
            return n1 - new BigNumber(1);
        }

        public static BigNumber operator *(BigNumber n1, BigNumber n2)
        {
            var product = new BigNumber();
            var intermediateProducts = new BigNumber[n2.NumData.Length];
            int carry;

            for (var i = n2.NumData.Length-1; i > -1; i--)
            {
                carry = 0;
                var intermediateProduct = new BigNumber
                {
                    NumData = new int[DetermineMaxArraySize(n1.NumData, n2.NumData, OperationTypes.Multiplication) + n2.NumData.Length - i],
                    Positive = true
                };
                long factor1 = (long) n2.NumData[i];
                for (var j = n1.NumData.Length -1; j > -1; j--)
                {
                    long factor2 = (long) n1.NumData[j];
                    long tempResult = factor1 * factor2 + carry;
                    intermediateProduct.NumData[j+1] = (int) (tempResult % UpperLimitPerBlock);
                    carry = (int) (tempResult / UpperLimitPerBlock);
                } 
                intermediateProduct.NumData[0] = carry;
                intermediateProducts[i] = intermediateProduct;
            }
            foreach (var num in intermediateProducts) 
            {
                product += num;
            }
            CondenseNumData(product);
            product.Positive = DetermineSign(n1, n2, OperationTypes.Multiplication);
            return product;
        }

        public static BigNumber operator /(BigNumber n1, BigNumber n2)
        {
            return new BigNumber
            {
                NumData =  new int[DetermineMaxArraySize(n1.NumData, n2.NumData, OperationTypes.Division)],
                Positive = DetermineSign(n1, n2, OperationTypes.Division)
            };
        }

        public static BigNumber operator !(BigNumber n1)
        {
            BigNumber product = n1;
            for (BigNumber i = n1 - new BigNumber(1); i > new BigNumber(); i--)
            {
                product = product * i;
            }
            return product;
        }

        public override string ToString()
        {
            var retval = new StringBuilder(NumData.Length*MaxDigits);
            if (!Positive)
                retval.Append("-");
            retval.Append(NumData[0]);
            var rest = NumData.Skip(1)
                .Select(i => String.Format("{0:D9}", i));
            retval.Append(String.Join("",rest));
            return retval.ToString();
        }

        private static int DetermineMaxArraySize(IList<int> numData1, IList<int> numData2, OperationTypes type)
        {
            switch (type)
            {
                case OperationTypes.Addition:
                    var larger = numData1.Count > numData2.Count 
                        ? numData1.Count
                        : numData2.Count;
                    if (numData1[0] + numData2[0] > UpperLimitPerBlock-1)
                    {
                        return larger + 1;
                    }
                    return larger;
                case OperationTypes.Subtraction:
                    return (numData1.Count > numData2.Count
                        ? numData1.Count
                        : numData2.Count);
                case OperationTypes.Multiplication:
                    return numData1.Count;
                case OperationTypes.Division:
                    return 1;
                default:
                    throw new InvalidEnumArgumentException("Unimplemented operation attempted.");
            }
        }

        private static bool DetermineSign(BigNumber n1, BigNumber n2, OperationTypes type)
        {
            switch (type)
            {
                case OperationTypes.Addition:
                    return n1.Positive && n2.Positive;
                case OperationTypes.Subtraction:
                    return n1 >= n2;
                case OperationTypes.Multiplication:
                case OperationTypes.Division:
                    return !(n1.Positive ^ n2.Positive);
                default:
                    throw new InvalidEnumArgumentException("Unimplemented operation attempted.");
            }
        }

        private static int[] PerformUnsignedAdd(int[] n1Data, int[] n2Data)
        {
            var sum = new int[DetermineMaxArraySize(n1Data, n2Data, OperationTypes.Addition)];
            var carry = 0;
            for (var i = sum.Length - 1; i > -1; i--)
            {
                var add1 = 0;
                var add2 = 0;
                if (i - (sum.Length - n1Data.Length) > -1)
                {
                    add1 = n1Data[i - (sum.Length - n1Data.Length)];
                }
                if (i - (sum.Length - n2Data.Length) > -1)
                {
                    add2 = n2Data[i - (sum.Length - n2Data.Length)];
                }
                var tempResult = add1 + add2 + carry;
                sum[i] = tempResult % UpperLimitPerBlock;
                carry = tempResult / UpperLimitPerBlock;
            }
            return sum;
        }

        /// <summary>
        /// Removes extra array spots that only contain zeros
        /// </summary>
        /// <param name="num"></param>
        private static void CondenseNumData(BigNumber num)
        {
            int remove = 0;
            for (int i = 0; i < num.NumData.Length; i++)
            {
                if (num.NumData[i] != 0) 
                {
                    break;
                }
                remove++;
            }
            int[] newNumData = new int[num.NumData.Length-remove];
            for (int i = 0; i < newNumData.Length; i++)
            {
                newNumData[i] = num.NumData[i+remove];
            }
            num.NumData = newNumData;
        }
    }
}
