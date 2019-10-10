using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.Password)]
        //[MaxLength(32)]
        //[MinLength(6)]
        //[RegularExpression("^((?=.*?[a-z])(?=.*?[0-9])).{6,32}$")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^\\d{11}$")]
        public string TC { get; set; }
    }
}
