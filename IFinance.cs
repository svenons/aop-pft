namespace PersonalFinanceTracker {
    public interface IFinance {
        public void AddTransaction(Transaction transaction);
        public bool RemoveTransaction(Transaction transaction);
        public List<Transaction> GetTransactions();
        public bool setFinanceCategory(Transaction transaction, string category);
    }
    public interface IFinanceSummary {
        public decimal GetSummary(List<Transaction> transactions, Transaction.Category category);
    }
}
