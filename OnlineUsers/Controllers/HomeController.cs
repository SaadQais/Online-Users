using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineUsers.Data;
using OnlineUsers.Models;
using OnlineUsers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineUsers.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }

        public IActionResult Index()
        {
            ViewBag.OnlineUsersCount = GetOnlineUsersCount();
            ViewBag.AllUsersCount = GetAllUsers().Count;
            ViewBag.Current = "Home";
            return View();
        }

        private List<ApplicationUser> GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            return users;
        }
        
        private int GetOnlineUsersCount()
        {
            int count = 0;
            if(UsersUtils.AllUsers != null)
            {
                UsersUtils.AllUsers.ForEach(user =>
                {
                    if (DateTime.Now.Subtract(user.LastActive).TotalMinutes <= 10)
                        count++;
                    else
                        UsersUtils.AllUsers.Remove(user);
                });
            }

            return count;
        }
    }
}
