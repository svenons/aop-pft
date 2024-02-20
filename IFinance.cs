namespace PersonalFinanceTracker {
    public interface IFinance {
        public void AddTransaction(Transaction transaction);
        public void RemoveTransaction(Transaction transaction);
        public void GetTransactions();
        public void setFinanceCategory(Transaction transaction, string category);
    }
}
