using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TransferResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public decimal? SenderBalance { get; set; }
        public decimal? RecipientBalance { get; set; }
    }
}
