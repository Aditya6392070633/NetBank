using Microsoft.AspNetCore.Mvc;

namespace NetBank.Controllers;

public class BaseController : Controller
{
    protected int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
    protected string? CurrentRole => HttpContext.Session.GetString("Role");
    protected bool IsLoggedIn => CurrentUserId.HasValue;
    protected IActionResult LoginRedirect() => RedirectToAction("Login", "Auth");
}
