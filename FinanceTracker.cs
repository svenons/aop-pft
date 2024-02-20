namespace PersonalFinanceTracker {
    public class FinanceTracker: IFinance{
        public List<Transaction>? Transactions { get; set; }
        public JsonFinanceStorage storage = new JsonFinanceStorage();
        public void AddTransaction(Transaction transaction)
        {
            if (Transactions == null)
            {
                Transactions = storage.Load();
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
                Transactions = storage.Load();
            }
            return Transactions;
        }
        public bool Save()
        {
            if (storage != null)
            {
                return storage.Save(Transactions!);
            }
            return false;
        }
        public void Load()
        {
            Transactions = storage.Load();
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
