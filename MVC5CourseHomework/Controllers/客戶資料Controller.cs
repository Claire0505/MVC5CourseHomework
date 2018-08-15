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
using MVC5CourseHomework.Helpers;

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
        public ActionResult Index(string sortOrder)
        {
            var custName = customerRepo.GetCustomerName();
            ViewBag.custName = new SelectList(custName);
            var data = customerRepo.All();

            ViewBag.custNameSortParm = String.IsNullOrEmpty(sortOrder) ? "custName_desc" : "";
            ViewBag.unNumSortParm = String.IsNullOrEmpty(sortOrder) ? "unNum_desc" : "";
            ViewBag.telNumSortParm = String.IsNullOrEmpty(sortOrder) ? "telNum_desc" : "";
            ViewBag.faxNumSortParm = String.IsNullOrEmpty(sortOrder) ? "faxNum_desc" : "";
            ViewBag.addressSortParm = String.IsNullOrEmpty(sortOrder) ? "address_desc" : "";
            ViewBag.emailSortParm = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";

            switch (sortOrder)
            {
                case "custName_desc":
                    data = data.OrderByDescending(o => o.客戶名稱);
                    break;
                case "unNum_desc":
                    data = data.OrderByDescending(o => o.統一編號);
                    break;
                case "telNum_desc":
                    data = data.OrderByDescending(o => o.電話);
                    break;
                case "faxNum_desc":
                    data = data.OrderByDescending(o => o.傳真);
                    break;
                case "address_desc":
                    data = data.OrderByDescending(o => o.地址);
                    break;
                case "email_desc":
                    data = data.OrderByDescending(o => o.Email);
                    break;
                default:
                    data = data.OrderBy(o => o.Id);
                    break;
            }


           
            return View(data.ToList());
        }

        //對客戶資料新增搜尋功能
        public ActionResult Search(string submit, string custName, string custUid, string custTel, string custFax, 
                    string custAddress, string custEmail)
        {
            var data = customerRepo.Search(custName, custUid, custTel, custFax, custAddress, custEmail);

            var custNameList = customerRepo.GetCustomerName();
            ViewBag.custName = new SelectList(custNameList);

            //指定由那一個View顯示查詢結果
            if (submit.Equals("Search"))
            {
                return View("Index", data);
            }
            else
            {
                int timeStamp = Convert.ToInt32(DateTime.UtcNow.AddHours(8).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                string outputsTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                ExcelExportHelper excelExportHepler = new ExcelExportHelper();

                var exportData =
                    data.Select(s => new 客戶資料ViewModel()
                    {
                        Id = s.Id,
                        客戶名稱 = s.客戶名稱,
                        統一編號 = s.統一編號,
                        電話 = s.電話,
                        傳真 = s.傳真,
                        地址 = s.地址,
                        Email = s.Email,
                    })
                    .ToList();

                var memoryStream = excelExportHepler.Stream(exportData);

                return File(
                   memoryStream.ToArray(),
                   "application/vnd.ms-excel",
                   $"Export_客戶資料_{outputsTime}.xlsx");

            }
            
        }


        //新增客戶清單列表
        public ActionResult CustomerList()
        {
            var contact = contactRepo.All();
            var bank = bankRepo.All();
            var data = customerRepo.GetContactBankCount(contact, bank);
          
            return View("CustomerList", data);

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

        /*
        // 使用 ClosedXML 這個 NuGet 套件實作資料匯出功能，每個清單頁上都要有可以匯出 Excel 檔案的功能，要用到 FileResult 下載檔案
        public ActionResult Export(string custName, string custUid, string custTel, string custFax,
            string custAddress, string custEmail)
        {
            // 建立Excel
            using (XLWorkbook wb = new XLWorkbook())
            {
                string outputsTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                var data = customerRepo
                    .Search(custName, custUid, custTel, custFax, custAddress, custEmail)
                    .Select(s => new { s.Id, s.客戶名稱, s.統一編號, s.電話, s.傳真, s.地址, s.Email });

                var sheet = wb.Worksheets.Add("客戶資料表", 1);

                //修改標題列Style
                var header = sheet.Range("A1:G1");
                header.Style.Fill.BackgroundColor = XLColor.LightBlue;
                header.Style.Font.FontColor = XLColor.Gray;
                header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //欄位名稱
                sheet.Cell("A1").Value = "Id";
                sheet.Cell("B1").Value = "客戶名稱";
                sheet.Cell("C1").Value = "統一編號";
                sheet.Cell("D1").Value = "電話";
                sheet.Cell("E1").Value = "傳真";
                sheet.Cell("F1").Value = "地址";
                sheet.Cell("G1").Value = "Email";

                sheet.Cell(2, 1).InsertData(data);

                //第一欄隱藏
                sheet.Column(1).Hide();
                //自動伸縮欄寬
                sheet.Column(2).AdjustToContents();
                sheet.Column(2).Width += 2;

                sheet.Column(3).AdjustToContents();
                sheet.Column(3).Width += 2;

                sheet.Column(4).AdjustToContents();
                sheet.Column(4).Width += 2;

                sheet.Column(5).AdjustToContents();
                sheet.Column(5).Width += 2;

                sheet.Column(6).AdjustToContents();
                sheet.Column(6).Width += 2;

                sheet.Column(7).AdjustToContents();
                sheet.Column(7).Width += 2;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    return File(
                        memoryStream.ToArray(),
                        "application/vnd.ms-excel",
                        $"Export_客戶資料_{outputsTime}.xlsx");
                }
            }

        }
        */

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
