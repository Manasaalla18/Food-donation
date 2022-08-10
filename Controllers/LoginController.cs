using Microsoft.AspNetCore.Mvc;
using FoodDonation.Models;

namespace FoodDonation.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel lg)
        {
            if (ModelState.IsValid)
            {
                using (FDContext db = new FDContext())
                {
                    var res = db.Users.Where(x => x.UserId == lg.UserId && x.Password == lg.Password).ToList();
                    if (res.Count() > 0)
                    {

                        var User = res.FirstOrDefault();
                        HttpContext.Session.SetInt32("Role", User.roleId);
                        HttpContext.Session.SetString("UserId", User.UserId);
                        return RedirectToAction("Index", "dashboard");
                    }
                    else
                    {
                        TempData["msg"] = "0";
                    }
                }
            }
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(UserMaster um)
        {


            using (FDContext db = new FDContext())
            {
                if (um.Category == "User")
                {
                    um.roleId = 101;
                }
                else if (um.Category == "NGO")
                {
                    um.roleId = 102;
                }
                else
                {
                    um.roleId = 103;
                }
                var K = db.Users.Where(x => x.UserId == um.UserId);
                if (K.Count() > 0)
                {
                    ViewBag.Msg = "Already Exit";

                    return View();
                }
                db.Users.Add(um);
                if (db.SaveChanges() > 0)
                {
                    TempData["status"] = "1";
                    ModelState.Clear();
                }
                else
                {
                    TempData["status"] = "0";
                }
            }

            return View();
        }

        public IActionResult FeedBack()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FeedBack(FeedBack f)
        {
            FDContext db = new FDContext();
            db.FeedBack.Add(f);
            if (db.SaveChanges() > 0)
            {
                TempData["status1"] = "1";
                ModelState.Clear();
            }
            else
            {
                TempData["status1"] = "0";
            }
            return View();

        }

        public IActionResult FeedBackView()
        {
            FDContext db = new FDContext();
            var res = db.FeedBack.ToList();
            ViewBag.Msg = res;
            return View();
        }

    }
}