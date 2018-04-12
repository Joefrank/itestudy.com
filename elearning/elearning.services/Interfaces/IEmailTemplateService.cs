
using elearning.model.DataModels;
using elearning.model.Enums;

namespace elearning.services.Interfaces
{
    public interface IEmailTemplateService
    {
        EmailTemplate GetTemplate(EmailTemplateType template);
    }
}
