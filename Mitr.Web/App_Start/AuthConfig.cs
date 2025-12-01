using Microsoft.AspNet.Membership.OpenAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.App_Start
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            //GoogleOAuth2Client clientGoog = new GoogleOAuth2Client("[Your ClientId]", "[Secret key]");
            //IDictionary<string, string> extraData = new Dictionary<string, string>();
            //OpenAuth.AuthenticationClients.Add("google", () => clientGoog, extraData);
        }
    }
}