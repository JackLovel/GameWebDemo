using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameWebDemo.Models; 

namespace GameWebDemo.Controllers
{
    public class AccountController : Controller
    {
        GameDBEntities db = new GameDBEntities();
         
        public ActionResult Index() 
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }    
        
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            // 根据传递过来的用户名和密码，到数据库中查询是否存在
            Account account = db.Accounts.FirstOrDefault(m => m.AccountName == UserName 
                                                && m.AccountPwd == Password);

            if (account != null)
            {
                // 把对象传入去
                Session["login"] = account; 
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                TempData["msg"] = "用户的名称或者密码错误!";
                return View();
            }
        }
        
        public ActionResult Register()
        {
            return View();
        }
    }
}