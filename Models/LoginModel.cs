using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/* Patrick Garcia
 * 
 */

namespace Milestone2.Models
{
    public class LoginModel
    {
        [DisplayName("Username: ")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(15, ErrorMessage ="Username can only be 15 characters long")]
        public String UserName { get; set; }


        [DisplayName("Password: ")]
        [Required(AllowEmptyStrings = false)]
        [MaxLength(15, ErrorMessage = "Username can only be 15 characters long")]
        public String Password { get; set; }
    }
}