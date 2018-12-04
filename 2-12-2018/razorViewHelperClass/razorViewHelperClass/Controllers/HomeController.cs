using razorViewHelperClass.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace razorViewHelperClass.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if(TempData["verUrl"]==null)
            //{
            //    return RedirectToAction("VerificationPage");
            //}
            return View();
        }

        public ActionResult VerificationPage(string key = "")
        {
            if (key=="")
            {
                ViewBag.error = "Access Denide!";
            }
            else
            {
                if(Session["key"]!=null)
                {
                    //db key is session
                    ViewBag.imageUrl = Request.Cookies["imageCode"].Values["imgUrl"].ToString();
                    ViewBag.message = "Welcome to MySite!";
                }
                else
                {
                    ViewBag.error = "Key Expire, plz try To click again regenerate link!";

                    key = Guid.NewGuid().ToString();//user.VerificationKey.ToString();

                    //update key in database

                    Session["key"] = key;
                    Session.Timeout = 2;
                    TempData["verUrl"] = Url.Action("Index", "Home") + "/?key=" + key;
                    return RedirectToAction("Index");

                }
            }
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult SingUp()
        {
            //List<SelectListItem> StateListItems = new List<SelectListItem>();

            //SelectListItem item = new SelectListItem() { Text = "Gujrat", Value = "GUJ" };
            //StateListItems.Add(item);

            //item = new SelectListItem() { Text = "MP", Value = "MP" };
            //StateListItems.Add(item);

            //ViewBag.StateName = StateListItems;


            ViewBag.StateName = UserModel.StateNames;
            ViewBag.CityName = UserModel.StateNames;

            return View();
        }
        [HttpPost]
        public ActionResult SingUp(UserModel user, HttpPostedFileBase postedImage)
        {
            string key = user.VerificationKey.ToString();

            user.UserID = Guid.NewGuid();

            ViewBag.StateName = UserModel.StateNames;
            ViewBag.CityName = UserModel.StateNames;


            try
            {
                string imageDirectory = Server.MapPath("~/Content/Images/");
                if(!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }
                string ext ="."+ postedImage.FileName.Split('.')[1];
                string imageName = user.UserID.ToString() + ext;

                HttpCookie httpCookie = new HttpCookie("imageCode");
                httpCookie.Values.Add("imgUrl", "Images/"+imageName);
                httpCookie.Expires = DateTime.Now.AddMonths(1);

                Response.Cookies.Add(httpCookie);

                postedImage.SaveAs(imageDirectory + imageName);

                user.AvtarUrl = "~/Content/Images/" + imageName;
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View();
            }



            if (!ModelState.IsValid)
            {
                //some data that need by model (userModel) 
                //is not sent from the page
                ViewBag.error = "Some Data Is missing, Please Enter All details again!";
                return View();//this will return the actual view with viewbag

            }

            try
            {
                //try to save data 
                Session["key"] = key;
                Session.Timeout = 2;
                TempData["verUrl"] = Url.Action("VerificationPage", "Home") + "/?key=" + key;
                ViewBag.message = "Sign Up Success, Please check your email id!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
            }

            return View();
        }

    }
}