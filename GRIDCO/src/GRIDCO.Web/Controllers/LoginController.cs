using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GRIDCO.Core.ProjectAggregate.Entity;
using GRIDCO.SharedKernel.Interfaces;
using GRIDCO.SharedKernel;
using Microsoft.AspNetCore.Http;
using GRIDCO.Core.Abstract;
using GRIDCO.Infrastructure.Data;

namespace GRIDCO.Web.Controllers
{
    public class LoginController : Controller
    {
        public async Task<IActionResult> Login()
        {
            ViewBag.Captch = RandomString(6);
            return View();
        }

        private readonly IRepository<GRIDCO_Manage_User> mUser;
        AppDbContext con;

        private readonly IRepository<GRIDCO_User_Credential> credential;
        private readonly IRepositoryInterface repositoryInterface;
        public LoginController(IRepository<GRIDCO_Manage_User> _mUser, IRepository<GRIDCO_User_Credential> _credential, AppDbContext _con
           , IRepositoryInterface _repositoryInterface)
        {
            this.mUser = _mUser;
            this.credential = _credential;
            this.con = _con;
           // this.otpmaster = _otpmaster;
            this.repositoryInterface = _repositoryInterface;


        }
        [HttpPost]
        public async Task<IActionResult> Login(GRIDCO_User_Credential uc)
        {

            ViewBag.Captch = RandomString(6);

            var users = await credential.ListAsync();
            var userdata = await mUser.ListAsync();


            var user = users.Where(x => x.User_Name == uc.User_Name && x.Password == MainModel.EncodeBase64(uc.Password)).FirstOrDefault();


            if (user != null && user.Is_Deleted == false)
            {
                var data = userdata.Where(x => x.Id == user.User_Id).FirstOrDefault();
                HttpContext.Session.SetString("UserType", user.User_Type.ToString());

                HttpContext.Session.SetString("UserId", user.User_Id.ToString());
                HttpContext.Session.SetString("UserName", user.User_Name);
                if (user.User_Type.ToString() == "1")
                {
                   // HttpContext.Session.SetString("Dist_Id", data.District_Id.ToString());
                    return RedirectToAction("Demo", "Home");
                }

                else if (user.User_Type.ToString() == "2")
                {
                    return RedirectToAction("Demo", "Home");

                }

                else if (user.User_Type.ToString() == "3")
                {
                    return RedirectToAction("MDDashboard", "Dashboard");

                }
                else if (user.User_Type.ToString() == "4")
                {
                    return RedirectToAction("Demo", "Home");

                }
                else if (user.User_Type.ToString() == "5")
                {
                    return RedirectToAction("UserDashboard", "Dashboard");

                }
                else if (user.User_Type.ToString() == "6")
                {
                    return RedirectToAction("Demo", "Home");

                }

                else if (user.User_Type.ToString() == "7")
                {
                    return RedirectToAction("Demo", "Home");

                }

                else if (user.User_Type.ToString() == "8")
                {
                    return RedirectToAction("Demo", "Home");

                }
            }
            else if (user != null && user.Is_Deleted == true)
            {
                TempData["warning"] = "user doesn't exist";
                return View();
            }
            else
            {
                TempData["warning"] = "Invalid Username or Password ";
                return View();
            }
            return View();
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        public IActionResult CreateCaptcha()
        {
            string data = RandomString(6);
            return Json(data);
        }


        public async Task<IActionResult> Login_User()
        {
            ViewBag.Captch = RandomString(6);
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        public IActionResult Logout_User()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login_User");
        }

        [HttpGet]
        public IActionResult Forgotpassword()
        {
            return View();
        }

    }
}
