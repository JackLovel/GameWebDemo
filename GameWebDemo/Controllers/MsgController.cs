using GameWebDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            Msg msg = db.Msgs.Include("Account").SingleOrDefault(m => m.MsgID == Msg_id);
            return View(msg); 
        }
    }
}