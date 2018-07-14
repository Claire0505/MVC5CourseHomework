using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5CourseHomework.Models;

namespace MVC5CourseHomework.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料
        public ActionResult Index()
        {
            // 畫面只需顯示還未刪除的資料「是否已刪除 == false」，讓資料庫「標示已刪除」即可，不要真的刪除資料
            var data = db.客戶資料.Where(w => w.是否已刪除 == false).ToList();
            return View(data);
        }

        //對客戶資料新增搜尋功能
        public ActionResult Search(string keyword, string unNum, string telNum)
        {
            var data = db.客戶資料.Where(w => w.是否已刪除 == false).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.Where(w => w.客戶名稱.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(unNum))
            {
                data = data.Where(w => w.統一編號.Contains(unNum));
            }

            if (!string.IsNullOrEmpty(telNum))
            {
                data = data.Where(w => w.電話.Contains(telNum));
            }

            //指定由那一個View顯示查詢結果
            return View("Index", data);
        }

        //新增客戶清單列表
        public ActionResult CustomerList()
        {
            // 畫面只需顯示還未刪除的資料「是否已刪除 == false」，讓資料庫「標示已刪除」即可，不要真的刪除資料
            var data = from customer in db.客戶資料
                       where customer.是否已刪除 == false
                       select new CustomerViewModel()
                        {
                            Id = customer.Id,
                            客戶名稱 = customer.客戶名稱,
                            聯絡人數量 = db.客戶聯絡人.Count(p => p.客戶Id == customer.Id),
                            銀行帳戶數量 = db.客戶銀行資訊.Count(p => p.客戶Id == customer.Id)
                        };

            return View(data);
           
        }

        // 使用Remote 來驗證 Email資料不能重複。
        // 參數名稱要和Model的一樣，否則不能驗證，參數的大小寫無所謂。
        public JsonResult IsCheckEmailEsist(string Email)
        {

            return Json(IsEmialAvailable(Email),JsonRequestBehavior.AllowGet);
        }

        public bool IsEmialAvailable(string EmailId)
        {
            var checkEmail = (from c in db.客戶資料
                         where c.Email.ToUpper() == EmailId.ToUpper()
                         select new { EmailId }).FirstOrDefault();

            bool status;
            if (checkEmail != null)
            {
                //Already registred
                status = false;
            }
            else
            {   
                //Avaiable to use
                status = true;
            }

            return status;
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            //db.客戶資料.Remove(客戶資料);
            //修改 ClientsController 的刪除功能，讓資料庫「標示已刪除」即可，不要真的刪除資料
            客戶資料.是否已刪除 = true;
            db.SaveChanges();

            return RedirectToAction("Index");
          
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
