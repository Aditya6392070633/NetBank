using Microsoft.AspNetCore.Mvc;
using NetBank.Data;
using NetBank.Models;

namespace NetBank.Controllers;

public class AccountController : BaseController
{
    private readonly ApplicationDbContext _db;
    public AccountController(ApplicationDbContext db) => _db = db;
    public IActionResult Dashboard()
    {
        if (!IsLoggedIn) return LoginRedirect();
        var accounts = _db.Accounts.Where(x => x.UserId == CurrentUserId).ToList();
        ViewBag.TotalTransactions = _db.Transactions.Count(t => accounts.Select(a=>a.AccountNumber).Contains(t.FromAccount) || accounts.Select(a=>a.AccountNumber).Contains(t.ToAccount));
        ViewBag.PendingLoans = _db.Loans.Count(x => x.UserId == CurrentUserId && x.Status == "Pending");
        return View(accounts);
    }
    [HttpPost]
    public IActionResult OpenAccount(string accountType)
    {
        if (!IsLoggedIn) return LoginRedirect();
        _db.Accounts.Add(new Account{UserId=CurrentUserId!.Value, AccountNumber=DateTime.Now.Ticks.ToString()[^10..], AccountType=accountType, Balance=0});
        _db.SaveChanges(); return RedirectToAction("Dashboard");
    }
    [HttpPost]
    public IActionResult CloseAccount(int id)
    {
        var acc = _db.Accounts.FirstOrDefault(x => x.AccountId == id && x.UserId == CurrentUserId);
        if (acc != null && acc.Balance == 0) { acc.IsActive = false; _db.SaveChanges(); }
        return RedirectToAction("Dashboard");
    }
}
