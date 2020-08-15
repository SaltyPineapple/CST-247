using Milestone2.Services.Business;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MinesweeperService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        public List<PlayerModel> users { get; set; }
        public List<PlayerScoreModel> playerScores { get; set; }

        public UserService()
        {
            SecurityService service = new SecurityService();
            users = new List<PlayerModel>();
            playerScores = new List<PlayerScoreModel>();
            var temp = service.GetAllUsers();
            users = temp.Item1;
            playerScores = temp.Item2;
        }

        public UserDTO GetAllUsers()
        {
            var user = new UserDTO();
            user.ErrorCode = 0;
            user.ErrorMessage = "OK";
            user.Data = users;
            return user;
        }

        public UserDTO GetUser(string id)
        {
            var user = new UserDTO();
            var l = users.Count();//getting user list count
            var i = int.Parse(id);

            if (i < l)
            {
                user.ErrorCode = 0;
                user.ErrorMessage = "OK";
                user.Data.Add(users[i]);
            }
            else
            {
                user.ErrorCode = -1;
                user.ErrorMessage = "User Does Not Exist";
            }
            return user;
        }

        public string SayHello()
        {
            return "Hello, and goodbye, as always";
        }

        public UserScoreDTO GetAllUserScores()
        {
            var userScore = new UserScoreDTO();
            userScore.ErrorCode = 0;
            userScore.ErrorMessage = "OK";
            userScore.Data = playerScores;
            return userScore;
        }
    }
}
