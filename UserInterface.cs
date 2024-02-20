using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace PersonalFinanceTracker {
    public class UserInterface {

        public static void InitialiseUI() {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(80, 20);
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) Console.SetBufferSize(80, 20);
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

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

                case 4:
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = true;
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
            //Console.WriteLine($"W: {windowWidth}\nH: {windowHeight}");
        }
        public static int MainMenu() {
            int mainMenuWidth = 30;
            int mainMenuHeight = 6;
            int leftIndent = (Console.WindowWidth-mainMenuWidth)/2 -1;
            int topIndent = (Console.WindowHeight-mainMenuHeight)/2 -1;
            
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

            int currentSelection = 0;
            string[] menuItems = {"Add Transaction", "Remove Transaction", "List Transactions", "Financial Statement", "Exit"};
            while(true) {
                for(int _ = 0; _ <= 4; ++_) {
                    Console.SetCursorPosition(leftIndent + 2, topIndent + 1 + _);
                    Console.Write($"{(_ == currentSelection ? "> " : "")}{menuItems[_]}  ");
                }

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

        public static void CreateTransactionMenu() {
            int mainMenuWidth = 50;
            int mainMenuHeight = 4;
            int leftIndent = (Console.WindowWidth-mainMenuWidth)/2 -1;
            int topIndent = (Console.WindowHeight-mainMenuHeight)/2 -1;

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

            string[] menuItems = {"Description", "Amount", "Category"};
            string description = "";
            decimal? amount = null;
            Transaction.Category? category = null;

            while(true) {
                for(int _ = 0; _ <= 2; ++_) {
                    for(int __ = 0; __ <= 2; ++__) {
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

                            default:
                                break;
                        }
                        
                    }

                    string clearString = new string(' ', Console.WindowWidth - 2);
                    switch(_) {
                        case 0:
                            while(description == "") {
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.Write(clearString);
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.CursorVisible = true;
                                Console.Write("Enter Description: ");
                                description = GetInputAtBottom();
                            }
                            Console.SetCursorPosition(0, Console.WindowHeight - 2);
                            Console.Write(clearString);
                            Console.CursorVisible = false;
                            break;
                        
                        case 1:
                            while(amount == null) {
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.Write(clearString);
                                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                                Console.CursorVisible = true;
                                Console.Write("Enter Amount: ");
                                string input = "";
                                input= GetInputAtBottom();
                                
                                if(!Int32.TryParse(input, out int testAmount)) {
                                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                    Console.Write("Please enter a valid number.");
                                    amount = null;
                                } else amount = testAmount;
                            }
                            Console.CursorVisible = false;
                            break;
                        
                        case 2:
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            Console.Write(clearString);
                            Console.SetCursorPosition(0, Console.WindowHeight - 1);
                            Console.CursorVisible = true;

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
                            for(int __ = 0; __ <= count; ++__) {
                                Console.SetCursorPosition(0, Console.WindowHeight - count - 1 + __);
                                Console.Write(clearString);
                            }
                            break;
                        
                        default:
                            break;
                    }
                }
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
                break;
            }
        }

        public static void DeleteTransactionMenu() {

        }
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
                }
                else break;
            }
            return input;
        }
    }
}