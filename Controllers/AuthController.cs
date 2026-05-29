using Microsoft.AspNetCore.Mvc;
using NetBank.Data;
using NetBank.Models;

namespace NetBank.Controllers;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _db;
    public AuthController(ApplicationDbContext db) => _db = db;

    public IActionResult Login() => View(new LoginVm());
    [HttpPost]
    public IActionResult Login(LoginVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        var user = _db.Users.FirstOrDefault(x => x.Email == vm.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(vm.Password, user.PasswordHash))
        { ModelState.AddModelError("", "Invalid email or password"); return View(vm); }
        HttpContext.Session.SetInt32("UserId", user.UserId);
        HttpContext.Session.SetString("UserName", user.Name);
        HttpContext.Session.SetString("Role", user.Role);
        return user.Role == "Admin" ? RedirectToAction("Index", "Admin") : RedirectToAction("Dashboard", "Account");
    }

    public IActionResult Register() => View(new RegisterVm());
    [HttpPost]
    public IActionResult Register(RegisterVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        if (_db.Users.Any(x => x.Email == vm.Email)) { ModelState.AddModelError("Email", "Email already exists"); return View(vm); }
        var user = new User { Name = vm.Name, Email = vm.Email, PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password), Role = "User" };
        _db.Users.Add(user); _db.SaveChanges();
        _db.Accounts.Add(new Account { UserId = user.UserId, AccountNumber = GenerateAccountNumber(), Balance = 1000, AccountType = "Savings" });
        _db.SaveChanges();
        TempData["Success"] = "Registration successful. Please login.";
        return RedirectToAction("Login");
    }
    public IActionResult Logout(){ HttpContext.Session.Clear(); return RedirectToAction("Login"); }
    private string GenerateAccountNumber() => DateTime.Now.Ticks.ToString()[^10..];
}
