using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Test3RestApi.Models
{
    public class Student
    {       
        public string Id { get; set; }
        [Required]
        public string Fullname { get; set; }
        public string Nationality { get; set; }
    }
}
