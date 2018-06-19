using System.ComponentModel;
using System.Globalization;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using CMCommon.Authentication.Infrastructure;
using CMCommon.Security.Interfaces;
using CMCommon.Logging.Interfaces;
using CMCommon.Authentication.Models;

namespace CMCommon.Authentication.Implementation
{
    public class OckAuthService : IOAuthService
    {
        private const string CkNameUserId = "UserId";
        private const string CkNameUserFullName = "UserName";
        private const string CkNameEmail = "Email";
        private const string CkNameCulture = "Culture";
        private const string CkNameLanguageId = "LanguageId";
        private const string CkNameCompanyId = "CompanyId";
        private const string CkNameProfiles = "Profiles";
        private const string CkNameRoles = "Roles";
        private const string CkNameDepartmentId = "DepartmentId";

        private readonly string _cookiePrefix = string.Empty;
        private readonly string _cookieDomain = string.Empty;
        private readonly HttpContext _context;
        private readonly ClientUser _user = new ClientUser();
        private readonly IEncryptionService _encryptionService;
        private readonly ILoggerService _logger;

        #region publicproperties     

        public string UserFullName
        {
            get { return ReadEncryptedCookie<string>(CkNameUserFullName); }
        }

        public int UserId
        {
            get { return ReadEncryptedCookie<int>(CkNameUserId); }
        }

        public int UserLanguageId
        {
            get { return ReadEncryptedCookie<int>(CkNameLanguageId); }
        }

        public  int UserCompanyId
        {
            get { return ReadEncryptedCookie<int>(CkNameCompanyId); }
        }

        public int UserDepartmentId
        {
            get { return ReadEncryptedCookie<int>(CkNameDepartmentId); }
        }

        public string UserProfiles
        {
            get { return ReadEncryptedCookie<string>(CkNameProfiles); }
        }

        public string UserRoles
        {
            get { return ReadEncryptedCookie<string>(CkNameRoles); }
        }

        public  string UserCulture
        {
            get { return ReadEncryptedCookie<string>(CkNameCulture); }
        }

        public string UserEmail
        {
            get { return ReadEncryptedCookie<string>(CkNameEmail); }
        }

        #endregion

        #region constructors

        public OckAuthService(ILoggerService logger)
        {
            _context = HttpContext.Current;
            _logger = logger;
        }
        
        public OckAuthService(ILoggerService logger, string cookieDomain, string cookiePrefix)
        {
            _context = HttpContext.Current;
            _cookiePrefix = cookiePrefix;
            _cookieDomain = cookieDomain;
            _logger = logger;
        }

        public OckAuthService(IEncryptionService encryptionService, ILoggerService logger, string cookieDomain, string cookiePrefix)
        {
            _encryptionService = encryptionService;
            _context = HttpContext.Current;
            _cookiePrefix = cookiePrefix;
            _cookieDomain = cookieDomain;
            _logger = logger;
        }

        #endregion

        public HttpCookie GenerateCookie(string cookieName, string cookieValue, TimeSpan tmDuration)
        {
            var cookie = new HttpCookie(_cookiePrefix + cookieName)
            {
                Value = !string.IsNullOrEmpty(cookieValue) ? cookieValue : "",
                Expires = DateTime.Now + tmDuration,
                Domain = _cookieDomain
            };

            return cookie;
        }

        public bool PlaceClientCookie(string cookieName, string ckValue, TimeSpan tmDuration)
        {
            var ck = GenerateCookie(cookieName, ckValue, tmDuration);
            HttpContext.Current.Response.Cookies.Add(ck);
            return true;
        }

        public bool DeleteClientCookie(string cookieName)
        {
            return PlaceClientCookie(cookieName, "", new TimeSpan(-1, 0, 0, 0));
        }

        /// <summary>
        /// Generates an encrypted cookie based on values supplied in parameters
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="tmDuration"></param>
        /// <returns></returns>
        public HttpCookie GenerateEncryptedCookie(string cookieName, string cookieValue, TimeSpan tmDuration)
        {
            var cookie = new HttpCookie(_cookiePrefix + cookieName)
                {
                    Value = !string.IsNullOrEmpty(cookieValue) ? _encryptionService.Encrypt(cookieValue) : "",
                    Expires = DateTime.Now + tmDuration,
                    Domain = _cookieDomain
                };

            return cookie;
        }

