using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC5CourseHomework.Models
{
    public class 客戶資料ViewModel
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }

        [StringLength(8, ErrorMessage = "欄位長度不得大於 8 個字元")]
        [Required]
        [RegularExpression(@"\d{8}", ErrorMessage = "你輸入的格式不正確，格式為 8 碼數字")]
        public string 統一編號 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        [RegularExpression(@"^[0]\d{8,9}", ErrorMessage = "你輸入的格式不正確，格式為 0 開頭加 8 或 9 碼數字")]
        public string 電話 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [RegularExpression(@"^[0]\d{8,9}", ErrorMessage = "你輸入的格式不正確，格式為 0 開頭加 8 或 9 碼數字")]
        public string 傳真 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [EmailAddress(ErrorMessage = "你輸入的 E-Mail 格是不正確")]
        public string Email { get; set; }

        
    }
}