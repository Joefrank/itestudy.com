using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elearning.model.ViewModels
{
   public class AddArticleCategoryVM
    {
        public string categoryName { get; set; }
        public string description { get; set; }
        public DateTime dateCreated { get; set; }
    }
}
