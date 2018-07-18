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
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        // 使用 Repository Pattern 管理所有新刪查改(CRUD)等功能
        客戶聯絡人Repository custContactRepo;
        客戶資料Repository customerRepo;

        public 客戶聯絡人Controller()
        {
            custContactRepo = RepositoryHelper.Get客戶聯絡人Repository();
            customerRepo = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            var jobTitle = custContactRepo.GetJobTitle();
            ViewBag.職稱 = new SelectList(jobTitle);

            var data = custContactRepo.All().ToList();
            return View(data);
        }

        //客戶聯絡人新增搜尋功能
        public ActionResult Search(string 職稱, string name, string phone)
        {
           
            var data = custContactRepo.Search(職稱, name, phone);

            var jobTitle = custContactRepo.GetJobTitle();
            ViewBag.職稱 = new SelectList(jobTitle);
            return View("Index",data);
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = custContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            var customer = customerRepo.All();
            ViewBag.客戶Id = new SelectList(customer, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                custContactRepo.Add(客戶聯絡人);
                custContactRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            var customer = customerRepo.All();
            ViewBag.客戶Id = new SelectList(customer, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = custContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            var customer = customerRepo.All();
            ViewBag.客戶Id = new SelectList(customer, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                var db = custContactRepo.UnitOfWork.Context;
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                custContactRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            var customer = customerRepo.All();
            ViewBag.客戶Id = new SelectList(customer ,"Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 =custContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = custContactRepo.Find(id);
            custContactRepo.Delete(客戶聯絡人);
            custContactRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                custContactRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
