using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
<<<<<<< HEAD
using GameWebDemo.Models; 
=======

>>>>>>> 4aea17a9135a33304ff4e47dd424853f0526a140
namespace GameWebDemo.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
        GameDBEntities1 db = new GameDBEntities1();
        public ActionResult Index()
        {
            // 查询，获取所有用户信息，包含主表和从表的所有内容
            //var accounts = db.Msgs.Include("Accounts").OrderByDescending(m => m.MsgTime).ToList();
            // 多表查询
            ViewBag.list = db.Msgs.Include("Account").OrderByDescending(m => m.MsgTime).ToList(); 
=======
        public ActionResult Index()
        {
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

>>>>>>> 4aea17a9135a33304ff4e47dd424853f0526a140
            return View();
        }
    }
}