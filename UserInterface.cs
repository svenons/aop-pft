using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Transactions;

namespace PersonalFinanceTracker {
    public class UserInterface {

        // Function that initialises and subsequently starts the UI.
        public static IFinance finance = new FinanceTracker();
        public static void InitialiseUI() {
            finance.Load();

            // Setting initial console parameters
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 20);

            // Required on Windows to stop CMD/Powershell from being scrollable, leading to annoying behaviour.
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Console.SetBufferSize(80, 20);
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Calling MainMenu Method to get input to the call next submenu.
            int userInput = MainMenu();
            switch(userInput) {
                case 0:
                    CreateTransactionMenu();
                    break;
                
                case 1:
                ListTransactionsMenu();
                    break;

                case 2:
                    SummaryMenu();
                    break;

                // Case for exiting the application. Resetting console to workable state, and then quitting.
                case 3:
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = true;
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }

        // Method for the Main Menu
        public static int MainMenu() {
            // Setting Parameters               
            int mainMenuWidth = 30;
            int mainMenuHeight = 5;
            int leftIndent = (Console.WindowWidth-mainMenuWidth)/2 -1;
            int topIndent = (Console.WindowHeight-mainMenuHeight)/2 -1;
            
            // --- // Start Writing Window Outlines
            Console.SetCursorPosition(leftIndent + 3, topIndent - 1);
            Console.Write("Personal Finance Tracker");

            Console.SetCursorPosition(leftIndent, topIndent);
            Console.Write('\u2554');
            for(int _ = 0; _ <= mainMenuWidth - 2; ++_) Console.Write('\u2550');
            Console.Write('\u2557');

            Console.SetCursorPosition(leftIndent, topIndent);
            for(int _ = 0; _ <= mainMenuHeight -2; ++_) {
                Console.SetCursorPosition(leftIndent, Console.GetCursorPosition().Top + 1);
                Console.Write('\u2551');
                Console.SetCursorPosition(Console.GetCursorPosition().Left + mainMenuWidth - 1, Console.GetCursorPosition().Top);
                Console.Write('\u2551');
            }

            Console.SetCursorPosition(leftIndent, topIndent + mainMenuHeight);
            Console.Write('\u255A');
            for(int _ = 0; _ <= mainMenuWidth - 2; ++_) Console.Write('\u2550');
            Console.Write('\u255D');
            // --- // End Writing Window Outlines

            // Write Options to the screen
            int currentSelection = 0;
            string[] menuItems = {"Add Transaction", "List Transactions", "Financial Statement", "Exit"};

            // Loop Until Enter Key is pressed to select option
            while(true) {
                for(int _ = 0; _ <= 3; ++_) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + _);
                    Console.Write($"{(_ == currentSelection ? "> " : "")}{menuItems[_]}  ");
                }

