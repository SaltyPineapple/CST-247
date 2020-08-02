using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity_3.Models {
    public class Customer {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Age { get; set; }

        public Customer(int id, string name, int age) {
            Name = name;
            ID = id;
            Age = age;
        }
    }
}