using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MinesweeperService
{
    [DataContract]
    public class UserDTO
    {
        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public String ErrorMessage { get; set; }

        [DataMember]
        public List<PlayerModel> Data { get; set; } = new List<PlayerModel>();
    }
}