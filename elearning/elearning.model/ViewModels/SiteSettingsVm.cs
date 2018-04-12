using CMCommon.Authentication.Models;
using elearning.model.Enums;

namespace elearning.model.ViewModels
{
    public class SiteSettingsVm
    {
        public SiteType SiteType { get; set; }

        public string SiteName { get; set; }

        public ClientUser ClientUser { get; set; }
    }
}
