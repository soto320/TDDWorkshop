using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Workshop
{


    public class TransactionService
    {
        public bool Transfer(Account sender, Account recipient, decimal amount, string description)
        {
            if (sender.Balance >= amount)
            {
                sender.Balance -= amount;
                recipient.Balance += amount;
                return true;
            }
            return false;
        }
    }
}
