using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test2StringCalculator.Models
{
    public class StringCalculator
    {
        [Display(Name = "Numbers", Prompt = "Enter numbers to calculate ")]
        public string Numbers { get; set; }
    }
}
