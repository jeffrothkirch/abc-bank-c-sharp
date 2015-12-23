using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class AccountTest
    {
        [Test]
        public void checking_InterestIearned_Correct()
        {
            double depoistAmount = 5000000;
            double expectedInterest = (depoistAmount * 0.001);

            var account = new Account(Account.CHECKING);
            account.deposit(depoistAmount);

            Assert.AreEqual(expectedInterest, account.interestEarned());
        }

        [Test]
        public void savings_InterestIearned_Correct()
        {
            var account = new Account(Account.SAVINGS);
            account.deposit(5000000);

            Assert.AreEqual(9999.0, account.interestEarned());
        }

        [Test]
        public void maxisavings_InterestIearned_NoTransactionsTenDays_Correct()
        {
            var account = new Account(Account.MAXI_SAVINGS);
            account.deposit(5000000);

            Assert.AreEqual(5000000 * 0.05, account.interestEarned());
        }
    }
}
