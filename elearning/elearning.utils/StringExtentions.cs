using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.utils
{
    public static class StringExtentions
    {
        public static string ReplaceValues(this String inputString, Dictionary<string, string> dicto)
        {
            var temp = new StringBuilder(inputString);

            foreach(var entry in dicto)
            {
                temp.Replace(entry.Key, entry.Value);
            }

            return temp.ToString();
        }
    }
}
