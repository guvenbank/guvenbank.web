using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class TransactionModel
    {
        public int senderCustomerNo { get; set; }
        public int receiverCustomerNo { get; set; }
        public int senderBankAccountNo { get; set; }
        public int receiverBankAccountNo { get; set; }
        public double amount { get; set; }
        public string summary { get; set; }
        public DateTime date { get; set; }
        public string receiverFullName { get; set; }
        public TransactionTypes type { get; set; }
    }

    public enum TransactionTypes : int
    {
        Internal,
        External
    }
}
