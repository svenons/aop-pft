# Project: `Personal Finance Tracker`
An A-OOP assignment.

### Objective: `Develop a console application that allows users to track personal finances over time. `

The application will enable users to:
- record income and expenses
- categorize them
- view summaries of their financial activity. 
- Data persistence will be achieved through JSON files, allowing users to maintain their financial records across sessions.
The project also requires students to use collaborative coding practices using Git.

### Required Components:
##### IFinance Interface
- Specifies methods for adding, viewing, and categorizing transactions.
##### IFinanceStorage Interface
- Specifies methods for loading and saving transaction data.
##### Transaction Class
- Properties: Transaction ID (Guid), Date (DateTime), Description (string), Amount (decimal), Category (string, e.g., "Income", "Groceries", "Utilities").
- ID should use the type Guid (i.e., public Guid ID {get; set; } = Guid.NewGuid();
- Constructor initializes a transaction with the provided details.
##### FinanceTracker Class
- Implements the IFinance interface, managing a list of Transaction objects. It includes functionality to add transactions, categorize them, and provide summaries (e.g., total income, total expenses, balance).
##### JsonFinanceStorage Class
- Implementes the IFinanceStorage interface, focusing on handling persistence by reading from and writing to a transactions.json JSON file, managing serialization and deserialization of Transaction objects.
##### Program Class (User Interface)
- Provides a console-based UI that allows users to interact with the finance tracker. Users can add new transactions, view transaction history, and see financial summaries based on categories or overall.

### Technical Requirements:
- JSON Data Persistence: Use System.Text.Json for handling transaction data in JSON format, ensuring the application can save and load user data effectively.
- Simple Financial Summaries: Implement methods in FinanceTracker to calculate total income, total expenses, and current balance, allowing users to get quick insights into their financial status.
- Exception Handling and Input Validation: Robustly handle potential errors, especially related to file I/O and user input, to prevent data corruption and ensure a user-friendly experience.
- Git for Collaborative Coding: Students are required to use Git for version control, managing their project code within a shared repository. This involves regular commits, branching for feature development, merging changes, and resolving any potential conflicts.

### Example Workflow:
- The application starts by loading existing transaction data from transactions.json. If the file does not exist, it initializes with an empty list of transactions.
- Users are presented with a menu to add a transaction, view all transactions, view financial summaries, or exit the application.
- Adding a transaction prompts the user for details (date, description, amount, category), which are then saved to the transaction list.
- Viewing transactions displays the list with options to filter by category.
- Financial summaries provide an overview of income, expenses, and the current balance.
- Exiting the application triggers saving the current state of transactions to the JSON file, ensuring no data loss.


## Authors

- [@Nick](https://github.com/nicki419)
- [@Norrokas](https://github.com/Norrokas)
- [@svenons](https://github.com/svenons/)


## Acknowledgements

 - This exercise is part of Advanced Object Oriented Programming on [Software Engineering by SDU](https://mitsdu.dk/en/mit_studie/bachelor/softwareengineering_bachelor_soenderborg)
 - Created by [Dr. Maximus Kaos](https://github.com/MaxDKaos/) and [Maximilian von Zastrow](https://github.com/vzastrow)

