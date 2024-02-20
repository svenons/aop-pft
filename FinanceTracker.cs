namespace PersonalFinanceTracker {
    public class FinanceTracker: IFinance{
        public List<Transaction>? Transactions { get; set; }
        public void AddTransaction(Transaction transaction)
        {
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }
            Transactions.Add(transaction);
        }
        public bool RemoveTransaction(Transaction transaction)
        {
            if (Transactions != null)
            {
                if(Transactions.Contains(transaction))
                {
                    Transactions.Remove(transaction);
                    return true;
                }             
            }
            return false;
        }
        public List<Transaction> GetTransactions()
        {
            if (Transactions == null)
            {
                Transactions = new List<Transaction>();
            }
            return Transactions;
        }
        public bool setFinanceCategory(Transaction transaction, string newCategory)
        {
            if (Transactions != null)
            {
                foreach (Transaction t in Transactions)
                {
                    if (t.ID == transaction.ID)
                    {
                        t.TransactionCategory = (Transaction.Category)Enum.Parse(typeof(Transaction.Category), newCategory);
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class FinanceSummary: IFinanceSummary
    {
        public decimal GetSummary(List<Transaction> transactions, Transaction.Category category)
        {
            Summary summary = new Summary(transactions, category);
            return summary.Amount;
        }
    }
}
