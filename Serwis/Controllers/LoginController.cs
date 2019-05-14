﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serwis.Models;

namespace Serwis.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(Users user)
        {
            using(var db = new Service())
            {
                db.Users.Load();
                var usr = db.Users.Where(p => p.Login == user.Login && p.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    HttpContext.Session.SetString("Id", usr.Id.ToString());
                    HttpContext.Session.SetString("Login", usr.Login);
                    return Redirect("Customers/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Login lub hasło są złe");
                }
            }
            return View();
        }

        public IActionResult LoggedIn()
        {
           
            if(HttpContext.Session.Get("Id") != null)
            {

                ViewBag.Message = HttpContext.Session.Get("Login");
                return View();
            }
            else
            {
                return RedirectToAction("Customers");
            }
        }
    }
}