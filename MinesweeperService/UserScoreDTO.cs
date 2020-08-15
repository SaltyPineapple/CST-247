using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MinesweeperService
{
    [DataContract]
    public class UserScoreDTO
    {
        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public String ErrorMessage { get; set; }

        [DataMember]
        public List<PlayerScoreModel> Data { get; set; } = new List<PlayerScoreModel>();
    }
}