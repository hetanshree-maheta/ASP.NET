using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationApp.Models;

namespace RegistrationApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register rs)
        {
            mcaEntities db = new mcaEntities();
            bool exist = db.Registers.Any(s => s.Username == rs.Username);
            if (exist)
            {
                TempData["Message"] = "User already registered.";
                return RedirectToAction("Index");
            }
            else
            {
                db.Registers.Add(rs);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
        }
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            using (mcaEntities db = new mcaEntities())
            {
           
                bool check = db.Registers.Any(s => s.Username == Username && s.Password == Password);

                if (check)
                {
                    //ViewBag.Msg = "Login Successfully";

                    return RedirectToAction("Welcome");
                }
                else
                {
                    ViewBag.Message = "Wrong Username or Password";
                    // Return to login view with an error message if login fails
                    return View();
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Welcome()
        {
            ViewBag.Message = "Logged In Successfully.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload(ImageUploadModel model)
        {
            if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
            {
                var fileName = path.GetFileName(model.ImageFile.FileName);
                var path = path.Combine(Server.MapPath("~/UploadedImages"), fileName);
                model.ImageFile.SaveAs(path);
                ViewBag.Message = "Image uploaded successfully!";
            }
            else
            {
                ViewBag.Message = "Please select an image.";
            }

            return View("Index");
        }
    


}
}