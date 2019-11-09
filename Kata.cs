using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solution
{
    public partial class Kata
    {
        #region Old Kata
        public static bool is_valid_IP(string IpAddres)
        {
            string[] stringValues = IpAddres.Split('.');
            if (stringValues.Length != 4) { return false; }

            int castInt = 0;
            foreach (string s in stringValues)
            {
                if (s.StartsWith("0")) { return false; }
                if (s.Contains(" ")) { return false; }
                if (!int.TryParse(s, out castInt)) { return false; }
                if (castInt < 0 || castInt > 255) { return false; }
            }

            return true;
        }

        /// <summary>
        /// Write a function to convert a name into initials.
        /// <para>https://www.codewars.com/kata/abbreviate-a-two-word-name</para>
        /// </summary>
        /// <param name="name"> Two word name</param>
        /// <returns>Initials with period seperator</returns>
        public static string AbbrevName(string name)
        {
            var components = name.Split(' ');
            if (components.Length == 2)
            {
                var com = components.Select(e => e.First().ToString().ToUpper());
                return string.Join(".", com);
            }
            return "";
        }


        #endregion

        /// <summary>
        /// Write a function that accepts an array of 10 integers (between 0 and 9), that returns a string of those numbers in the form of a phone number.
        /// <para>https://www.codewars.com/kata/create-phone-number</para>
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static string CreatePhoneNumber(int[] numbers)
        {
            return string.Format("({0}{1}{2}) {3}{4}{5}-{6}{7}{8}{9}", numbers.Select(e => e.ToString()).ToArray());
        }

        /// <summary>
        /// <para>https://www.codewars.com/kata/take-a-ten-minute-walk</para>
        /// </summary>
        /// <param name="walk"></param>
        /// <returns></returns>
        public static bool IsValidWalk(string[] walk)
        {
            if (walk.Length != 10) return false;

            int x = 0;
            int y = 0;

            foreach (string direction in walk)
            {
                switch (direction)
                {
                    case "n":
                        x++; break;
                    case "s":
                        x--; break;
                    case "e":
                        y++; break;
                    case "w":
                        y--; break;
                    default:
                        break;
                }
            }

            if (x != 0 || y != 0) return false;

            return true;
        }

        /// <summary>
        /// You get an array of numbers, return the sum of all of the positives ones.
        /// <para>https://www.codewars.com/kata/sum-of-positive/train/</para>
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int PositiveSum(int[] arr)
        {
            return arr.Where(e => e > 0).Sum();
        }

        /// <summary>
        /// Bob is working as a bus driver. However, he has become extremely popular amongst the city's residents. With so many passengers wanting to get aboard his bus, he sometimes has to face the problem of not enough space left on the bus! He wants you to write a simple program telling him if he will be able to fit all the passengers.
        /// <para>https://www.codewars.com/kata/will-there-be-enough-space/train/csharp</para>
        /// </summary>
        /// <param name="cap"></param>
        /// <param name="on"></param>
        /// <param name="wait"></param>
        /// <returns></returns>
        public static int Enough(int cap, int on, int wait)
        {
            return on + wait > cap ? Math.Abs(on + wait - cap) : 0;
        }

        /// <summary>
        /// You are given the total volume m of the building. Being given m can you find the number n of cubes you will have to build?
        /// <para>https://www.codewars.com/kata/build-a-pile-of-cubes/train/csharp</para>
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static long findNb(long m)
        {
            long cubeCount = 1;
            long volumeCount = 1;
            while (volumeCount <= m)
            {
                if (volumeCount == m) return cubeCount;
                cubeCount++;
                volumeCount += (cubeCount * cubeCount * cubeCount);
            }
            return -1L;
        }

        /// <summary>
        /// </summary>
        ///
        public static int GetLongestPalindrome(string str)
        {
            int longestPalindrome = 0;
            if (String.IsNullOrEmpty(str)) { return longestPalindrome; }

            // Loop through increasing lengths of substrings until the full length
            for (int subStringLength = 0; subStringLength <= str.Length; subStringLength++)
            {
                int position = 0;
                // Move the substring search through the string
                while (position + subStringLength <= str.Length)
                {
                    if (IsThisAPalindrome(str.Substring(position, subStringLength)))
                    {
                        longestPalindrome = subStringLength;
                        break;
                    }
                    position++;
                }
            }
            return longestPalindrome;
        }

        private static bool IsThisAPalindrome(string str)
        {
            return str.SequenceEqual(str.Reverse());
        }

        /// <summary>There was a test in your class and you passed it. Congratulations!
        /// But you're an ambitious person. You want to know if you're better than the average student in your class.
        /// You receive an array with your peers' test scores. Now calculate the average and compare your score!
        /// <para>https://www.codewars.com/kata/how-good-are-you-really/train/csharp</para>
        /// </summary>
        public static bool BetterThanAverage(int[] ClassPoints, int YourPoints)
        {
            List<int> list = ClassPoints.ToList();
            list.Append(YourPoints);
            return YourPoints > list.Average() ? true : false;
        }

    }
}