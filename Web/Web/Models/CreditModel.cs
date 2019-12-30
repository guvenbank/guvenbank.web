using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CreditModel
    {
        public decimal krediMiktari { get; set; }

        public int yas { get; set; }

        public int aldigi_kredi_sayi { get; set; }

        public HasHome evDurumu { get; set; }

        public HasPhone telefonDurumu { get; set; }

        public enum HasHome : byte
        {
            VAR,
            YOK
        }

        public enum HasPhone : byte
        {
            VAR,
            YOK
        }
    }
}
