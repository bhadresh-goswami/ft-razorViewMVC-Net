using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RazorViewEngApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(int? id)
        {
            return View();
        }



        [HttpGet]
        public ActionResult Login(string name=null)
        {
            if(name!=null)
            {
                ViewBag.message = "Your name is "+name;
            }
            return View();
        }

        [HttpPost]
        //[ActionName("Login")]
        public ActionResult Login(FormCollection formCollection)
        {
            try
            {
                string name = formCollection["txtName"].ToString();
                string pass = formCollection["txtPassword"].ToString();
                if (
                       name.Equals("abc") &&
                       pass.Equals("123"))
                {
                    ViewBag.message = "Login Success!";
                }
                else
                {
                    ViewBag.error = "User Name or Password is Wrong!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }
            return View();
        }
        //[HttpPost]
        ////[ActionName("Login")]
        //public ActionResult Login(string name, string pass)
        //{
        //    return View();
        //}

    }
}