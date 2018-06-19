using System;
using System.Collections.Generic;
using System.Linq;

namespace CMCommon.Authentication.Models
{
    public class ClientUser
    {
        public List<string> Profiles { get; set; }

        public List<string> Roles { get; set; }

        public string FullName { get; set; }
       
        public string UserName { get; set; }

        public string Data { get; set; }

        public int UserId { get; set; }

        public int LanguageId { get; set; }

        public int CompanyId { get; set; }

        public int DepartmentId { get; set; }

        public string Culture { get; set; }

        public TimeSpan CookieDuration { get; set; }

        public bool IsInRole(string role)
        {
            return Roles.Any() && Roles.Any(x => x.Trim().Equals(role.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }        

        public bool HasRole(string role)
        {
            return Roles.Contains(role.Trim());
        }

        public bool IsInProfile(string profile)
        {
            return Profiles.Any() && Profiles.Any(x => x.Trim().Equals(profile.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }       

        public bool HasProfile(int profileid)
        {
            return Profiles.Any() && Profiles.Contains(profileid.ToString());
        }

        public bool IsLogged()
        {
            return (UserId > 0 && Roles != null && Roles.Any());
        }
    }
}
