using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Activity4Part2_REST {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1 {

        Random random = new Random();
        List<User> userList = new List<User>();

        public Service1() {
            User u1 = new User(1, "Mark", true, 92000, GetListOfScores(random));
            User u2 = new User(2, "David", true, 62000, GetListOfScores(random));
            User u3 = new User(3, "Joel", false, 122000, GetListOfScores(random));
            User u4 = new User(4, "Brianne", false, 80000, GetListOfScores(random));

            userList.Add(u1);
            userList.Add(u2);
            userList.Add(u3);
            userList.Add(u4);
        }

        private static List<int> GetListOfScores(Random rand) {
            List<int> scoresList = new List<int>();

            for (int x = 0; x < 10; x++) {
                scoresList.Add(rand.Next(100));
            }

            return scoresList;
        }

        public UserDTO GetAllUsers() {
            UserDTO userDTO = new UserDTO();
            userDTO.MessageCode = 1;
            userDTO.MessageText = "Everyone is here";
            userDTO.UserList = userList;
            
            return userDTO;
        }

        public string GetData(string value) {
            return string.Format("You entered: {0}", value);
        }

        public UserDTO GetUsersByID(string ID) {
            UserDTO userDTO = new UserDTO();
            List<User> correctUsers = userList.Where(x => x.ID == int.Parse(ID)).ToList();

            userDTO.UserList = correctUsers;
            userDTO.MessageCode = correctUsers.Count();
            userDTO.MessageText = "People with an ID of " + ID;

            return userDTO;

        }

        public UserDTO GetUsersByName(string name) {
            UserDTO userDTO = new UserDTO();
            List<User> correctUsers = userList.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

            userDTO.UserList = correctUsers;
            userDTO.MessageCode = correctUsers.Count();
            userDTO.MessageText = "People who have " + name + " in their name.";

            return userDTO;

        }
    }
}
