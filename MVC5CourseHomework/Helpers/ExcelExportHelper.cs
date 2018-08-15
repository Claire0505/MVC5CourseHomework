using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ClosedXML.Excel;

namespace MVC5CourseHomework.Helpers
{
    public class ExcelExportHelper
    {
        
        public XLWorkbook Export<T>(List<T> data)
        {
            // 建立Excel物件
            XLWorkbook workbook = new XLWorkbook();

            // 加入 ExcelSheet 名稱
            var sheet = workbook.Worksheets.Add("Report");

           
            // 標題首欄位   
            int colIdx = 1;

            // 透過迴圈取得 Proterties 名稱
            foreach (var item in typeof(T).GetProperties())
            {             
                sheet.Cell(1, colIdx++).Value = item.Name;
             
            }
           
            int rowIdx = 2;
           
            foreach (var item in data)
            {             
                // 每筆資料欄位啟始位置
                int columnIndex = 1;
              
                foreach (var jtem in item.GetType().GetProperties())
                {
                    // 將資料內容加上 "'" 避免受到 excel 預設格式影響，並依 row 及 column 填入
                    sheet.Cell(rowIdx, columnIndex).Value = string.Concat("'", Convert.ToString(jtem.GetValue(item, null)));
                    columnIndex++;
                }

                rowIdx++;
              
                //修改標題列顏色
                var rowFromWorksheet = sheet.Row(1); //標題列
                rowFromWorksheet.Cells(1, columnIndex-1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                rowFromWorksheet.Cells(1, columnIndex-1).Style.Font.FontColor = XLColor.Gray;

                //第一欄id隱藏
                sheet.Column(1).Hide();
            
                //自動伸縮欄寬
                sheet.Column(2).AdjustToContents();
                sheet.Column(2).Width += 2;
                //自動伸縮欄寬
                sheet.Column(rowIdx.ToString()).AdjustToContents();
                sheet.Column(rowIdx.ToString()).Width += 2;

            }

            return workbook;
        }

        public MemoryStream Stream<T>(List<T> data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var workbook = this.Export(data);

                workbook.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
        }
    }
}