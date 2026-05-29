using NetBank.Models;

namespace NetBank.Data;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        if (db.Users.Any()) return;
        var admin = new User { Name = "Admin", Email = "admin@netbank.com", Role = "Admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") };
        var user = new User { Name = "Deepak Singh", Email = "deepak@netbank.com", Role = "User", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123") };
        db.Users.AddRange(admin, user); db.SaveChanges();
        db.Accounts.Add(new Account { UserId = user.UserId, AccountNumber = "1001001001", Balance = 50000, AccountType = "Savings" });
        db.SaveChanges();
    }
}
