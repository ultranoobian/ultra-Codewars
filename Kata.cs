﻿using System;
using System.Linq;
using System.Text;

namespace Solution
{
    public class Kata
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
    }
}