using System.Runtime.InteropServices;

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
                    break;

                case 2:
                    break;

                case 3:
                    break;

                // Case for exiting the application. Resetting console to workable state, and then quitting.
                case 4:
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
            int mainMenuHeight = 6;
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
            string[] menuItems = {"Add Transaction", "Remove Transaction", "List Transactions", "Financial Statement", "Exit"};

            // Loop Until Enter Key is pressed to select option
            while(true) {
                for(int _ = 0; _ <= 4; ++_) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + _);
                    Console.Write($"{(_ == currentSelection ? "> " : "")}{menuItems[_]}  ");
                }

                // Input Handler and Menu Navigation
                ConsoleKey userInput = Console.ReadKey().Key;
                switch(userInput) {
                    case ConsoleKey.DownArrow:
                        if(currentSelection == 4) currentSelection = 0;
                        else ++currentSelection;
                        break;
                    
                    case ConsoleKey.UpArrow:
                        if(currentSelection == 0) currentSelection =4;
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
                                    Console.Write("Enter date and time as YYYY-MM-DD HH:MM (24 Hour), or leave blank for now.");
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

        public static void DeleteTransactionMenu() {

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