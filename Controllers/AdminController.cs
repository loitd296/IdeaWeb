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
            Console.WriteLine(Password);
            if (!_context.Role.Any())
            {
                _context.Role.Add(new Role { name = "Admin" });
                _context.Role.Add(new Role { name = "Manager" });
                _context.Role.Add(new Role { name = "Staff" });
                _context.Department.Add(new Department { Name = "Department A" });
                _context.User.Add(new User { 
                    name ="Administrator",
                    dob = DateTime.Now,
                    phone ="0",
                    email="admin@gmail.com",
                    password=Password,
                    flag = 1,
                    DepartmentId = 1,
                });
                _context.UserRole.Add(new UserRole {userId = 1, roleId = 1});
                await _context.SaveChangesAsync();
                return RedirectToAction("Login","User");
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