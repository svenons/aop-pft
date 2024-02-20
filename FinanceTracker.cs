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
        public FinanceTracker()
        {
            Transactions = new JsonFinanceStorage().Load(); // Load transactions from file before starting
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
