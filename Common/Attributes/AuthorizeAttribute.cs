using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Common.Attributes
{
    public class CastomAttribute : TypeFilterAttribute
    {
        public CastomAttribute(params string[] claim) : base(typeof(AuthorizeFilter))
        {
            Arguments = new object[] { claim };
        }
    }

    public class AuthorizeFilter : IAuthorizationFilter
    {
        readonly string[] _claim;

        public AuthorizeFilter(params string[] claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var IsAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (IsAuthenticated)
            {
                bool flagClaim = false;
                foreach (var item in _claim)
                {
                    if (context.HttpContext.User.HasClaim(ClaimTypes.Role, item))
                        flagClaim = true;
                }
                if (!flagClaim)
                    context.Result = new RedirectResult("~/Account/Login");
            }
            else
            {
                context.Result = new RedirectResult("~/Account/Login");
            }
            return;
        }
    }

}
