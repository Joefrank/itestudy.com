
using System.Collections.Generic;

namespace elearning.utils
{
    public class ListGenerator
    {
        public static IEnumerable<string> GetTitles()
        {
            return new List<string>
            {
                "Mr",
                "Mrs",
                "Miss",
                "Dr.",
                "Pr."
            };
        }

        public class Title
        {
            public int Id { get; set; }
            public string Name { get; set;}
        }
    }
}
