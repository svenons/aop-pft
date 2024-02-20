namespace PersonalFinanceTracker {
    public interface IFinance {
        public void AddTransaction(Transaction transaction);
        public bool RemoveTransaction(Transaction transaction);
        public List<Transaction> GetTransactions();
        public bool setFinanceCategory(Transaction transaction, string category);
    }
}
