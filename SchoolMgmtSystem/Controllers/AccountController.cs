using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SchoolMgmtSystem.BLL;
using SchoolMgmtSystem.Models;
using SchoolMgmtSystem.ViewModels;

namespace SchoolMgmtSystem.Controllers;

public class AccountController : Controller
{
    private readonly IStudentService _studentService;

    public AccountController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        LoginResult result = _studentService.Login(model.Username, model.Password);

        if (!result.Success)
        {
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        HttpContext.Session.SetInt32("StudentId", result.StudentId);
        HttpContext.Session.SetString("Username", result.Username);
        HttpContext.Session.SetString("FirstName", result.FirstName);

        return RedirectToAction("List", "Student");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
