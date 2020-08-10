﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Activity4Part2_REST {
    [DataContract]
    public class UserDTO {
        [DataMember]
        public List<User> UserList { get; set; }

        [DataMember]
        public int MessageCode { get; set; }

        [DataMember]
        public string MessageText { get; set; }

    }
}