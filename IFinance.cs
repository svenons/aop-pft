namespace PersonalFinanceTracker {
    public interface IFinance {
        public void AddTransaction(Transaction transaction);
        public bool RemoveTransaction(Transaction transaction);
        public bool editTransaction(Transaction transaction, Transaction newTransaction);
        public List<Transaction> GetTransactions();
        public bool Save();
        public void Load();
    }
}
