using System;
using System.Web;
using CMCommon.Authentication.Models;

namespace CMCommon.Authentication.Infrastructure
{
    public interface IOAuthService
    {
        HttpCookie GenerateEncryptedCookie(string cookieName, string cookieValue, TimeSpan tmDuration);

        bool PlaceClientEncryptedCookie(string cookieName, string ckValue, TimeSpan tmDuration);

        T ReadEncryptedCookie<T>(string cookieName);

        ClientUser GetClientUser();

        void PersistClientUser(ClientUser user);

        void LogUserOut();

        bool IsUserLoggedIn();
    }
}
