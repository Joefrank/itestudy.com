using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elearning.model.ViewModels
{
   public class AddArticleVM
    {
        public string title { get; set; }
        public string content { get; set; }
        public string youtubeLinks { get; set; }
        public string dateCreated { get; set; }
        public int CategoryID { get; set; }

    }
}
