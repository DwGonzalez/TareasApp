﻿using System.Security.Claims;

namespace TareasApp.Helper
{
    public static class ClaimsHelper
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;
        }
    }
}
