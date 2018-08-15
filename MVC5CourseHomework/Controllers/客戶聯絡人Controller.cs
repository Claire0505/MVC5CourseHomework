using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ClosedXML.Excel;
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
        public ActionResult Index(string sortOrder)
        {
            var data = custContactRepo.All();

            var jobTitle = custContactRepo.GetJobTitle();
            ViewBag.職稱 = new SelectList(jobTitle);

            ViewBag.jobTitleSortParm = String.IsNullOrEmpty(sortOrder) ? "jobTitle_desc" : "";
            ViewBag.nameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.emailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewBag.phoneNumSortParm = String.IsNullOrEmpty(sortOrder) ? "phone_desc" : "";
            ViewBag.telNumSortParm = String.IsNullOrEmpty(sortOrder) ? "telNum_desc" : "";
            ViewBag.customerNameSortParm = String.IsNullOrEmpty(sortOrder) ? "customerName_desc" : "";

            switch (sortOrder)
            {
                case "jobTitle_desc":
                    data = data.OrderByDescending(o => o.職稱);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(o => o.姓名);
                    break;
                case "email_desc":
                    data = data.OrderByDescending(o => o.Email);
                    break;
                case "phone_desc":
                    data = data.OrderByDescending(o => o.手機);
                    break;
                case "telNum_desc":
                    data = data.OrderByDescending(o => o.電話);
                    break;
                case "customerName_desc":
                    data = data.OrderByDescending(o => o.客戶資料);
                    break;
                default:
                    data = data.OrderBy(o => o.客戶Id);
                    break;
            }

          
            return View(data.ToList());
        }

        //客戶聯絡人新增搜尋功能
        public ActionResult Search(string contactName, string contactPhone, string contactTel, string 職稱)
        {
           
            var data = custContactRepo.Search(contactName, contactPhone, contactTel, 職稱);

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

        //使用 ClosedXML 這個 NuGet 套件實作資料匯出功能，每個清單頁上都要有可以匯出 Excel 檔案的功能，要用到 FileResult 下載檔案
        public ActionResult Export(string contactName, string contactPhone, string contactTel, string contactTitle)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                int timeStamp = Convert.ToInt32(DateTime.UtcNow.AddHours(8).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

                string outputsTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                var data = custContactRepo
                    .Search(contactName, contactPhone, contactTel, contactTitle)
                    .Select(s => new { s.Id, s.職稱, s.姓名, s.Email, s.手機, s.電話, s.客戶資料.客戶名稱 });

                var ws = wb.Worksheets.Add("custdata", 1);

                //欄位名稱
                ws.Cell("A1").Value = "Id";
                ws.Cell("B1").Value = "職稱";
                ws.Cell("C1").Value = "姓名";
                ws.Cell("D1").Value = "Email";
                ws.Cell("E1").Value = "手機";
                ws.Cell("F1").Value = "電話";
                ws.Cell("G1").Value = "客戶名稱";

                ws.Cell(2, 1).InsertData(data);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    return File(
                        memoryStream.ToArray(),
                        "application/vnd.ms-excel",
                        $"Export_客戶聯絡人_{outputsTime}.xlsx");
                }
            }
            
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
