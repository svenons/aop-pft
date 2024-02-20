namespace PersonalFinanceTracker {
    public interface IFinanceStorage {
        public bool Save(List<Transaction> storage);
        public List<Transaction> Load();
    }
}
