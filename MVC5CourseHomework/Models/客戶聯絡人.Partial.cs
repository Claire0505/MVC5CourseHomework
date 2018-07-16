namespace MVC5CourseHomework.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人
    {
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress(ErrorMessage = "無效的Email")]

        //使用Remote 遠程驗證屬性 using system.web.mvc
        [Remote(action: "IsArleadySigned", controller: "客戶聯絡人", AdditionalFields = "客戶Id", HttpMethod = "Post", ErrorMessage = "輸入Email已經存在")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]

        [RegularExpression(@"^09\d{2}\d{6}", ErrorMessage = "你輸入的格式不正確，格式為 09 開頭加 8 碼數字")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
        
        public bool 是否已刪除 { get; set; }

    }
}
