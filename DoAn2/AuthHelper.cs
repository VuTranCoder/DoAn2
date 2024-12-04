using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn2
{
    public static class AuthHelper
    {
        public static bool IsLoggedIn(HttpSessionStateBase session)
        {
            return session["UserID"] != null;
        }
    }
}