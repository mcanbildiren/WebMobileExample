using Identity101.Models.Identity;
using Identity101.Models.Role;
using Identity101.Services.Email;
using Identity101.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity101.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, IEmailService emailService, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _emailService = _emailService;
        _roleManager = roleManager;
        CheckRoles();
    }

    private void CheckRoles()
    {
        foreach (var item in Roles.RoleList)
        {
            if (_roleManager.RoleExistsAsync(item).Result)
                continue;
            var result = _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = item
            }).Result;
        }
    }

    [HttpGet("~/kayit-ol")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("~/kayit-ol")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Bir hata oluştu");
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
            Name = model.Name,
            Surname = model.Surname
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            //TODO: Email gönderme - Aktivasyon
            //TODO: Login olma
            return RedirectToAction("Login");
        }
        var messages = string.Join("\n", result.Errors.Select(x => x.Description));
        ModelState.AddModelError(string.Empty, messages);
        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }
}