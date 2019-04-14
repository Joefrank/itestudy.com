using elearning.model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.admin.Helpers
{
    public static class GenericHelper
    {
        public static List<GlossaryVm> GetGlossaryList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var returnList = new List<GlossaryVm>();

            foreach (var atype in Enum.GetValues(typeof(T)))
            {
                returnList.Add(new GlossaryVm { Id = (int)atype, Name = atype.ToString() });
            }

            return returnList;
        }       

    }
}