using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string TC { get; set; }
        public string Token { get; set; }
        public int CustomerNo { get; set; }

        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
