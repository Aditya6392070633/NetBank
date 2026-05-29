using Microsoft.AspNetCore.Mvc;
using NetBank.Data;
using NetBank.Models;

namespace NetBank.Controllers;

public class LoanController : BaseController
{
    private readonly ApplicationDbContext _db;
    public LoanController(ApplicationDbContext db)=>_db=db;
    public IActionResult Index() => IsLoggedIn ? View(_db.Loans.Where(l=>l.UserId==CurrentUserId).OrderByDescending(l=>l.AppliedOn).ToList()) : LoginRedirect();
    [HttpPost]
    public IActionResult Apply(decimal amount){ if(!IsLoggedIn) return LoginRedirect(); _db.Loans.Add(new Loan{UserId=CurrentUserId!.Value, Amount=amount}); _db.SaveChanges(); return RedirectToAction("Index"); }
}
