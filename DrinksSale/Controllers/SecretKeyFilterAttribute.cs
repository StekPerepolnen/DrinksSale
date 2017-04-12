using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace DrinksSale.Controllers
{
    public class SecretKeyFilterAttribute : FilterAttribute, IAuthenticationFilter
    {
        public string Parameter { get; set; }

        public string Value { get; set; }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "GET" && filterContext.HttpContext.Request.QueryString[Parameter] != Value
                || filterContext.HttpContext.Request.HttpMethod == "POST" && filterContext.HttpContext.Request.Form[Parameter] != Value)
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                });
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}