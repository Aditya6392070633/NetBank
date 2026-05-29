# NetBank — Online Banking Management System

Complete ASP.NET Core MVC interview project with Razor Views, Bootstrap 5, EF Core Code First, SQL Server, BCrypt password hashing, user/admin modules, fund transfer, beneficiaries, transaction history, and loan approval.

## How to Run

1. Install .NET 8 SDK and SQL Server/LocalDB.
2. Open this folder in Visual Studio or VS Code.
3. Check `appsettings.json` connection string.
4. Run:

```bash
dotnet restore
dotnet run
```

The app auto-creates database using `EnsureCreated()` and seeds demo data.

## Demo Login

User: `deepak@netbank.com` / `user123`

Admin: `admin@netbank.com` / `admin123`

## Interview Points

- MVC separation: Models, Controllers, Razor Views
- EF Core Code First with relationships
- BCrypt hashed passwords
- Session-based authentication
- Role-based admin/user workflow
- Transaction validation: source account, receiver account, and insufficient balance
- Loan approval workflow
- Bootstrap responsive UI

## Main Modules

- Auth: Register, Login, Logout
- Account: Dashboard, balance, open/close account
- Transaction: Fund transfer, history with date filter
- Beneficiary: Add and view beneficiaries
- Loan: Apply and check status
- Admin: Users, accounts, loans, all transactions
