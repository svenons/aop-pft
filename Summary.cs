using System.Globalization;

namespace PersonalFinanceTracker
{
    public class Summary
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public Summary() {}

        public List<int> RetrieveYears(List<Transaction> transactions)
        {
            List<int> years = transactions.Select(t => t.Date.Year).Distinct().ToList();
            years.Sort();
            return years;
        }

        public List<int> RetrieveMonths(List<Transaction> transactions, int year)
        {
            List<int> months = new List<int>();

            // Add all unique months to the list
            foreach (Transaction _ in transactions)
            {
                if (_.Date.Year == year && !months.Contains(_.Date.Month))
                {
                    months.Add(_.Date.Month);
                }
            }

            // Sort the list in ascending order
            months.Sort();

            return months;
        }

        public List<int> RetrieveDays(List<Transaction> transactions, int year, int month)
        {
            List<int> days = new List<int>();

            foreach (Transaction _ in transactions)
            {
                if (_.Date.Year == year && _.Date.Month == month && !days.Contains(_.Date.Day))
                {
                    days.Add(_.Date.Day);
                }
            }

            days.Sort();

            return days;
        }

        public List<Transaction> RetrieveYearlySummaryItems(List<Transaction> transactions, int year) {
            List<Transaction> returnedTransactions = new List<Transaction>();
            foreach(Transaction _ in transactions) {
                if(_.Date.Year == year) returnedTransactions.Add(_);
            }
            return returnedTransactions;
        }

        public List<Transaction> RetrieveMonthlySummaryItems(List<Transaction> transactions, List<int> date) {
            List<Transaction> returnedTransactions = new List<Transaction>();
            foreach(Transaction _ in transactions) {
                if(_.Date.Month == date[1] && _.Date.Year == date[0]) returnedTransactions.Add(_);
            }
            return returnedTransactions;
        }

        public List<Transaction> RetrieveDailySummaryItems(List<Transaction> transactions, List<int> date) {
            List<Transaction> returnedTransactions = new List<Transaction>();
            foreach(Transaction _ in transactions) {
                if(_.Date.Day == date[2] && _.Date.Month == date[1] && _.Date.Year == date[0]) returnedTransactions.Add(_);
            }
            return returnedTransactions;
        }
    
        public (string, Dictionary<Transaction.Category, List<Transaction>>) GenerateSummary(List<int> date, List<Transaction> transactions)
        {
            Dictionary<Transaction.Category, List<Transaction>> transactionsByCategory = new Dictionary<Transaction.Category, List<Transaction>>();
            for (int _ = 0; _ < Enum.GetValues(typeof(Transaction.Category)).Length; ++_)
            {
                transactionsByCategory.Add((Transaction.Category)_, new List<Transaction>());
                foreach (Transaction __ in transactions)
                {
                    if (__.TransactionCategory == (Transaction.Category)_ && IsWithinDateRange(__.Date, date))
                    {
                        transactionsByCategory[(Transaction.Category)_].Add(__);
                    }
                }
            }

            string returnString = GenerateSummaryString(date);
            return (returnString, transactionsByCategory);
        }

        private bool IsWithinDateRange(DateTime transactionDate, List<int> date)
        {
            switch (date.Count())
            {
                case 0:
                    return true;

                case 1:
                    return transactionDate.Year == date[0];

                case 2:
                    return transactionDate.Year == date[0] && transactionDate.Month == date[1];

                case 3:
                    return transactionDate.Year == date[0] && transactionDate.Month == date[1] && transactionDate.Day == date[2];

                default:
                    return false;
            }
        }

        private string GenerateSummaryString(List<int> date)
        {
            string returnString = "";
            switch (date.Count())
            {
                case 0:
                    returnString = "Summary for all transactions in database:";
                    break;

                case 1:
                    returnString = $"Summary for all transactions in {date[0]}";
                    break;

                case 2:
                    returnString = $"Summary for all transactions in {DateTimeFormatInfo.CurrentInfo.GetMonthName(date[1])}, {date[0]}";
                    break;

                case 3:
                    returnString = $"Summary for all transactions on {DateTimeFormatInfo.CurrentInfo.GetMonthName(date[1])} {date[2]}, {date[0]}";
                    break;
            }
            return returnString;
        }
    }
}