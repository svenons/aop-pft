namespace PersonalFinanceTracker {
    class Program {
        private UserInterface UserInterface = new();
        static void Main(string[] args) {
                while(true) {
                UserInterface.InitialiseUI();
            }
            /*IFinance finance = new FinanceTracker();
            
            Transaction transaction1 = new Transaction(DateTime.Now, "transaction1", 1200, Transaction.Category.Income);
            Transaction transaction2 = new Transaction(DateTime.Now, "transaction2", 200, Transaction.Category.Utilities);
            Transaction transaction3 = new Transaction(DateTime.Now, "transaction3", 300, Transaction.Category.Rent);
            Transaction transaction4 = new Transaction(DateTime.Now, "transaction4", 400, Transaction.Category.Groceries);
            finance.AddTransaction(transaction1);
            finance.AddTransaction(transaction2);
            finance.AddTransaction(transaction3);
            finance.AddTransaction(transaction4);

            var transactions = finance.GetTransactions();
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.ID}, {transaction.Date}, {transaction.Description}, {transaction.Amount}, {transaction.TransactionCategory}");
            }

            finance.Save();*/
            
        }
    }
}