        /// <summary>
        /// Places an encrypted cookie to client.
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="ckValue"></param>
        /// <param name="tmDuration"></param>
        /// <returns></returns>
        public bool PlaceClientEncryptedCookie(string cookieName, string ckValue, TimeSpan tmDuration)
        {
            var ck = GenerateEncryptedCookie(cookieName, ckValue, tmDuration);
            HttpContext.Current.Response.Cookies.Add(ck);
            return true;
        }

        /// <summary>
        /// Gets an encrypted cookie from client and returns type as requested
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public T ReadEncryptedCookie<T>(string cookieName)
        {
              
                try
                {
                    var cookie = HttpContext.Current.Request.Cookies[_cookiePrefix + 
                        cookieName];
                    if (cookie == null || (!string.IsNullOrEmpty(cookie.Domain) && cookie.Domain != _cookieDomain))
                        return default(T);

                    var converter = TypeDescriptor.GetConverter(typeof(T));

                    if (converter.CanConvertFrom(typeof (string)))
                    {
                        string value = _encryptionService.Decrypt(cookie.Value);
                        return (T) converter.ConvertFromString(value);
                    }
                    else
                    {
                        return default(T);
                    }
                   
                }
                catch(Exception ex)
            {
                _logger.LogItem(ex.Message);
                return default(T);
            }
        }

        public ClientUser GetClientUser()
        {
            try
            {
                var lstProfiles = !string.IsNullOrEmpty(UserProfiles)? 
                    (UserProfiles.Split(',').Select(s => s.Trim())).ToList() : new List<string>();
                var lstRoles = UserRoles.Split(',').Select(s => s.Trim()).ToList();

                return new ClientUser
                    {
                        Profiles = lstProfiles,
                        Roles = lstRoles,//user must have some roles
                        UserId = UserId,
                        UserName = UserEmail,
                        FullName = UserFullName,
                        Culture = UserCulture,
                        LanguageId = UserLanguageId,
                        CompanyId = UserCompanyId,
                        DepartmentId = UserDepartmentId
                    };
            }
            catch(Exception ex)
            {
                _logger.LogItem("GetUser - Could not compile user data" + ex.Message);
            }

            return null;
        }

        public void PersistClientUser(ClientUser user)
        {
            try
            {
                var sProfiles = user.Profiles != null? string.Join(",", user.Profiles.Select(x => x).ToArray()) : "";
                var sRoles = string.Join(",", user.Roles.Select(x => x).ToArray());

                PlaceClientEncryptedCookie(CkNameUserId, user.UserId.ToString(CultureInfo.InvariantCulture), user.CookieDuration);
                PlaceClientEncryptedCookie(CkNameEmail, user.UserName, user.CookieDuration);
                PlaceClientEncryptedCookie(CkNameUserFullName, user.FullName, user.CookieDuration);
                PlaceClientEncryptedCookie(CkNameCulture, user.Culture, user.CookieDuration);
                PlaceClientEncryptedCookie(CkNameLanguageId, user.LanguageId.ToString(CultureInfo.InvariantCulture), user.CookieDuration);
                PlaceClientEncryptedCookie(CkNameCompanyId, user.CompanyId.ToString(), user.CookieDuration);
                PlaceClientEncryptedCookie(CkNameDepartmentId, user.DepartmentId.ToString(), user.CookieDuration);
                if(!string.IsNullOrEmpty(sProfiles))
                PlaceClientEncryptedCookie(CkNameProfiles, sProfiles, user.CookieDuration);
                PlaceClientEncryptedCookie(CkNameRoles,sRoles , user.CookieDuration);
            }
            catch (Exception ex)
            {
                _logger.LogItem("PersistUser - Failed to persist user data username " + user.UserName + " exception: " + Environment.NewLine + ex.Message);
            }
        }


        public void LogUserOut()
        {
            try
            {
                DeleteClientCookie(CkNameUserId);
                DeleteClientCookie(CkNameEmail);
                DeleteClientCookie(CkNameUserFullName);
                DeleteClientCookie(CkNameCulture);
                DeleteClientCookie(CkNameLanguageId);
                DeleteClientCookie(CkNameProfiles);
                DeleteClientCookie(CkNameRoles);
                DeleteClientCookie(CkNameCompanyId);
                DeleteClientCookie(CkNameDepartmentId);
            }
            catch (Exception ex)
            {
                _logger.LogItem("LogUserOut failed." + ex.Message);
            }
        }

        public bool IsUserLoggedIn()
        {
            return UserId > 0 && !string.IsNullOrEmpty(UserRoles) && 
                !string.IsNullOrEmpty(UserEmail) && !string.IsNullOrEmpty(UserFullName);
        }
    }
}
