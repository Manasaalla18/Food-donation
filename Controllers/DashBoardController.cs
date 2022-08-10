using FoodDonation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Food_Donation.Controllers
{
    public class DashBoardController : Controller
    {
        FDContext db = new FDContext();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                var s = HttpContext.Session.GetString("UserId").ToString();
                string name = db.Users.Where(x => x.UserId == s).SingleOrDefault()?.FirstName;
                ViewData["name"] = name;
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Donate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Donate(Donate d)
        {
            using (FDContext db = new FDContext())
            {
                Donate d1 = new Donate();
                d1.UserId = HttpContext.Session.GetString("UserId").ToString();
                d1.Name = d.Name;
                d1.PurposeOfDonation = d.PurposeOfDonation;
                d1.Location = d.Location;
                d1.ContactNumber = d.ContactNumber;
                d1.Status = "processing";
                d1.NoOfServing = d.NoOfServing;
                db.Donation.Add(d1);
                if (db.SaveChanges() > 0)
                {
                    TempData["status1"] = "1";
                    ModelState.Clear();
                }
                else
                {
                    TempData["status1"] = "0";
                }
            }

            return View();
        }
        public IActionResult Accept(string id)
        {
            if ((HttpContext.Session.GetString("UserId") == "Admin"))
            {
                Donate? ss = new Donate();
                Donate? s = new Donate();
                using (FDContext db = new FDContext())
                {

                    ss = db.Donation.Where(x => x.RequestID == id).FirstOrDefault();
                    s = ss;
                    db.Donation.Remove(ss);
                    db.SaveChanges();
                    s.Name = s.Name;
                    s.NoOfServing = s.NoOfServing;
                    s.PurposeOfDonation = s.PurposeOfDonation;
                    s.Location = s.Location;
                    s.ContactNumber = s.ContactNumber;
                    s.UserId = s.UserId;
                    s.Status = "Accepted";
                    HttpContext.Session.SetString("RequestId", s.RequestID);
                    db.Donation.Add(s);
                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg1"] = "1";
                    }
                    else
                    {
                        TempData["msg1"] = "0";
                    }
                }
                return RedirectToAction("AdminMsg", "DashBoard");
            }
            else
            {
                return RedirectToAction("ViewReq", "DashBoard");
            }

        }

        public IActionResult Reject(string id)
        {
            if ((HttpContext.Session.GetString("UserId") == "Admin"))
            {
                Donate? ss = new Donate();
                Donate? s = new Donate();
                using (FDContext db = new FDContext())
                {
                    ss = db.Donation.Where(x => x.RequestID == id).FirstOrDefault();
                    s = ss;
                    db.Donation.Remove(ss);
                    db.SaveChanges();
                    s.Name = s.Name;
                    s.NoOfServing = s.NoOfServing;
                    s.PurposeOfDonation = s.PurposeOfDonation;
                    s.Location = s.Location;
                    s.ContactNumber = s.ContactNumber;
                    s.UserId = s.UserId;
                    s.Status = "Rejected";
                    db.Donation.Add(s);
                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg1"] = "1";
                    }
                    else
                    {
                        TempData["msg1"] = "0";
                    }
                }
                return RedirectToAction("ViewReq", "DashBoard");
            }
            else
            {
                return RedirectToAction("ViewReq", "DashBoard");
            }

        }

        public IActionResult ViewReq()
        {
            FDContext db = new FDContext();
            if (HttpContext.Session.GetString("UserId").ToString() == "Admin")
            {
                var data = db.Donation.ToList();
                ViewBag.Donation = data;
                return View();
            }
            else
            {
                var data = db.Donation.Where(x => x.UserId == HttpContext.Session.GetString("UserId").ToString());
                ViewBag.Donation = data;
                return View();
            }
        }

        public IActionResult FoodRequest()
        {

            return View();
        }


        [HttpPost]
        public IActionResult FoodRequest(FoodRequest fr)
        {
            using (FDContext db = new FDContext())
            {
                FoodRequest foodRequest = new FoodRequest();
                foodRequest.UserId = HttpContext.Session.GetString("UserId").ToString();
                foodRequest.Date = fr.Date;
                foodRequest.Locality = fr.Locality;
                foodRequest.Status = "Processing";
                foodRequest.Occasion = fr.Occasion;
                foodRequest.Quantity = fr.Quantity;
                db.Requests.Add(foodRequest);
                if (db.SaveChanges() > 0)
                {
                    TempData["msg"] = "1";
                    db.SaveChanges();
                }
                else
                {
                    TempData["msg"] = "0";
                }
            }
            return View();

        }


        public IActionResult FoodRequestView()
        {

            var data = db.Requests.Where(x => x.UserId == HttpContext.Session.GetString("UserId")).ToList();
            ViewBag.Request = data;
            return View();

        }

        public IActionResult ViewAllFoodRequest()
        {
            var data = db.Requests.Where(x => x.Status == "Accepted" || x.Status == "Processing").ToList();
            ViewBag.Request = data;
            return View();
        }


        public IActionResult AcceptRequest(string id)
        {
            if (HttpContext.Session.GetString("UserId").ToString() == "Admin")
            {
                FoodRequest? ss = new FoodRequest();
                FoodRequest? s = new FoodRequest();
                using (FDContext db = new FDContext())
                {
                    ss = db.Requests.Where(x => x.FoodRequestId == id).FirstOrDefault();
                    s = ss;
                    db.Requests.Remove(ss);
                    db.SaveChanges();
                    s.UserId = s.UserId;
                    s.Quantity = s.Quantity;
                    s.Occasion = s.Occasion;
                    s.Locality = s.Locality;
                    s.Date = s.Date;
                    s.Status = "Accepted";
                    db.Requests.Add(s);
                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg1"] = "Accepted Successfully";
                    }
                    else
                    {
                        TempData["msg1"] = "Acceptance  unSuccessfull";
                    }
                }
                return RedirectToAction("ViewAllFoodRequest", "DashBoard");
            }
            else
            {
                return RedirectToAction("ViewAllFoodRequest", "DashBoard");
            }
        }
        public IActionResult RejectRequest(string id)
        {
            if (HttpContext.Session.GetString("UserId").ToString() == "Admin")
            {

                FoodRequest? ss = new FoodRequest();
                FoodRequest? s = new FoodRequest();
                using (FDContext db = new FDContext())
                {
                    ss = db.Requests.Where(x => x.FoodRequestId == id).FirstOrDefault();
                    s = ss;
                    db.Requests.Remove(ss);
                    db.SaveChanges();
                    s.UserId = s.UserId;
                    s.Quantity = s.Quantity;
                    s.Occasion = s.Occasion;
                    s.Locality = s.Locality;
                    s.Date = s.Date;
                    s.Status = "Rejected";
                    db.Requests.Add(s);
                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg2"] = "Rejected Successfully";
                    }
                    else
                    {
                        TempData["msg1"] = "Failed to Reject";
                    }
                }
                return RedirectToAction("ViewAllFoodRequest", "DashBoard");
            }
            else
            {
                return RedirectToAction("ViewAllFoodRequest", "DashBoard"); ;

            }
        }

        public IActionResult LogForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogForm(LogisticModel l)
        {
            LogisticModel l1 = new LogisticModel();
            l1.location = l.location;
            l1.vechicalNumber = l.vechicalNumber;
            l1.PhoneNumber = l.PhoneNumber;
            l1.Status = "on";
            l1.LogUserId = HttpContext.Session.GetString("UserId").ToString();
            var K = db.Logistics.Where(x => x.LogUserId == l1.LogUserId);
            if (K.Count() > 0)
            {
                TempData["Msg"] = "2";
                return View();
            }
            l1.Name = l.Name;
            db.Logistics.Add(l1);
            if (db.SaveChanges() > 0)
            {
                db.SaveChanges();
                TempData["msg"] = "1";
            }
            else
            {
                TempData["msg"] = "0";
            }
            return View();
        }
        public IActionResult LogView()
        {
            if (HttpContext.Session.GetString("UserId").ToString() == "Admin")
            {
                var data = db.Logistics.ToList();
                ViewBag.logistics = data;
                return View();
            }
            else
            {
                var data = db.Logistics.Where(x => x.LogUserId == HttpContext.Session.GetString("UserId").ToString()).ToList();
                ViewBag.logistics = data;
                return View();
            }


        }

        public IActionResult AcceptLogistic(string id)
        {
            if (HttpContext.Session.GetString("UserId").ToString() == "Admin")
            {
                LogisticModel? ss = new LogisticModel();

                LogisticModel s = new LogisticModel();
                using (FDContext db = new FDContext())
                {
                    ss = db.Logistics.Where(x => x.LogUserId == id).FirstOrDefault();
                    s = ss;
                    db.Logistics.Remove(ss);
                    db.SaveChanges();
                    s.LogUserId = s.LogUserId;
                    s.Name = s.Name;
                    s.vechicalNumber = s.vechicalNumber;
                    s.PhoneNumber = s.PhoneNumber;
                    s.location = s.location;
                    s.Status = "Accepted";
                    db.Logistics.Add(s);
                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg1"] = "Accepted Successfully";
                    }
                    else
                    {
                        TempData["msg1"] = "Acceptance  unSuccessfull";
                    }
                }
                return RedirectToAction("LogView", "DashBoard");
            }
            else
            {
                return RedirectToAction("LogView", "Dashboard");
            }
        }
        public IActionResult RejectLogistic(string id)
        {

            if (HttpContext.Session.GetString("UserId").ToString() == "Admin")
            {

                LogisticModel? ss = new LogisticModel();

                LogisticModel s = new LogisticModel();
                using (FDContext db = new FDContext())
                {

                    ss = db.Logistics.Where(x => x.LogUserId == id).FirstOrDefault();
                    s = ss;
                    db.Logistics.Remove(ss);
                    db.SaveChanges();
                    s.LogUserId = s.LogUserId;
                    s.Name = s.Name;
                    s.vechicalNumber = s.vechicalNumber;
                    s.PhoneNumber = s.PhoneNumber;
                    s.location = s.location;
                    s.Status = "Rejected";
                    db.Logistics.Add(s);
                    if (db.SaveChanges() > 0)
                    {
                        TempData["msg1"] = "Rejected Successfully";
                    }
                    else
                    {
                        TempData["msg1"] = "Failed to Reject";
                    }
                }
                return RedirectToAction("LogView", "DashBoard");
            }
            else
            {
                return RedirectToAction("LogView", "Dashboard");
            }
        }

        public IActionResult AdminMsg()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminMsg(AdminMsg a)
        {
            AdminMsg a1 = new AdminMsg();
            a1.RequestId = HttpContext.Session.GetString("RequestId").ToString();
            a1.DonarUserId = a.DonarUserId;
            a1.LogisticUserId = a.LogisticUserId;
            a1.Message = a.Message;
            db.AdminMsgs.Add(a1);
            if (db.SaveChanges() > 0)
            {
                db.SaveChanges();
                TempData["msg"] = "1";
                ModelState.Clear();
            }
            else
            {
                TempData["msg"] = "0";
            }
            return View();
        }

        public IActionResult AdminMsgView()
        {
            var data = db.AdminMsgs.Where(x => x.DonarUserId == HttpContext.Session.GetString("UserId").ToString() || x.LogisticUserId == HttpContext.Session.GetString("UserId").ToString());
            ViewBag.msgs = data;
            return View();
        }

        public IActionResult Password()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Password(ResetPassword res1)
        {

            UserMaster res = new UserMaster();
            res = db.Users.Where(x => x.UserId == HttpContext.Session.GetString("UserId")).FirstOrDefault();
            if (res.Password == res1.OldPassword)
            {
                db.Users.Remove(res);
                db.SaveChanges();
                res.Password = res1.NewPassword;
                db.Users.Add(res);

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
            else
            {
                TempData["Msg"] = "OldPassword not Matched ";
                return View();
            }

        }

    }
}
