using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Registration.Models
{
    [DataContract]
    public class PlayerScoreModel
    {
        public PlayerScoreModel(String username, int clicks, int timetaken)
        {
            this.Username = username;
            this.Clicks = clicks;
            this.TimeTaken = timetaken;
        }

        [DataMember]
        public String Username { get; set; }

        [DataMember]
        public int Clicks { get; set; }

        [DataMember]
        public int TimeTaken { get; set; }
    }
}