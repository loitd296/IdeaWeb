using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IdeaWeb.Models;
using Microsoft.EntityFrameworkCore;
using IdeaWeb.Data;
using IdeaWeb.Untils;

namespace IdeaWeb.Controllers;

public class AdminController : Controller
{
    private readonly IdeaWebContext _context;
    private readonly ILogger<HomeController> _logger;

    public AdminController(ILogger<HomeController> logger, IdeaWebContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.Layout = "indexAdmin";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }

    public IActionResult CreateDefaultAdmin()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateDefaultAdmin(string UserName, string Password)
    {
        if (UserName == "Admin" && Password == "admin@123")
        {
            Encode encode = new Encode();
            Password = encode.encode(Password);
            if (!_context.Role.Any())
            {
                _context.Role.Add(new Role { name = "Admin" });
                _context.Role.Add(new Role { name = "Manager" });
                _context.Role.Add(new Role { name = "Staff" });
                _context.Department.Add(new Department { Name = "Admin" });
                _context.Department.Add(new Department { Name = "IT" });
                _context.Department.Add(new Department { Name = "Marketing" });
                _context.Department.Add(new Department { Name = "Design" });
                _context.User.Add(new User
                {
                    name = "Administrator",
                    dob = DateTime.Now,
                    phone = "0",
                    email = "admin@gmail.com",
                    password = Password,
                    flag = 1,
                    DepartmentId = 1,
                });
                _context.User.Add(new User
                {
                    name = "QA IT",
                    dob = DateTime.Now,
                    phone = "0",
                    email = "cuongdpgcc200122@fpt.edu.vn",
                    password = Password,
                    flag = 1,
                    DepartmentId = 2,
                });
                _context.User.Add(new User
                {
                    name = "QA Marketing",
                    dob = DateTime.Now,
                    phone = "0",
                    email = "lapdzcutephomaique8888@gmail.com",
                    password = Password,
                    flag = 1,
                    DepartmentId = 3,
                });
                _context.User.Add(new User
                {
                    name = "QA Design",
                    dob = DateTime.Now,
                    phone = "0",
                    email = "hnmluanit@gmail.com",
                    password = Password,
                    flag = 1,
                    DepartmentId = 4,
                });
                _context.UserRole.Add(new UserRole { userId = 1, roleId = 1 });
                _context.UserRole.Add(new UserRole { userId = 2, roleId = 2 });
                _context.UserRole.Add(new UserRole { userId = 3, roleId = 2 });
                _context.UserRole.Add(new UserRole { userId = 4, roleId = 2 });
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }
        }
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}