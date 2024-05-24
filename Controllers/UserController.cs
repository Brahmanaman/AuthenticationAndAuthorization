using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticationAndAuthorization.Models;
using AuthenticationAndAuthorization.DbContext;
using System.Web.Security;

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
                        return RedirectToAction("Login");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View();

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Login User = connectionContext.LoginUser().Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
                    if (User != null)
                    {
                        HttpCookie cookieUserId = new HttpCookie("UserId", User.UserId.ToString());
                        Response.Cookies.Add(cookieUserId);


                        HttpCookie cookieUserName = new HttpCookie("UserName", User.UserName.ToString());
                        Response.Cookies.Add(cookieUserName);


                        HttpCookie cookieUserEmail = new HttpCookie("UserEmail", User.Email.ToString());
                        Response.Cookies.Add(cookieUserEmail);

                        cookieUserName.Expires = DateTime.Now.AddSeconds(10);



                        ModelState.Clear();
                        TempData["Message"] = "User LoggedIN Successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = "Invalid Userid or Password";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}