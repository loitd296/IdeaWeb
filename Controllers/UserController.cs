using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdeaWeb.Data;
using IdeaWeb.Models;
using System.Net;
using System.Net.Mail;
using IdeaWeb.Untils;



namespace IdeaWeb.Controllers
{
    public class UserController : Controller
    {

        private readonly IdeaWebContext _context;
        const string SessionName = "_Name";
        const string SessionId = "_ID";
        const string SessionRole = "_Role";
        public UserController(IdeaWebContext context)
        {
            _context = context;

        }

        // GET: User
        public async Task<IActionResult> Index(int pg = 1)
        {
            ViewBag.Layout = "indexAdmin";
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.User.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _context.UserRole.Include(u => u.user).ThenInclude(u => u.Department).Include(u => u.roles).Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.UserRole = _context.Role;
            //var ideaWebContext = _context.User.Include(u => u.Department);
            return View(data);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Layout = "indexAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Department)
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewBag.Layout = "indexAdmin";
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,phone,dob,email,password,flag,DepartmentId")] User user)
        {
            var Encode = new Encode();
            string en_password = Encode.encode(user.password.ToString());
            if (ModelState.IsValid)
            {
                user.password = en_password;
                Send send = new Send();
                user.flag = 0;
                _context.Add(user);
                await _context.SaveChangesAsync();
                var users = _context.User.FirstOrDefault(p => p.id == user.id);
                if (users != null)
                {
                    var userRoles = new UserRole();
                    userRoles.roleId = 2;
                    userRoles.userId = users.id;
                    _context.Add(userRoles);
                    await _context.SaveChangesAsync();
                }


                Console.WriteLine(user.email);
                var email = user.email.ToString();
                var subject = "PLEASE CONFIRM YOUR EMAIL BY CLICK IN LINK";
                string body = "https://localhost:7188/User/ConfirmAccount?email=" + email;
                send.SendEmail(email, subject, body);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", user.DepartmentId);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("id,name,phone,dob,email,password,flag,DepartmentId")] User user, String repassword)
        {
            var Encode = new Encode();
            string en_password = Encode.encode(user.password.ToString());
            string en_repassword = Encode.encode(repassword);
            Console.WriteLine(en_password + " " + en_repassword);
            if (en_password == en_repassword)
            {
                if (ModelState.IsValid)
                {
                    user.password = en_password;
                    Send send = new Send();
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    var users = _context.User.FirstOrDefault(p => p.id == user.id);
                    if (users != null)
                    {
                        var userRoles = new UserRole();
                        userRoles.roleId = 2;
                        userRoles.userId = users.id;
                        _context.Add(userRoles);
                        await _context.SaveChangesAsync();
                    }

                    Console.WriteLine(user.email);
                    var email = user.email.ToString();
                    var subject = "PLEASE CONFIRM YOUR EMAIL BY CLICK IN LINK";
                    string body = "https://localhost:7188/User/ConfirmAccount?email=" + email;
                    send.SendEmail(email, subject, body);
                    return RedirectToAction(nameof(Login));

                }
            }
            else
            {
                ViewBag.ErrorMessage = "Repassword != Password";
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(String UserName, String Password)
        {
            var Encode = new Encode();
            if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password))
            {
                var en_password = Encode.encode(Password);
                var user = await _context.User.FirstOrDefaultAsync(u => u.email == UserName && u.password == en_password);
                var userRole =  _context.UserRole.Include(u => u.roles).FirstOrDefault(u => u.userId == user.id);
                
                if (user != null && user.flag == 1)
                {
                    HttpContext.Session.SetString(SessionName, user.name);
                    HttpContext.Session.SetInt32(SessionId, user.id);
                    HttpContext.Session.SetString(SessionRole, userRole.roles.name);
                    
                    if(userRole.roles.name == "Admin" || userRole.roles.name == "Manager"){
                        return RedirectToAction("Index", "Admin");
                    }else{
                        return RedirectToAction("IdeaIndex", "Idea");
                    }
                    
                    
                }
                else if (user != null && user.flag == 0)
                {
                    ViewBag.ErrorMessage = "PLease Confrim Your ";
                }
                else
                {
                    ViewBag.ErrorMessage = "User not found";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Username or Password cannot be empty";
            }
            return View();
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Layout = "indexAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", user.DepartmentId);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,phone,dob,email,password,flag,DepartmentId")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Send send = new Send();
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    Console.WriteLine(user.email);
                    var email = user.email.ToString();
                    var subject = "PLEASE CONFIRM YOUR EMAIL BY CLICK IN LINK";
                    string body = "https://localhost:7188/User/ConfirmAccount?email=" + email;
                    send.SendEmail(email, subject, body);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", user.DepartmentId);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmAccount(String email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.email == email);
            string message = "";
            if (user != null && user.flag == 0)
            {
                user.flag = 1;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            else if (user == null)
            {
                message = "Your email does not create";
            }
            ViewBag.message = message;
            return View();
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Layout = "indexAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Department)
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.id == id);
        }
        public IActionResult Login() { return View(); }

        public IActionResult Profile() { return View(); }
        public IActionResult EditProfile() { return View(); }
        public IActionResult loginSuccess() { return View(); }
        public IActionResult InputCodeRecoveryPass() { return View(); }
        public IActionResult InputEmailRecoveryPass() { return View(); }
        public IActionResult InputNewPassword() { return View(); }
        public IActionResult Register()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }
        public IActionResult ChartNumber()
        {
            ViewBag.Layout = "indexAdmin";
            var totalIdea = _context.Idea.Count();
            var department = _context.Department.GroupBy(s => s.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = 0,
                Percent = (double)0,
                personCount = 0
            }).ToList();
            Console.WriteLine(totalIdea);
            var data = _context.Idea.Include(s => s.User).ThenInclude(s => s.Department)
            .GroupBy(s => s.User.Department.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Count(),
                Percent = Math.Round(((double)g.Count() / totalIdea) * 100),
                personCount = g.Select(s => s.User).Distinct().Count()
            })
            .ToList();
            foreach (var item in department)
            {
                var existingData = data.FirstOrDefault(d => d.Name.Contains(item.Name));
                if (existingData == null)
                {
                    data.Add(item);
                }
            }
            Random rnd = new Random();
            int red = rnd.Next(0, 255);
            int blue = rnd.Next(0, 255);
            int green = rnd.Next(0, 255);
            string[] labels = new string[data.Count()];
            string[] count = new string[data.Count()];
            string[] rgbs = new string[data.Count()];
            foreach (var item in data)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Percent);
                Console.WriteLine(item.personCount);
                Console.WriteLine(item.Count);
            }
            for (int i = 0; i < data.Count(); i++)
            {
                labels[i] = data[i].Name;
                count[i] = data[i].Count.ToString();

                rgbs[i] = ("'rgb(" + red.ToString() + "," + green.ToString() + "," + blue.ToString() + ")'");
            }