                // Input Handler and Menu Navigation
                ConsoleKey userInput = Console.ReadKey().Key;
                switch(userInput) {
                    case ConsoleKey.DownArrow:
                        if(currentSelection == 3) currentSelection = 0;
                        else ++currentSelection;
                        break;
                    
                    case ConsoleKey.UpArrow:
                        if(currentSelection == 0) currentSelection =3;
                        else --currentSelection;
                        break;

                    case ConsoleKey.Enter:
                        return currentSelection;

                    default:
                        break;
                }
            }
        }

        // Method for the Menu used to create a new Transaction
        public static void CreateTransactionMenu() {

            // Setting Parameters
            int mainMenuWidth = 50;
            int mainMenuHeight = 5;
            int leftIndent = (Console.WindowWidth-mainMenuWidth)/2 -1;
            int topIndent = (Console.WindowHeight-mainMenuHeight)/2 -1;

            // --- // Start Writing Window Outlines
            Console.Clear();
            Console.SetCursorPosition(leftIndent + 13, topIndent - 1);
            Console.Write("Personal Finance Tracker");

            Console.SetCursorPosition(leftIndent, topIndent);
            Console.Write('\u2554');
            for(int _ = 0; _ <= mainMenuWidth - 2; ++_) Console.Write('\u2550');
            Console.Write('\u2557');

            Console.SetCursorPosition(leftIndent, topIndent);
            for(int _ = 0; _ <= mainMenuHeight -2; ++_) {
                Console.SetCursorPosition(leftIndent, Console.GetCursorPosition().Top + 1);
                Console.Write('\u2551');
                Console.SetCursorPosition(Console.GetCursorPosition().Left + mainMenuWidth - 1, Console.GetCursorPosition().Top);
                Console.Write('\u2551');
            }

            Console.SetCursorPosition(leftIndent, topIndent + mainMenuHeight);
            Console.Write('\u255A');
            for(int _ = 0; _ <= mainMenuWidth - 2; ++_) Console.Write('\u2550');
            Console.Write('\u255D');
            // --- // End Writing Window Outlines

            // Items to display
            string[] menuItems = {"Description", "Amount", "Category", "Date"};
            string description = "";
            decimal? amount = null;
            DateTime date = new DateTime();
            Transaction.Category? category = null;

            // While True Loop until ALL inputs are handled.
            while(true) {

                // Outer For loop: For handling each input
                for(int _ = 0; _ <= 3; ++_) {
                    // Inner For Loop: Printing menuItems and user inputs to screen
                    for(int __ = 0; __ <= 3; ++__) {
                        Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + __);
                        Console.Write($"{menuItems[__]}: ");

                        switch(__) {
                            case 0:
                                if(description != null) Console.Write(description);
                                break;

                            case 1:
                                if(amount != null) Console.Write($"{amount} Kr");
                                break;

                            case 2:
                                if(category != null) Console.Write(category.ToString());
                                break;
                            
                            case 3:
                                if(date != DateTime.MinValue) Console.Write($"{date.Day}.{date.Month}.{date.Year}, {date.Hour}:{date.Minute}");
                                break;

                            default:
                                break;
                        }
                        
                    }

                    // String used to clear within boundaries of "Window"
                    string clearString = new string(' ', Console.WindowWidth - 2);

                    // Switch for getting user inputs - each input is somewhat different.
                    switch(_) {
                        // Description
                        case 0:
                            while(description == "") {
                                // Clearing bottom of screen, then getting input
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.Write(clearString);
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.CursorVisible = true;
                                Console.Write("Enter Description: ");
                                description = GetInputAtBottom();
                            }
                            // Clearing Error Message
                            Console.SetCursorPosition(0, Console.WindowHeight - 2);
                            Console.Write(clearString);
                            Console.CursorVisible = false;
                            break;
                        
                        // Amount
                        case 1:
                            while(amount == null) {
                                // Clearing bottom of screen, then getting input
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.Write(clearString);
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.CursorVisible = true;
                                Console.Write("Enter Amount: ");
                                string input = "";
                                input= GetInputAtBottom();
                                
                                // Trying to parse to number
                                if(!decimal.TryParse(input, out decimal testAmount)) {
                                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                    Console.Write("Please enter a valid decimal number.");
                                    amount = null;
                                } else amount = testAmount;
                            }
                            Console.CursorVisible = false;
                            break;
                        
                        // Category
                        case 2:
                        // Clearing Bottom of screen, then getting input
                            Console.SetCursorPosition(0, Console.WindowHeight - 2);
                            Console.Write(clearString);
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            Console.Write(clearString);
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            Console.CursorVisible = true;

                            // Input functions identically to Main Menu
                            int count = Enum.GetValues(typeof(Transaction.Category)).Length;
                            int userSelection = 0;
                            Console.CursorVisible = false;
                            while(category == null) {
                                Console.SetCursorPosition(0, Console.WindowHeight - count - 1);
                                Console.Write("Select Category: ");

                                for(int __ = 0; __ < count; ++__) {
                                    Console.SetCursorPosition(0, Console.WindowHeight - count + __);
                                    Console.Write($"{(__ == userSelection ? "> " : "")}{((Transaction.Category)__).ToString()}  ");
                                }

                                ConsoleKey userInput = Console.ReadKey().Key;
                                switch(userInput) {
                                    case ConsoleKey.DownArrow:
                                        if(userSelection == count - 1) userSelection = 0;
                                        else ++userSelection;
                                        break;
                                    
                                    case ConsoleKey.UpArrow:
                                        if(userSelection == 0) userSelection = count - 1;
                                        else --userSelection;
                                        break;

                                    case ConsoleKey.Enter:
                                        category = (Transaction.Category)userSelection;
                                        break;

                                    default:
                                        break;
                                }
                            }
                            // Clearing Up after the category selection
                            for(int __ = 0; __ <= count; ++__) {
                                Console.SetCursorPosition(0, Console.WindowHeight - count - 1 + __);
                                Console.Write(clearString);
                            }
                            break;

                        case 3:
                            while(date == DateTime.MinValue) {
                                // Clearing bottom of screen, then getting input
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.Write(clearString);
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.CursorVisible = true;
                                Console.Write("Enter Date and Time: ");
                                string input = "";
                                input = GetInputAtBottom();

                                bool conversionWorked = DateTime.TryParse(input, out DateTime testDate);
                                if(input.Length != 0 && !conversionWorked) {
                                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                    Console.Write("Enter date and time as dd.MM.yyyy HH:MM (24 Hour), or leave blank for now.");
                                    date = DateTime.MinValue;
                                } else if(input.Length == 0) date = DateTime.Now;
                                else date = testDate;

                            }
                            // Clearing Up
                            Console.SetCursorPosition(0, Console.WindowHeight - 2);
                            Console.Write(clearString);
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            Console.Write(clearString);
                            break;
                        
                        default:
                            break;
                    }
                }

                // Writing Everything again - required to show category at the end before confirming.
                for(int __ = 0; __ <= 3; ++__) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + __);
                    Console.Write($"{menuItems[__]}: ");

                    switch(__) {
                        case 0:
                            Console.Write(description);
                            break;

                        case 1:
                            Console.Write($"{amount} Kr");
                            break;

                        case 2:
                            Console.Write(category.ToString());
                            break;

                        case 3:
                            Console.Write($"{date.Day}.{date.Month}.{date.Year}, {date.Hour}:{date.Minute}");
                            break;

                        default:
                            break;
                    }
                    
                }

                // Save & Discard prompt. Functions identically to Main Menu.
                int currentSelection = 0;
                Console.CursorVisible = false;
                string[] choices = {"Save", "Discard"};
                int? choice = null;
                while(choice == null) {
                    Console.SetCursorPosition(0, Console.WindowHeight - 3);
                    Console.Write("Save Changes?");

                    for(int _ = 0; _ < 2; ++_) {
                        Console.SetCursorPosition(0, Console.WindowHeight - 2 + _);
                        Console.Write($"{(_ == currentSelection ? "> " : "")}{choices[_]}  ");
                    }

                    ConsoleKey userInput = Console.ReadKey().Key;
                    switch(userInput) {
                        case ConsoleKey.DownArrow:
                            if(currentSelection == 1) currentSelection = 0;
                            else ++currentSelection;
                            break;
                        
                        case ConsoleKey.UpArrow:
                            if(currentSelection == 0) currentSelection = 1;
                            else --currentSelection;
                            break;

                        case ConsoleKey.Enter:
                            choice = currentSelection;
                            break;

                        default:
                            break;
                    }
                }
                // If Save
                if(choice == 0) {
                    string passedDescription = "";
                    if(description != null) passedDescription = description;
                    decimal passedAmount = 0;
                    if(amount != null) passedAmount = (decimal)amount;
                    Transaction.Category passedCategory = Transaction.Category.Income;
                    if(category != null) passedCategory = (Transaction.Category)category;
                    
                    Transaction save = new Transaction(date, passedDescription, passedAmount, passedCategory);
                    finance.Load();
                    finance.AddTransaction(save);
                    finance.Save();
                }
                break;
            }
        }

        public static void ListTransactionsMenu() {
            {
        Console.Clear();
        Console.CursorVisible = false; // hiding the cursor

        int selectedIndex = 0;
        var transactions = finance.GetTransactions();
        bool continueRunning = true;

        do
        {
            Console.Clear();
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Console.SetBufferSize(80, 8 + transactions.Count);
            // Display a header for the List of Transactions
            Console.WriteLine("List of Transactions");
            Console.WriteLine("======================\n");

            // Display table headers for transactions
            Console.WriteLine($"{"Date",-12} {"Description",-33} {"Amount",-17} {"Category",-14}");
            Console.WriteLine(new string('-', 80)); // Separator line

            // Loop to display transactions with current selection highlighted
            for (int i = 0; i < transactions.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{transactions[i].Date,-12:dd.MM.yyyy} {transactions[i].Description,-33} {transactions[i].Amount + " Kr.",-18} {transactions[i].TransactionCategory,-14}");

                Console.ResetColor();
            }

            Console.ResetColor();
            Console.WriteLine(new string('-', 80));
            Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to exit.");
            Console.SetCursorPosition(0,0);
            Console.SetCursorPosition(0,5 + selectedIndex + 2);

            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : transactions.Count - 1;
                    if(selectedIndex > 0) Console.SetCursorPosition(0, Console.CursorTop - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex < transactions.Count - 1) ? selectedIndex + 1 : 0;
                    if(selectedIndex < transactions.Count - 1) Console.SetCursorPosition(0, Console.CursorTop + 1);
                    break;
                case ConsoleKey.Enter:
                    // Implement action on selected transaction
                    if(EditOrDeleteTransactionMenu(transactions[selectedIndex])) --selectedIndex;
                    Console.CursorVisible = false;
                    //Console.WriteLine($"Action on transaction: {transactions[selectedIndex].Description}");
                    break;
                case ConsoleKey.Escape:
                    continueRunning = false;
                    break;
            }
        } while (continueRunning);
    }

        }

        public static bool EditOrDeleteTransactionMenu(Transaction selectedTransaction)
        {
            int mainMenuWidth = 50;
            int mainMenuHeight = 7; // Increased by one to accommodate delete option
            int leftIndent = (Console.WindowWidth - mainMenuWidth) / 2 - 1;
            int topIndent = (Console.WindowHeight - mainMenuHeight) / 2 - 3;

            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(leftIndent + 13, topIndent - 1);
            Console.WriteLine("Edit/Delete Transaction");

            // Drawing window outlines as per CreateTransactionMenu
            // Start
            Console.SetCursorPosition(leftIndent, topIndent);
            Console.Write('\u2554'); // Top left corner
            for(int i = 0; i <= mainMenuWidth - 2; ++i) Console.Write('\u2550'); // Top border
            Console.Write('\u2557'); // Top right corner

            for(int i = 0; i <= mainMenuHeight - 2; ++i) {
                Console.SetCursorPosition(leftIndent, Console.CursorTop + 1);
                Console.Write('\u2551'); // Left border
                Console.SetCursorPosition(leftIndent + mainMenuWidth, Console.CursorTop);
                Console.Write('\u2551'); // Right border
            }

            Console.SetCursorPosition(leftIndent, topIndent + mainMenuHeight);
            Console.Write('\u255A'); // Bottom left corner
            for(int i = 0; i <= mainMenuWidth - 2; ++i) Console.Write('\u2550'); // Bottom border
            Console.Write('\u255D'); // Bottom right corner
            // End

            string[] menuItems = { "Description", "Amount", "Category", "Date", "Delete Transaction", "Go Back" };
            string description = selectedTransaction.Description;
            decimal? amount = selectedTransaction.Amount;
            DateTime date = selectedTransaction.Date;
            Transaction.Category? categoryReplace = selectedTransaction.TransactionCategory;

            // Loop for input handling (similar to CreateTransactionMenu)
            int currentSelection = 0;
            bool editing = true;

            while(editing) {
                for(int i = 0; i < menuItems.Length; ++i) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + i);
                    Console.Write(new string(' ', mainMenuWidth - 4));
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + i);
                    if (i <= 3) {
                        Console.Write($"{(i == currentSelection ? "> " : "")}{menuItems[i]}: ");
                    }
                    else {
                        Console.Write($"{(i == currentSelection ? "> " : "")}{menuItems[i]}");
                    }
                    // Display current values
                    switch(i) {
                        case 0:
                            Console.Write(description);
                            break;
                        case 1:
                            Console.Write($"{amount} Kr");
                            break;
                        case 2:
                            Console.Write(categoryReplace.ToString());
                            break;
                        case 3:
                            Console.Write(date.ToString("dd.MM.yyyy HH:mm"));
                            break;
                        case 4:
                            // No additional info needed for delete option
                            break;
                        case 5:
                            break;
                    }
                }

                // Input handling
                var keyread = Console.ReadKey(true).Key;
                switch(keyread) {
                    case ConsoleKey.UpArrow:
                        currentSelection = (currentSelection > 0) ? currentSelection - 1 : menuItems.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        currentSelection = (currentSelection < menuItems.Length - 1) ? currentSelection + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        if(currentSelection <= 3)
                        {
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            Console.Write(new string(' ', Console.WindowWidth - 2));
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            

                            var oldTransaction = selectedTransaction;
                            bool changed = false;
                            if (currentSelection == 0)
                            {
                                while(true) {
                                    Console.CursorVisible = true;
                                    Console.Write($"New {menuItems[currentSelection]} : ");
                                    string replace = GetInputAtBottom();
                                    if(replace == "") {
                                        Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                        Console.Write("Description cannot be empty.");
                                        Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                        continue;
                                    }
                                    selectedTransaction.Description = replace;
                                    changed = true;
                                    break;
                                }
                            }
                            else if (currentSelection == 1)
                            {
                                Console.CursorVisible = true;
                                Console.Write($"New {menuItems[currentSelection]} : ");
                                string replace = GetInputAtBottom();
                                if (decimal.TryParse(replace, out decimal newAmount))
                                {
                                    selectedTransaction.Amount = newAmount;
                                    changed = true;
                                }
                                else
                                {
                                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                    Console.Write("Please enter a valid decimal number.");
                                }
                            }
                            else if (currentSelection == 2)
                            {
                                // Clearing Bottom of screen, then getting input
                                string clearString = new string(' ', Console.WindowWidth - 2);
                                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                Console.Write(clearString);
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.Write(clearString);
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.CursorVisible = true;

                                // Input functions identically to Main Menu
                                int count = Enum.GetValues(typeof(Transaction.Category)).Length;
                                int userSelection = 0;
                                Console.CursorVisible = false;
                                Transaction.Category? categoryReplacement = null;
                                while(categoryReplacement == null) {
                                    Console.SetCursorPosition(0, Console.WindowHeight - count - 1);
                                    Console.Write("Select Category: ");

                                    for(int __ = 0; __ < count; ++__) {
                                        Console.SetCursorPosition(0, Console.WindowHeight - count + __);
                                        Console.Write($"{(__ == userSelection ? "> " : "")}{((Transaction.Category)__).ToString()}  ");
                                    }

                                    ConsoleKey userInput = Console.ReadKey().Key;
                                    switch(userInput) {
                                        case ConsoleKey.DownArrow:
                                            if(userSelection == count - 1) userSelection = 0;
                                            else ++userSelection;
                                            break;
                                        
                                        case ConsoleKey.UpArrow:
                                            if(userSelection == 0) userSelection = count - 1;
                                            else --userSelection;
                                            break;

                                        case ConsoleKey.Enter:
                                            categoryReplacement = (Transaction.Category)userSelection;
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                // Clearing Up after the category selection
                                for(int __ = 0; __ <= count; ++__) {
                                    Console.SetCursorPosition(0, Console.WindowHeight - count - 1 + __);
                                    Console.Write(clearString);
                                }
                                selectedTransaction.TransactionCategory = (Transaction.Category)categoryReplacement;
                                changed = true;
                            }
                            else if (currentSelection == 3)
                            {
                                Console.CursorVisible = true;
                                Console.Write($"New {menuItems[currentSelection]} : ");
                                string replace = GetInputAtBottom();
                                if (DateTime.TryParse(replace, out DateTime newDate))
                                {
                                    selectedTransaction.Date = newDate;
                                    changed = true;
                                }
                                else
                                {
                                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                    Console.Write("Please enter a valid date and time.");
                                }
                            }

                            if (finance.editTransaction(oldTransaction, selectedTransaction) && changed)
                            {
                                finance.Save();
                                editing = false;
                            }
                            break;
                        }
                        else if (currentSelection == 4)
                        {
                            if (finance.RemoveTransaction(selectedTransaction))
                            {
                                finance.Save();
                                editing = false;
                            }
                            return true;
                        }
                        else if (currentSelection == 5) {
                            editing = false;
                            break;
                        }
                        break;
                    case ConsoleKey.Escape:
                        editing = false;
                        break;
                }
            }
            return false;

            // Implement the logic to edit fields based on currentSelection
            // and confirm deletion if "Delete Transaction" is selected.
        }


        public static void SummaryMenu() {
            ChooseYearMenu();
        }

        public static void ChooseYearMenu() {
            List<int> Date = new List<int>();
            Console.Clear();
            Console.CursorVisible = false;

            Summary summary = new Summary();
            List<Transaction> transactions = finance.GetTransactions();
            List<int> years = summary.RetrieveYears(transactions);

            if (years.Count == 0) {
                return; // Exit if there are no transactions, lol
            }
            // Add "All" option if there are multiple months
            else if (years.Count > 1) {
                years.Insert(0, 0); // Insert "All" at the beginning represented by 0
            }
            else if (years.Count == 1)
            {
                Date.Add(years[0]);
                ChooseMonthMenu(Date);
                return;
            }

            int selectedIndex = 0;
            int menuWidth = 30;
            int menuHeight = years.Count + 2;
            int leftIndent = (Console.WindowWidth - menuWidth) / 2;
            int topIndent = (Console.WindowHeight - menuHeight) / 2;

            // Drawing the window outline
            Console.SetCursorPosition(leftIndent, topIndent);
            Console.Write('\u2554'); // Top left corner
            for (int i = 0; i < menuWidth - 2; i++) Console.Write('\u2550'); // Top border
            Console.Write('\u2557'); // Top right corner

            for (int i = 0; i < menuHeight - 2; i++) {
                Console.SetCursorPosition(leftIndent, topIndent + 1 + i);
                Console.Write('\u2551'); // Left border
                Console.SetCursorPosition(leftIndent + menuWidth - 1, topIndent + 1 + i);
                Console.Write('\u2551'); // Right border
            }

            Console.SetCursorPosition(leftIndent, topIndent + menuHeight - 1);
            Console.Write('\u255A'); // Bottom left corner
            for (int i = 0; i < menuWidth - 2; i++) Console.Write('\u2550'); // Bottom border
            Console.Write('\u255D'); // Bottom right corner

            // Displaying the years and navigating through them
            bool continueRunning = true;
            do {
                for (int i = 0; i < years.Count; i++) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + i);
                    if (i == selectedIndex) {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    // Display "All" for 0, or the month name for others using CultureInfo
                    string displayText = years[i] == 0 ? "All" : years[i].ToString();
                    Console.Write($"{(i == selectedIndex ? "> " : "  ")}{displayText}".PadRight(menuWidth - 4));
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key; // Read the key without displaying it
                switch (key) {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : years.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex < years.Count - 1) ? selectedIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        continueRunning = false; // Exit the loop on Enter
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            } while (continueRunning);

            int selectedYear = years[selectedIndex];
            if (selectedYear == 0) {
                MakeSummary(Date);
                return;
            }
            Date.Add(selectedYear);
            Console.Clear();

            ChooseMonthMenu(Date);
        }

        public static void ChooseMonthMenu(List<int> Date) {
            Console.Clear();
            int selectedYear = Date[0];
            Console.CursorVisible = false;

            Summary summary = new Summary();
            List<Transaction> transactions = finance.GetTransactions();
            List<int> monthList = summary.RetrieveMonths(transactions, selectedYear);

            // Add "All" option if there are multiple months
            if (monthList.Count > 1) {
                monthList.Insert(0, 0); // Insert "All" at the beginning represented by 0
            }
            else if (monthList.Count == 1) // If only 1 choice, skip to the day menu
            {
                Date.Add(monthList[0]);
                ChooseDayMenu(Date);
                return;
            }

            int selectedIndex = 0;
            int menuWidth = 30;
            int menuHeight = monthList.Count + 2;
            int leftIndent = (Console.WindowWidth - menuWidth) / 2;
            int topIndent = (Console.WindowHeight - menuHeight) / 2;

            // Drawing the window outline
            Console.SetCursorPosition(leftIndent, topIndent);
            Console.Write('\u2554'); // Top left corner
            for (int i = 0; i < menuWidth - 2; i++) Console.Write('\u2550'); // Top border
            Console.Write('\u2557'); // Top right corner

            for (int i = 0; i < menuHeight - 2; i++) {
                Console.SetCursorPosition(leftIndent, topIndent + 1 + i);
                Console.Write('\u2551'); // Left border
                Console.SetCursorPosition(leftIndent + menuWidth - 1, topIndent + 1 + i);
                Console.Write('\u2551'); // Right border
            }

            Console.SetCursorPosition(leftIndent, topIndent + menuHeight - 1);
            Console.Write('\u255A'); // Bottom left corner
            for (int i = 0; i < menuWidth - 2; i++) Console.Write('\u2550'); // Bottom border
            Console.Write('\u255D'); // Bottom right corner

            // Displaying "All" and the months, and navigating through them
            bool continueRunning = true;
            do {
                for (int i = 0; i < monthList.Count; i++) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + i);
                    if (i == selectedIndex) {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    // Display "All" for 0, or the month name for others using CultureInfo
                    string displayText = monthList[i] == 0 ? "All" : CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthList[i]);
                    Console.Write($"{(i == selectedIndex ? "> " : "  ")}{displayText}".PadRight(menuWidth - 4));
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key) {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : monthList.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex < monthList.Count - 1) ? selectedIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        continueRunning = false; // Exit the loop on Enter
                        break;
                    case ConsoleKey.Escape:
                        return; // Optional: Allow exiting the menu without a selection
                }
             } while (continueRunning);

            int selectedMonth = monthList[selectedIndex]; // This will be the actual month number or 0 for "All"
            if (selectedMonth == 0) {
                MakeSummary(Date);
                return;
            }
            Date.Add(selectedMonth);
            Console.Clear();

            ChooseDayMenu(Date);
        }

        public static void ChooseDayMenu(List<int> Date) {
            Console.Clear();
            Console.CursorVisible = false;
            int selectedYear = Date[0];
            int selectedMonth = Date[1];

            Summary summary = new Summary();
            List<Transaction> transactions = finance.GetTransactions();
            List<int> dayList = summary.RetrieveDays(transactions, selectedYear, selectedMonth);

            // Add "All" option if there are multiple days
            if (dayList.Count > 1) {
                dayList.Insert(0, 0); // Insert "All" at the beginning represented by 0
            }
            else if (dayList.Count == 1)
            {
                Date.Add(dayList[0]);
                MakeSummary(Date);
                return;
            } 

            int selectedIndex = 0; // Default selection index
            int menuWidth = 30;
            int menuHeight = dayList.Count + 2; // Adjust height based on the day list
            int leftIndent = (Console.WindowWidth - menuWidth) / 2;
            int topIndent = (Console.WindowHeight - menuHeight) / 2;

            // Drawing the window outline, at this point im just copying this.....
            Console.SetCursorPosition(leftIndent, topIndent);
            Console.Write('\u2554'); // Top left corner
            for (int i = 0; i < menuWidth - 2; i++) Console.Write('\u2550'); // Top border
            Console.Write('\u2557'); // Top right corner

            for (int i = 0; i < menuHeight - 2; i++) {
                Console.SetCursorPosition(leftIndent, topIndent + 1 + i);
                Console.Write('\u2551'); // Left border
                Console.SetCursorPosition(leftIndent + menuWidth - 1, topIndent + 1 + i);
                Console.Write('\u2551'); // Right border
            }

            Console.SetCursorPosition(leftIndent, topIndent + menuHeight - 1);
            Console.Write('\u255A'); // Bottom left corner
            for (int i = 0; i < menuWidth - 2; i++) Console.Write('\u2550'); // Bottom border
            Console.Write('\u255D'); // Bottom right corner

            // Displaying "All" (if applicable) and the days, and navigating through them
            bool continueRunning = true;
            do {
                for (int i = 0; i < dayList.Count; i++) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + i);
                    if (i == selectedIndex) {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    // Display "All" for 0, or the day number for others
                    string displayText = dayList[i] == 0 ? "All" : dayList[i].ToString();
                    Console.Write($"{(i == selectedIndex ? "> " : "  ")}{displayText}".PadRight(menuWidth - 4));
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true).Key;
                switch (key) {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : dayList.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex < dayList.Count - 1) ? selectedIndex + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        continueRunning = false; // Exit the loop on Enter
                        break;
                    case ConsoleKey.Escape:
                        return; // Optional: Allow exiting the menu without a selection
                }
            } while (continueRunning);

            int selectedDay = dayList[selectedIndex]; // This will be the actual day number or 0 for "All"
            Console.Clear();

            if (selectedDay == 0) {
                MakeSummary(Date);
                return;
            }
            Date.Add(selectedDay);

            MakeSummary(Date);
        }

        public static void MakeSummary(List<int> Date) {
            Console.Clear();
            Console.CursorVisible = false;

            Summary summary = new Summary();
            var summaryResult = summary.GenerateSummary(Date, finance.GetTransactions());
            string displayString = summaryResult.Item1;
            Dictionary<Transaction.Category, List<Transaction>> infoDict = summaryResult.Item2;

            Console.WriteLine(displayString);
            foreach (var category in infoDict.Keys) {
                if (infoDict[category].Count > 0) {
                    Console.WriteLine($"\n{category} Transactions:");
                    foreach (var transaction in infoDict[category]) {
                        Console.WriteLine($"{transaction.Date.ToString("dd.MM.yyyy HH:mm")} - {transaction.Description} - {transaction.Amount} Kr.");
                    }
                }
            }
            Console.WriteLine("\nPress ESC to return to the main menu.");
            Console.ReadKey(true);
        }

        // Own quick Console.ReadLine() implementation:
        //  Limits to 32 characters, thus preventing breaking a line break if the input is too long.
        //  Also prevents the line break when pressing Enter in Console.ReadLine() that occurs in the terminal
        //      and shifts the entire screen up.
        private static string GetInputAtBottom() {
            string input = "";
            while(true) {
                ConsoleKeyInfo inputChar = Console.ReadKey(true);
                if(inputChar.Key == ConsoleKey.Backspace && input.Length != 0) {
                    Console.SetCursorPosition(Console.CursorLeft -1, Console.CursorTop);
                    Console.Write(' ');
                    Console.SetCursorPosition(Console.CursorLeft -1, Console.CursorTop);
                    input = input.Remove(input.Length - 1, 1);
                }
                else if(inputChar.Key != ConsoleKey.Enter && inputChar.Key != ConsoleKey.Backspace) {
                    if(input.Length <= 31) {
                        Console.Write(inputChar.KeyChar);
                        input += inputChar.KeyChar;
                    }
                } else if(inputChar.Key == ConsoleKey.Backspace) {}
                else break;
            }
            return input;
        }
    }
}