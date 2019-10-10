using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BankAccountModel
    {
        public string No { get; set; }

        public decimal Balance { get; set; }

        //public static BankAccount FromModelTo(BankAccountModel bankAccountModel)
        //{
        //    return new BankAccount { No = bankAccountModel.No, Balance = bankAccountModel.Balance };
        //}
    }
}
