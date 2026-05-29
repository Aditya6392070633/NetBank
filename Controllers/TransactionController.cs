using Microsoft.AspNetCore.Mvc;
using NetBank.Data;
using NetBank.Models;

namespace NetBank.Controllers;

public class TransactionController : BaseController
{
    private readonly ApplicationDbContext _db;
    public TransactionController(ApplicationDbContext db) => _db = db;
    public IActionResult Transfer() => IsLoggedIn ? View(new TransferVm{FromAccount=_db.Accounts.FirstOrDefault(a=>a.UserId==CurrentUserId && a.IsActive)?.AccountNumber ?? ""}) : LoginRedirect();
    [HttpPost]
    public IActionResult Transfer(TransferVm vm)
    {
        if (!IsLoggedIn) return LoginRedirect();
        var from = _db.Accounts.FirstOrDefault(a => a.AccountNumber == vm.FromAccount && a.UserId == CurrentUserId && a.IsActive);
        var to = _db.Accounts.FirstOrDefault(a => a.AccountNumber == vm.ToAccount && a.IsActive);
        if (from == null) ModelState.AddModelError("FromAccount", "Invalid source account");
        if (to == null) ModelState.AddModelError("ToAccount", "Receiver account not found");
        if (from != null && from.Balance < vm.Amount) ModelState.AddModelError("Amount", "Insufficient balance");
        if (!ModelState.IsValid) return View(vm);
        from!.Balance -= vm.Amount; to!.Balance += vm.Amount;
        _db.Transactions.Add(new Transaction{FromAccount=vm.FromAccount, ToAccount=vm.ToAccount, Amount=vm.Amount, Type="Transfer", Remarks=vm.Remarks});
        _db.SaveChanges(); TempData["Success"] = "Transfer completed successfully."; return RedirectToAction("History");
    }
    public IActionResult History(DateTime? fromDate, DateTime? toDate)
    {
        if (!IsLoggedIn) return LoginRedirect();
        var nums = _db.Accounts.Where(a=>a.UserId==CurrentUserId).Select(a=>a.AccountNumber).ToList();
        var q = _db.Transactions.Where(t => nums.Contains(t.FromAccount) || nums.Contains(t.ToAccount));
        if (fromDate.HasValue) q = q.Where(t=>t.Date.Date >= fromDate.Value.Date);
        if (toDate.HasValue) q = q.Where(t=>t.Date.Date <= toDate.Value.Date);
        return View(q.OrderByDescending(t=>t.Date).ToList());
    }
    public IActionResult Beneficiaries() => IsLoggedIn ? View(_db.Beneficiaries.Where(b=>b.UserId==CurrentUserId).ToList()) : LoginRedirect();
    [HttpPost]
    public IActionResult AddBeneficiary(Beneficiary b){ if(!IsLoggedIn) return LoginRedirect(); b.UserId=CurrentUserId!.Value; _db.Beneficiaries.Add(b); _db.SaveChanges(); return RedirectToAction("Beneficiaries"); }
}
