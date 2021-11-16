using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test1WebApp.Models
{
    public class User
    {
        public string Id  { get; set; }
        //Assuming that the username is unique
        [Required]
        public string Username{ get; set; }
        [Required]
        public string Surname { get; set; }
        [DisplayName("Cellphone number")]
        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Maximum is 12 and Minimun is 6 for Cellphone number")]
        public string CellphoneNumber { get; set; }
    }
}
