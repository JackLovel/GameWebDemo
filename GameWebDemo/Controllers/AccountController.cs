using System.Linq;
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
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }  
        
        [HttpPost]
        public ActionResult Register(Account account)
        {
            Account a = account;
            if (a != null)
            {
                db.Accounts.Add(a);
                db.SaveChanges();

                TempData["msg"] = "注册成功，请登录！";
                return View("login");
            }
            else 
            {
                TempData["msg"] = "注册失败，请重新注册！";
                return View();
            }
        }  
        
        [HttpGet]
        public ActionResult Logout()
        {
            Session["login"] = null;
            return RedirectToAction("Index", "Home");
        }


    }
}