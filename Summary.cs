namespace PersonalFinanceTracker
{
    public class Summary
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public Summary(List<Transaction> transactions, Transaction.Category category) {
            foreach (Transaction t in transactions)
            {
                if (t.TransactionCategory == category)
                {
                    Amount += t.Amount;
                }
            }
        }

        public [int, int] RetrieveMonths(List<transaction> transactions) {
            
        }
    }
}