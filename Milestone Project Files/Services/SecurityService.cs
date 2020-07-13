using Milestone2.Models;
using Milestone2.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/* Patrick Garcia
 * 
 */

namespace Milestone2.Services.Business
{
    public class SecurityService
    {
        public bool Authenticate(LoginModel user)
        {
            SecurityDAO service = new SecurityDAO();
            return service.FindByUser(user);
        }
    }
}