using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BankAccountsModel
    {
        public string Status { get; set; }
        public List<BankAccountModel> BankAccounts { get; set; }

    }
}
