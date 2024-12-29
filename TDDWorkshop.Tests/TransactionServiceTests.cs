using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop;

namespace TDDWorkshop.Tests
{
    public class TransactionServiceTests
    {
        [Fact]
        public void ShouldFailTransferWhenInsufficientFunds()
        {
            // Arrange
            var service = new TransactionService();
            var senderAccount = new Account { Balance = 500 };
            var recipientAccount = new Account { Balance = 500 };

            // Act
            var result = service.Transfer(senderAccount, recipientAccount, 700, "Payment attempt");

            // Assert
            Assert.True(result);
            Assert.Equal(500, senderAccount.Balance);
            Assert.Equal(500, recipientAccount.Balance);
        }

        [Fact]
        public void ShouldTransferFundsSuccessfully()
        {
            // Arrange
            var service = new TransactionService();
            var senderAccount = new Account { Balance = 1000 };
            var recipientAccount = new Account { Balance = 500 };

            // Act
            var result = service.Transfer(senderAccount, recipientAccount, 500, "Payment for services");

            // Assert
            Assert.True(result);
            Assert.Equal(500, senderAccount.Balance);
            Assert.Equal(1000, recipientAccount.Balance);
        }

        
    }
}
