using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameWebDemo.Models;

namespace GameWebDemo.Controllers
{
    public class HomeController : Controller
    {
        GameDBEntities db = new GameDBEntities();
        public ActionResult Index()
        {
            // 查询，获取所有用户信息，包含主表和从表的所有内容
            //var accounts = db.Msgs.Include("Accounts").OrderByDescending(m => m.MsgTime).ToList();
            // 多表查询
            ViewBag.list = db.Msgs.Include("Account").OrderByDescending(m => m.MsgTime).ToList(); 
            return View();
        }
    }
}