using GameWebDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity; 

namespace GameWebDemo.Controllers
{
    public class MsgController : Controller
    {
        GameDBEntities db = new GameDBEntities();

        public ActionResult Index()
        {
            Account user = (Account)Session["login"];
            if (user != null)
            {
                var name = user.AccountName;
                List<Msg> list = db.Msgs.Include("Account")
                    .Where(m => m.Account.AccountName == name).ToList();
                return View(list);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public ActionResult Delete(int Msg_id)
        {
            int id = Msg_id;
            Msg msg = db.Msgs.SingleOrDefault(t => t.MsgID == id);
            if (msg != null)
            {
                db.Msgs.Remove(msg);
                db.SaveChanges();
                return RedirectToAction("index", "msg");
            }
            else
            {
                return RedirectToAction("index", "msg");
            }
        }

        public ActionResult Detail(int Msg_id)
        {
            var msg = db.Msgs.Include("Account").SingleOrDefault(m => m.MsgID == Msg_id);
            return View(msg);
        }

        [HttpGet]
        public ActionResult Edit(int Msg_id)
        {
            // 根据 Msg_id
            var user = Session["login"];
            if (user == null)
            {
                return RedirectToAction("login", "account");
            }
            var msg = db.Msgs.Include("Account").SingleOrDefault(m => m.MsgID == Msg_id);
            if (msg == null)
            {
                return RedirectToAction("index", "home");
            }

            return View(msg);
        }

        [HttpPost]
        public ActionResult Edit(Msg msg, HttpPostedFileBase PhotoFile)
        {
            // 补全 msg 对象中属性值 
            if (PhotoFile != null)
            {
                msg.MsgPhoto = "/img/" + PhotoFile.FileName;
                // 保存上传的文件
                PhotoFile.SaveAs(Server.MapPath(msg.MsgPhoto));
            }
            msg.MsgTime = DateTime.Now;

            // 将修改好的msg对象添加到数据库中
            // ef 6.0： System.Data.Entity.EntityState.Modified
            // ef 5.0 版本写法如下：
            db.Entry(msg).State = System.Data.EntityState.Modified;
            db.SaveChanges();

            // 需要取值用 RedirectToAction
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Msg msg, HttpPostedFileBase PhotoFile)
        {
            Account account = (Account)Session["login"];
            // 补全 msg 对象中属性值 
            if (PhotoFile != null)
            {
                msg.MsgPhoto = "/img/" + PhotoFile.FileName;
                // 保存上传的文件
                PhotoFile.SaveAs(Server.MapPath(msg.MsgPhoto));
            }
            msg.MsgTime = DateTime.Now;
            msg.MsgHit = 0;
            msg.AccountID = account.AccountID;

            // 将修改好的msg对象添加到数据库中
            // ef 6.0： System.Data.Entity.EntityState.Modified
            // ef 5.0 版本写法如下：
            db.Msgs.Add(msg); 
            db.SaveChanges();

            // 需要取值用 RedirectToAction
            return RedirectToAction("index");
        }
    }
}