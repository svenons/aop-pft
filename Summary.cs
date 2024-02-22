using System.Globalization;

namespace PersonalFinanceTracker
{
    public class Summary
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public Summary() {}

        public List<int> RetrieveYears(List<Transaction> transactions) {
            List<int> years = new List<int>();
            
            foreach(Transaction _ in transactions){
                int Year = _.Date.Year;
                if(!years.Contains(Year)){
                    years.Add(Year);
                }
            }

            int i, j;
            int temp;
            bool swapped;
            for(i = 0; i < years.Count; ++i){
                swapped = false;
                for(j = 0; j < years.Count - i - 1; j++){
                    if(years[j] > years[j + 1]){
                        temp = years[j];
                        years[j] = years[j + 1];
                        years[j + 1] = temp;
                        swapped = true;
                    }
                    
                }
                if(!swapped) break;
            }
            return years;
        }

        public List<int[]> RetrieveMonths(List<Transaction> transactions) {
            List<int[]> months = new List<int[]>();
            
            // Add all combinations of months and years to the list
            foreach(Transaction _ in transactions) {
                int[] monthYearCombo = {_.Date.Month, _.Date.Year};
                if(!months.Contains(monthYearCombo)) {
                    months.Add(monthYearCombo);
                }
            }

            // Sort the list by year, and then by month ascending. (Modified Bubble sort)
            int i, j;
            int[] temp;
            bool swapped;
            for(i = 0; i < months.Count; ++i) {
                swapped = false;
                for(j = 0; j < months.Count - i - 1; ++j) {
                    // Concencating YYYY and MM to one integer is the easiest way to compare.
                    string testString1 = months[j][1].ToString() + ((months[j][0] >= 10) ? "" : "0") + months[j][0].ToString();
                    string testString2 = months[j + 1][1].ToString() + ((months[j + 1][0] >= 10) ? "" : "0") + months[j + 1][0].ToString();
                    if(int.Parse(testString1) > int.Parse(testString2)) {
                        temp = months[j];
                        months[j] = months[j + 1];
                        months[j + 1] = temp;
                        swapped = true;
                    }
                }
                if(!swapped) break;
            }
            return months;
        }

        public List<int[]> RetrieveDays(List<Transaction> transactions) {
            List<int[]> days = new List<int[]>();
            
            // Add all combinations of months and years to the list
            foreach(Transaction _ in transactions) {
                int[] DayMonthYearCombo = {_.Date.Day, _.Date.Month, _.Date.Year};
                if(!days.Contains(DayMonthYearCombo)) {
                    days.Add(DayMonthYearCombo);
                }
            }

            // Sort the list by year, and then by month ascending. (Modified Bubble sort)
            int i, j;
            int[] temp;
            bool swapped;
            for(i = 0; i < days.Count; ++i) {
                swapped = false;
                for(j = 0; j < days.Count - i - 1; ++j) {
                    // Concencating YYYY and MM to one integer is the easiest way to compare.
                    string testString1 = days[j][2].ToString() + ((days[j][1] >= 10) ? "" : "0") + days[j][1].ToString() + ((days[j][0] >= 10) ? "" : "0") + days[j][0].ToString();
                    string testString2 = days[j + 1][2].ToString() + ((days[j + 1][1] >= 10) ? "" : "0") + days[j + 1][1].ToString() + ((days[j + 1][0] >= 10) ? "" : "0") + days[j + 1][0].ToString();
                    if(int.Parse(testString1) > int.Parse(testString2)) {
                        temp = days[j];
                        days[j] = days[j + 1];
                        days[j + 1] = temp;
                        swapped = true;
                    }
                }
                if(!swapped) break;
            }
            return days;
        }

        public List<Transaction> RetrieveYearlySummaryItems(List<Transaction> transactions, int year) {
            List<Transaction> returnedTransactions = new List<Transaction>();
            foreach(Transaction _ in transactions) {
                if(_.Date.Year == year) returnedTransactions.Add(_);
            }
            return returnedTransactions;
        }

        public List<Transaction> RetrieveMonthlySummaryItems(List<Transaction> transactions, int[] date) {
            List<Transaction> returnedTransactions = new List<Transaction>();
            foreach(Transaction _ in transactions) {
                if(_.Date.Month == date[0] && _.Date.Year == date[1]) returnedTransactions.Add(_);
            }
            return returnedTransactions;
        }

        public List<Transaction> RetrieveDailySummaryItems(List<Transaction> transactions, int[] date) {
            List<Transaction> returnedTransactions = new List<Transaction>();
            foreach(Transaction _ in transactions) {
                if(_.Date.Day == date[0] && _.Date.Month == date[1] && _.Date.Year == date[2]) returnedTransactions.Add(_);
            }
            return returnedTransactions;
        }
    
        public (string, Dictionary<Transaction.Category, List<Transaction>>) GenerateSummary(int[] date, List<Transaction> transactions) {
            Dictionary<Transaction.Category, List<Transaction>> transactionsByCategory = new Dictionary<Transaction.Category, List<Transaction>>();
            for(int _ = 0; _ < Enum.GetValues(typeof(Transaction.Category)).Length; ++_){
                transactionsByCategory.Add((Transaction.Category)_, new List<Transaction>());
                foreach(Transaction __ in transactions) {
                    if(__.TransactionCategory == (Transaction.Category)_) transactionsByCategory[(Transaction.Category)_].Add(__);
                }
            }
            string returnString = "";
            switch(date.Count()) {
                case 0:
                    returnString = "Summary for all transactions in database:";
                    break;
                
                case 1:
                    returnString = $"Summary for all transactions in {date[0]}";
                    break;
                
                case 2:
                    returnString = $"Summary for all transactions in {DateTimeFormatInfo.CurrentInfo.GetMonthName(date[0])}, {date[1]}";
                    break;
                
                case 3:
                    returnString = $"Summary for all transactions on {DateTimeFormatInfo.CurrentInfo.GetMonthName(date[1])} {date[0]}, {date[2]}"; 
                    break;
            }
            return (returnString, transactionsByCategory);
        }
    }
}