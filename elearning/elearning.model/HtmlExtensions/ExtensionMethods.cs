using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace elearning.model.HtmlExtensions
{  
    public static class ExtensionMethods
    {
        //Your Extension method
        public static MvcHtmlString DropDownWithValue(this HtmlHelper html, IEnumerable<string> valuesList, string idName, string selectedValue, string className)
        {
            var sbTemp = new StringBuilder();

            foreach(var val in valuesList)
            {
                var selectedStr = string.Empty;
                if(!string.IsNullOrEmpty(selectedValue) && val.Equals(selectedValue, StringComparison.CurrentCultureIgnoreCase))
                {
                    selectedStr = @" selected=""selected"" ";
                }

                sbTemp.AppendLine(string.Format("<option value=\"{0}\"{1}>{0}</option>", val, selectedStr));
            }

            var classStr = string.IsNullOrEmpty(className) ? "" : @" class=""" + className + @""" ";
            var idNameStr = " id=\"" + idName + "\" name=\"" + idName + "\" ";
            sbTemp.Insert(0, $"<select{classStr}{idNameStr}>").AppendLine("</select>");
            
            return new MvcHtmlString(sbTemp.ToString());
        }

        public static MvcHtmlString DropDownWithValue(this HtmlHelper html, IEnumerable<KeyValuePair<int, string>> valuesList, string idName, string selectedValue, string className)
        {
            var sbTemp = new StringBuilder();

            foreach (var val in valuesList)
            {
                var selectedStr = string.Empty;
                if (!string.IsNullOrEmpty(selectedValue) && val.Key.ToString().Equals(selectedValue, StringComparison.CurrentCultureIgnoreCase))
                {
                    selectedStr = @" selected=""selected"" ";
                }

                sbTemp.AppendLine($"<option value=\"{val.Key}\"{selectedStr}>{val.Value}</option>");
            }

            var classStr = string.IsNullOrEmpty(className) ? "" : @" class=""" + className + @""" ";
            var idNameStr = " id=\"" + idName + "\" name=\"" + idName + "\" ";
            sbTemp.Insert(0, $"<select{classStr}{idNameStr}>").AppendLine("</select>");

            return new MvcHtmlString(sbTemp.ToString());
        }
    }

}