            ViewData["rgbs"] = String.Join(",", rgbs);
            ViewData["labels"] = String.Format("'{0}'", String.Join("','", labels));
            ViewData["count"] = String.Join(",", count);
            return View();
        }
        public IActionResult ChartPercent()
        {
            ViewBag.Layout = "indexAdmin";
            var totalIdea = _context.Idea.Count();
            var department = _context.Department.GroupBy(s => s.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = 0,
                Percent = (double)0,
                personCount = 0
            }).ToList();
            Console.WriteLine(totalIdea);
            var data = _context.Idea.Include(s => s.User).ThenInclude(s => s.Department)
            .GroupBy(s => s.User.Department.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Count(),
                Percent = Math.Round(((double)g.Count() / totalIdea) * 100),
                personCount = g.Select(s => s.User).Distinct().Count()
            })
            .ToList();
            foreach (var item in department)
            {
                var existingData = data.FirstOrDefault(d => d.Name.Contains(item.Name));
                if (existingData == null)
                {
                    data.Add(item);
                }
            }
            Random rnd = new Random();

            string[] labels = new string[data.Count()];
            string[] count = new string[data.Count()];
            string[] rgbs = new string[data.Count()];
            foreach (var item in data)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Percent);
                Console.WriteLine(item.personCount);
                Console.WriteLine(item.Count);
            }
            for (int i = 0; i < data.Count(); i++)
            {
                labels[i] = data[i].Name;
                count[i] = data[i].Percent.ToString();
                int red = rnd.Next(0, 255);
                int blue = rnd.Next(0, 255);
                int green = rnd.Next(0, 255);
                rgbs[i] = ("'rgb(" + red.ToString() + "," + green.ToString() + "," + blue.ToString() + ")'");
            }

            ViewData["rgbs"] = String.Join(",", rgbs);
            ViewData["labels"] = String.Format("'{0}'", String.Join("','", labels));
            ViewData["count"] = String.Join(",", count);
            return View();
        }
         public IActionResult ChartContribute()
        {
            ViewBag.Layout = "indexAdmin";
            var totalIdea = _context.Idea.Count();
            var department = _context.Department.GroupBy(s => s.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = 0,
                Percent = (double)0,
                personCount = 0
            }).ToList();
            Console.WriteLine(totalIdea);
            var data = _context.Idea.Include(s => s.User).ThenInclude(s => s.Department)
            .GroupBy(s => s.User.Department.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Count(),
                Percent = Math.Round(((double)g.Count() / totalIdea) * 100),
                personCount = g.Select(s => s.User).Distinct().Count()
            })
            .ToList();
            foreach (var item in department)
            {
                var existingData = data.FirstOrDefault(d => d.Name.Contains(item.Name));
                if (existingData == null)
                {
                    data.Add(item);
                }
            }
            Random rnd = new Random();
            int red = rnd.Next(0, 255);
            int blue = rnd.Next(0, 255);
            int green = rnd.Next(0, 255);
            string[] labels = new string[data.Count()];
            string[] count = new string[data.Count()];
            string[] rgbs = new string[data.Count()];
            foreach (var item in data)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Percent);
                Console.WriteLine(item.personCount);
                Console.WriteLine(item.Count);
            }
            for (int i = 0; i < data.Count(); i++)
            {
                labels[i] = data[i].Name;
                count[i] = data[i].personCount.ToString();

                rgbs[i] = ("'rgb(" + red.ToString() + "," + green.ToString() + "," + blue.ToString() + ")'");
            }


            ViewData["rgbs"] = String.Join(",", rgbs);
            ViewData["labels"] = String.Format("'{0}'", String.Join("','", labels));
            ViewData["count"] = String.Join(",", count);
            return View();
        }
    }
}
