using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class HomePageModel
    {
        public TransactionsModel TransactionsModel { get; set; }

        public UserModel UserModel { get; set; }

        public BankAccountsModel BankAccountsModel { get; set; }

        //bankaccount
    }
}
