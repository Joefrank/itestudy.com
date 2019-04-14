
using CMCommon.Utils.Interfaces;
using System;
using System.Text;

namespace CMCommon.Utils.Implementation
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private string _set = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public RandomStringGenerator()
        {
        }

        public RandomStringGenerator(string strSet)
        {
            _set = strSet;
        }

        /// <summary>
        /// Pick strLength number of characters from _set. must make sure that _set length is greater than the number of characters
        /// </summary>
        /// <param name="strLength"></param>
        /// <returns></returns>
        public string GenerateRandomString(int strLength)
        {
            Random r = new Random();
            var temp = new StringBuilder();

            for (var x = 0; x < strLength; x++)
            {
                temp.Append(_set[r.Next(0, 61)]);
            }

            return temp.ToString();
        }

        /// <summary>
        /// Pick strLength number of characters from mystr. must make sure that mystr length is greater than the number of characters
        /// </summary>
        /// <param name="strLength"></param>
        /// <param name="mystr"></param>
        /// <returns></returns>
        public string GenerateRandomFromString(int strLength, string mystr)
        {
            Random r = new Random();
            var temp = new StringBuilder();

            for (var x = 0; x < strLength; x++)
            {
                temp.Append(mystr[r.Next(0, 61)]);
            }

            return temp.ToString();
        }
    }
}
