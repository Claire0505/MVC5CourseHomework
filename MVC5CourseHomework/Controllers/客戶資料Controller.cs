﻿using System;
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
        //private 客戶資料Entities db = new 客戶資料Entities();

        //使用 Repository Pattern 管理所有新刪查改(CRUD)等功能
        客戶資料Repository customerRepo;
        客戶聯絡人Repository contactRepo;
        客戶銀行資訊Repository bankRepo;

        public 客戶資料Controller()
        {
            customerRepo = RepositoryHelper.Get客戶資料Repository();
            contactRepo = RepositoryHelper.Get客戶聯絡人Repository(customerRepo.UnitOfWork);
            bankRepo = RepositoryHelper.Get客戶銀行資訊Repository(customerRepo.UnitOfWork);
        }

        // GET: 客戶資料
        public ActionResult Index()
        {
            // 畫面只需顯示還未刪除的資料「是否已刪除 == false」，讓資料庫「標示已刪除」即可，不要真的刪除資料
            var data = customerRepo.All();
            return View(data);
        }

        //對客戶資料新增搜尋功能
        public ActionResult Search(string keyword, string unNum, string telNum)
        {
            var data = customerRepo.Search(keyword, unNum, telNum);
            //指定由那一個View顯示查詢結果
            return View("Index", data);
        }

        //新增客戶清單列表
        public ActionResult CustomerList()
        {

            var contact = contactRepo.All();
            var bank = bankRepo.All();
            var data = customerRepo.GetContactBankCount(contact, bank);

            return View(data);
           
        }

        
        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = customerRepo.Find(id.Value);
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
                customerRepo.Add(客戶資料);
                customerRepo.UnitOfWork.Commit();
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
            客戶資料 客戶資料 = customerRepo.Find(id.Value);
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
                var db = customerRepo.UnitOfWork.Context;
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
            客戶資料 客戶資料 = customerRepo.Find(id.Value);
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
            客戶資料 客戶資料 = customerRepo.Find(id);
            customerRepo.Delete(客戶資料);
            customerRepo.UnitOfWork.Commit();

            return RedirectToAction("Index");
          
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                customerRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
