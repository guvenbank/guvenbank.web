using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class TransactionsModel
    {
        public string status { get; set; }
        public List<TransactionModel> transactions { get; set; }
    }
}
