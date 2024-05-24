﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenticationAndAuthorization.Models;
using AuthenticationAndAuthorization.DbContext;
using System.Web.Security;

namespace AuthenticationAndAuthorization.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ConnectionContext connectionContext = new ConnectionContext();

        [AllowAnonymous]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
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

                        //cookieUserName.Expires = DateTime.Now.AddSeconds(10);

                        Session["UserId"] = User.UserId.ToString();
                        Session["UserName"] = User.UserName.ToString();
                        Session["UserEmail"] = User.Email.ToString();



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

        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}