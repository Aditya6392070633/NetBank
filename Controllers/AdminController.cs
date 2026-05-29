using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBank.Data;

namespace NetBank.Controllers;

public class AdminController : BaseController
{
    private readonly ApplicationDbContext _db;
    public AdminController(ApplicationDbContext db)=>_db=db;
    private bool IsAdmin => CurrentRole == "Admin";
    public IActionResult Index(){ if(!IsAdmin) return LoginRedirect(); ViewBag.Users=_db.Users.Count(); ViewBag.Accounts=_db.Accounts.Count(); ViewBag.Transactions=_db.Transactions.Count(); ViewBag.Loans=_db.Loans.Count(l=>l.Status=="Pending"); return View(); }
    public IActionResult Users(){ if(!IsAdmin) return LoginRedirect(); return View(_db.Users.Include(u=>u.Accounts).ToList()); }
    public IActionResult Transactions(){ if(!IsAdmin) return LoginRedirect(); return View(_db.Transactions.OrderByDescending(t=>t.Date).ToList()); }
    public IActionResult Loans(){ if(!IsAdmin) return LoginRedirect(); return View(_db.Loans.Include(l=>l.User).OrderByDescending(l=>l.AppliedOn).ToList()); }
    [HttpPost]
    public IActionResult UpdateLoan(int id,string status){ if(!IsAdmin) return LoginRedirect(); var loan=_db.Loans.Find(id); if(loan!=null){loan.Status=status; _db.SaveChanges();} return RedirectToAction("Loans"); }
}
