using Microsoft.AspNetCore.Mvc;
using SchoolMgmtSystem.BLL;
using SchoolMgmtSystem.Models;
using SchoolMgmtSystem.ViewModels;
using System.Diagnostics;

namespace SchoolMgmtSystem.Controllers;

public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        RegisterViewModel model = new RegisterViewModel();
        model.Qualifications.Add(new QualificationViewModel());

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        System.Diagnostics.Debug.WriteLine("Hit the post action");

        // var temp = model.Age;


        Student student = new Student();
        student.FirstName = model.FirstName;
        student.LastName = model.LastName;
        student.Age = model.Age.Value;
        student.DOB = model.DOB.Value;
        student.Gender = model.Gender;
        student.Email = model.Email;
        student.Phone = model.Phone;
        student.Username = model.Username;
        student.Password = model.Password;

        foreach (QualificationViewModel q in model.Qualifications)
        {
            if (!string.IsNullOrWhiteSpace(q.CourseName))
            {
                student.Qualifications.Add(new Qualification
                {
                    CourseName = q.CourseName,
                    University = q.University,
                    PassingYear = q.PassingYear,
                    Percentage = q.Percentage
                });
            }
        }

        StudentInsertResult result = _studentService.Register(student);

        if (result.Status == 1) // 1 = duplicate email
        {
            ModelState.AddModelError("Email", "This email is already registered");
            return View(model);
        }

        if (result.Status == 2) // 2 = duplicate username
        {
            ModelState.AddModelError("Username", "This username is already taken");
            return View(model);
        }

        TempData["SuccessMessage"] = "Registration successful. Your student id is " + result.StudentCode + ". Please login.";
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult List(string search)
    {
        if (HttpContext.Session.GetString("Username") == null)
        {
            return RedirectToAction("Login", "Account");
        }

        List<StudentListItem> students = _studentService.GetAll(search);
        ViewBag.Search = search;
        return View(students);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        if (HttpContext.Session.GetString("Username") == null)
        {
            return RedirectToAction("Login", "Account");
        }

        Student student = _studentService.GetById(id);
        if (student == null)
        {
            return RedirectToAction("List");
        }

        return View(student);
    }
}
