using Milestone2.Models;
using Milestone2.Services.Data;
using Registration.Models;
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
        private SecurityDAO service;
        public bool Authenticate(LoginModel user)
        {
            service = new SecurityDAO();
            return service.FindByUser(user);
        }

        public (List<PlayerModel>, List<PlayerScoreModel>) GetAllUsers()
        {
            service = new SecurityDAO();
            return service.GetAllUsers();
        }

        public void SaveUserScore(PlayerScoreModel score)
        {
            service = new SecurityDAO();
            service.SaveUserScore(score);
        }
    }
}