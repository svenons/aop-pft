namespace PersonalFinanceTracker {
    public class Transaction {
        public Guid ID { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public enum Category {
                Income,
                Rent,
                Utilities,
                Groceries,
                Other
        }
        public Category TransactionCategory { get; set; }
        
        // Parameterless constructor
        public Transaction(DateTime now) {
            Description = "No description"; //added this to avoid null reference exception
        }
        public Transaction(DateTime date, string description, decimal amount, Category category) {
            Date = date;
            Description = description;
            Amount = amount;
            TransactionCategory = category;
        }
    }
}
