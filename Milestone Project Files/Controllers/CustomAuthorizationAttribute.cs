using Milestone2.Models;
using Milestone2.Services.Business;
using Registration.Models;
using System;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Registration.Controllers
{
    /*
     * This Authorization attribute blocks access to page decorated with [CustomAuthorization] annotation until user has logged in.
     * 
     * */
    internal class CustomAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            SecurityService service = new SecurityService();

            LoginModel user = (LoginModel)System.Web.HttpContext.Current.Session["user"];

            bool success = false;
                
            if(user != null)
            {
                success = service.Authenticate(user);
            }
                
            if (success)
            {
                //Do nothing user is already logged in
            }
            else
            {
                //User is not logged in. Redirect to login page
                filterContext.Result = new RedirectResult("/login");
            }
        }
    }
}