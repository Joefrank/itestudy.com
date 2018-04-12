using elearning.data;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.services.Interfaces;
using System.Linq;

namespace elearning.services.Implementation
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public EmailTemplate GetTemplate(EmailTemplateType templateType)
        {
            EmailTemplate template = null;

            using(var context = new DataDbContext())
            {
                var first = context.EmailTemplates.FirstOrDefault();
                var temp = first.Type == templateType;
                template = context.EmailTemplates.FirstOrDefault(x => x.Type == templateType);
            }

            return template;
        }
        
    }
}
