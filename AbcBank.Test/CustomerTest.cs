﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    [TestFixture]
    public class CustomerTest
    {

        [Test] //Test customer statement generation
        public void testApp()
        {

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").openAccount(checkingAccount).openAccount(savingsAccount);

            checkingAccount.deposit(100.0);
            savingsAccount.deposit(4000.0);
            savingsAccount.withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.getStatement());
        }

        [Test]
        public void testOneAccount()
        {
            Customer oscar = new Customer("Oscar").openAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.getNumberOfAccounts());
        }

        [Test]
        public void testThreeAcounts()
        {
            Customer oscar = new Customer("Oscar")
                    .openAccount(new Account(Account.SAVINGS));
            oscar.openAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.getNumberOfAccounts());
        }

        [Ignore]
        public void transferBetweenAccount_BadAmountThrows()
        {
            var accountFrom = new Account(Account.SAVINGS);
            var accountTo = new Account(Account.CHECKING);

            Customer oscar = new Customer("Oscar").openAccount(accountFrom);
            oscar.openAccount(accountTo);

            oscar.TransferBetweenAccounts(100, accountFrom.getId(), accountTo.getId());

            Assert.Throws<ArgumentException>(() => { }, "The amount needs to be greater than or equal to accout from balance");
        }
        [Test]
        public void transferBetweenAccount_Valid_Succeeds()
        {
            var accountFrom = new Account(Account.SAVINGS);
            var accountTo = new Account(Account.CHECKING);

            accountFrom.deposit(200);

            Customer oscar = new Customer("Oscar").openAccount(accountFrom);
            oscar.openAccount(accountTo);

            oscar.TransferBetweenAccounts(100, accountFrom.getId(), accountTo.getId());

            Assert.AreEqual(accountFrom.sumTransactions(), 100);
            Assert.AreEqual(accountTo.sumTransactions(), 100);
        }
    }
}
