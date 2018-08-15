using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC5CourseHomework.Models
{
    public class 客戶銀行資訊ViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 銀行名稱 { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:000}", ApplyFormatInEditMode = true)]
        public int 銀行代碼 { get; set; }

        public Nullable<int> 分行代碼 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 帳戶名稱 { get; set; }

        [StringLength(20, ErrorMessage = "欄位長度不得大於 20 個字元")]
        [Required]
        public string 帳戶號碼 { get; set; }

        public string 客戶名稱 { get; set; }

    }
}