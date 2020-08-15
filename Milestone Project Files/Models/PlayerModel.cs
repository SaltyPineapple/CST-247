using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Web;

/* Mark Pratt
 * 
 */

namespace Registration.Models {
    [DataContract]
    public class PlayerModel {

        public PlayerModel(String firstname, String lastname, String sex, String state, int age, String email, String username, String password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Sex = sex;
            this.State = state;
            this.Age = age;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        [Required(ErrorMessage = "First Name is Required")]
        [DataMember]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [DataMember]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [DataMember]
        public string Sex { get; set; }

        [Required(ErrorMessage = "State is Required")]
        [DataMember]
        public string State { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [DataMember]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [DataMember]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [DataMember]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int Id { get; set; }



    }
}