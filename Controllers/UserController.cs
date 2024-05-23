using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticationAndAuthorization.Models;
using AuthenticationAndAuthorization.DbContext;

namespace AuthenticationAndAuthorization.Controllers
{
    public class UserController : Controller
    {
        ConnectionContext connectionContext = new ConnectionContext();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(SignUp user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = connectionContext.RegisterUser(user);
                    if (result)
                    {
                        ModelState.Clear();
                        TempData["Message"] = "User Registered Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag["Message"] = "User not Registered";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();

        }
    }
}