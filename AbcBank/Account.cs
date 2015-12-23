using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank
{
    public class Account
    {
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        private readonly Guid _id = Guid.NewGuid();
        public List<Transaction> transactions;

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount));
            }
        }

        public void withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double interestEarned()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                case MAXI_SAVINGS:
                    if(transactions.Where(x => x.getTransactionDate().Date.AddDays(10) >= DateProvider.getInstance().now()).Any())
                        return amount * 0.05;

                    return amount * 0.010;
                default:
                     return amount * 0.001;
            }
        }

        private double CalculateMaxiSavingsInterest(double amount)
        {
            if (transactions.Where(x => x.getTransactionDate().Date.AddDays(10) >= DateProvider.getInstance().now()).Any())
                return 0.05;

            return 0.010;
        }

        public double sumTransactions()
        {
            return checkIfTransactionsExist(true);
        }

        private double checkIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int getAccountType()
        {
            return accountType;
        }

        public Guid getId()
        {
            return _id;
        }

    }
}
