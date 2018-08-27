# MVC5CourseHomework
[[ 第一周作業 ]]

* 資料庫下載: https://drive.google.com/open?id=1m2W1wC4P2kL_vArPHKgWfMGw9uEHw4ee
* 請使用 "客戶資料" 這個資料庫做開發 (如附件檔案)
* 請實作出「客戶資料管理」、「客戶聯絡人管理」與「客戶銀行帳戶管理」等簡易 CRUD 功能 (盡量做各種功能出來)
* 在列表頁要實作「搜尋」功能
* 實作一個清單頁面，顯示欄位有「客戶名稱、聯絡人數量、銀行帳戶數量」共三個欄位
* 主選單要有連結可以直接連到三個主要功能的列表頁。
* 對於 Create 與 Edit 表單要套用欄位驗證 (必填、Email格式、欄位長度限制等驗證)
* 刪除資料功能不能真的刪除資料庫中的資料，必須修改資料庫，加上一個「是否已刪除」欄位，資料庫中的該欄位為 bit 類型，0 代表未刪除，1 代表已刪除，且列表頁不得顯示已刪除的資料。
* 實作「客戶聯絡人」時，同一個客戶下的聯絡人，其 Email 不能重複。
* 實作一個「自訂輸入驗證屬性」可驗證「手機」的電話格式必須為 "\d{4}-\d{6}" 的格式 ( e.g. 0911-111111 )
* 使用 Repository Pattern 管理所有新刪查改(CRUD)等功能
* 修改所有的「刪除」邏輯，所有資料都不能真正被刪除，資料庫中也要新增「是否已刪除」欄位 (欄位要設定 bit 型態)
* 修改「客戶資料」表格，新增「客戶分類」欄位，可設定特定選項的分類
* 在「客戶資料列表」頁面新增篩選功能，可以用「客戶分類」欄位進行資料篩選 (下拉選單)
* 資料篩選的邏輯要寫在 Repository 的類別裡面
* 在「客戶聯絡人列表」頁面新增篩選功能，可以用「職稱」欄位進行資料篩選
* 修改「客戶資料列表」與「客戶聯絡人列表」頁面，設定讓每個欄位都能進行排序 (可升冪、可降冪排序)
* 如果可以的話，透過 JavaScript 實作一些 AJAX 操作，後端 MVC 使用 JsonResult 進行回應
* 使用 ClosedXML 這個 NuGet 套件實作資料匯出功能，每個清單頁上都要有可以匯出 Excel 檔案的功能，要用到 FileResult 下載檔案


# 【ASP.NET MVC 5 開發實戰】第二周作業專案

## 作業說明
  - 延續上周作業加以延伸開發

| #  | 項目                                                                                                                    | 是否完成 | 花費時間(min) |
|----|-------------------------------------------------------------------------------------------------------------------------|:-------:|:------------:|
| 1  | 實作在「客戶資料」的詳細資料頁面，加上「客戶聯絡人」清單                                                                        |       |           |
| 2  | 請多加利用範本檢視的開發技巧整理 View 頁面                                                                                     |   是    |           |
| 3  | 讓客戶聯絡人清單支援「批次更新」功能，可批次修改「職稱、手機、電話」這三個欄位，其他欄位不允許修改                                  |       |           |
| 4  | 在特定幾個可能發生例外的頁面，特別套用 [HandleError] 屬性，針對特定例外給予不同的自訂錯誤頁面，自訂錯誤頁面要能看出發生例外的實際原因 |       |           |
| 5  | 自訂一個動作過濾器，可以記錄每個 Action 與 ActionResult 執行的時間，資料可以輸出到 Debug 輸出視窗                                 |   是    |           |
| 6  | 套用任意一個現成的網頁版面，可下載免費版面，或使用我在課堂上給你們的版面進行套版                                                   |   是    |          |
| 7  | 網頁版面必須要有主選單(menu)、每頁都不同的導覽路標(breadcrumb)、需用到 @section                                                 |       |           |
| 8  | 請使用 X.PagedList.Mvc 這個 NuGet 套件實作列表頁的分頁功能                                                                    |       |     30      |
| 9  | 實作表單驗證機制 (FormsAuthentication)<br>- 要在「客戶資料」表格加【帳號】、【密碼】欄位<br>- 密碼欄位須加密或做雜湊處裡(Hash)<br>- 要有登入、登出功能      |       |            |
| 10 | 客戶登入後只有一個【修改客戶資料】功能，但只能修改【密碼、電話、傳真、地址、Email】等欄位，其他欄位不能被修改或更新。               |       |            |

> 參考文章: [簡介 ASP.NET 表單驗證 (FormsAuthentication) 的運作方式](http://blog.miniasp.com/post/2008/02/20/Explain-Forms-Authentication-in-ASPNET-20.aspx)

## 開發工具

- [Visual Studio 2017](https://docs.microsoft.com/zh-tw/visualstudio/releasenotes/vs2017-relnotes)
  - 請更新到最新版本
  - 建議安裝的 Visual Studio 擴充套件
    - [Web Essentials 2017](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.WebExtensionPack2017)
    - [Browser Link Inspector](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.BrowserLinkInspector2017)
    - [tangible T4 Editor 2.4.0 plus modeling tools for VS 2017](https://marketplace.visualstudio.com/items?itemName=tangibleengineeringGmbH.tangibleT4Editor240plusmodelingtoolsforVS2017)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/zh-tw/sql/ssms/download-sql-server-management-studio-ssms)
  - 請點擊【[下載 SQL Server Management Studio 17.7](https://go.microsoft.com/fwlink/?linkid=873126)】下載安裝檔。

## 專案 NuGet 套件介紹

以下是 Visual Studio 2017 15.7.4 內建的 ASP.NET MVC 5 專案範本的 NuGet 套件介紹。

### [ 後端套件 ]

- ASP.NET MVC 5.2.4
  - 官網: http://www.asp.net/mvc
  - 專案位址: https://github.com/aspnet/AspNetWebStack
  - 相關套件
    - Microsoft.AspNet.Razor 3.2.4
    - Microsoft.AspNet.WebPages 3.2.4
    - System.Diagnostics.DiagnosticSource 4.4.1
    - Microsoft.AspNet.TelemetryCorrelation 1.0.0
    - Microsoft.ApplicationInsights 2.5.1
    - Microsoft.ApplicationInsights.Agent.Intercept 2.4.0
    - Microsoft.ApplicationInsights.DependencyCollector 2.5.1
    - Microsoft.ApplicationInsights.PerfCounterCollector 2.5.1
    - Microsoft.ApplicationInsights.Web 2.5.1
    - Microsoft.ApplicationInsights.WindowsServer 2.5.1
    - Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel 2.5.1
- Microsoft.AspNet.Web.Optimization 1.1.3
  - 用來將 javascript, js 最小化 (minification) 與 打包 (bundling) 的工具
  - ASP.NET Optimization introduces a way to bundle and optimize CSS and JavaScript files.
  - 專案位址: https://aspnetoptimization.codeplex.com/
  - 官方文件: https://aspnetoptimization.codeplex.com/documentation
  - NuGet 套件: https://www.nuget.org/packages/Microsoft.AspNet.Web.Optimization
  - 相關連結
    - [Bundling and Minification | The ASP.NET Site](http://www.asp.net/mvc/overview/performance/bundling-and-minification)
    - [c# - Bundler not including .min files - Stack Overflow](http://stackoverflow.com/questions/11980458/bundler-not-including-min-files)
    - [kenhaines.net | WebGrease: As seen in Visual Studio 2012](http://kenhaines.net/post/2012/06/09/WebGrease-As-seen-in-Visual-Studio-2012.aspx)
    - [Web Optimization in Visual Studio 2012 RC | Howard Dierking](http://codebetter.com/howarddierking/2012/06/04/web-optimization-in-visual-studio-2012-rc/)
- Microsoft.Web.Infrastructure 1.0.0.0
  - 用來在執行時期動態註冊 HTTP modules (相依於 Microsoft.AspNet.Web.Optimization 套件)
- WebGrease 1.6.0
  - 用來最佳化 javascript, css 與圖片檔案 (相依於 Microsoft.AspNet.Web.Optimization 套件)
  - WebGrease is a suite of tools for optimizing javascript, css files and images.
  - 專案位址: https://webgrease.codeplex.com/
- Antlr 3.5.0.2
  - 用來解析 CSS 語法的工具 (相依於 WebGrease 套件) [ [說明](http://stackoverflow.com/questions/20412234/what-is-the-purpose-of-antlr-package-in-visual-studio-2013-asp-net-project) ]
- Newtonsoft.Json (Json.NET) 11.0.1
  - 提供 .NET 環境操作 JSON 資料 (相依於 WebGrease 套件)
  - 官網: https://www.newtonsoft.com/json
  - 專案位址: https://github.com/JamesNK/Newtonsoft.Json
- Microsoft.Net.Compilers 2.6.1
  - 此為 C# 6.0 以上的 .NET 編譯器 ("Roslyn") (The .NET Compiler Platform)
  - 專案位址: https://github.com/dotnet/roslyn
- Microsoft.CodeDom.Providers.DotNetCompilerPlatform 1.0.8
  - 此為 .NET 編譯器的 CodeDOM 提供者，用來提供解析 C# / VB.NET 原始碼的服務。

### [ 前端套件 ]

- Bootstrap 3.3.7
  - 官網: http://getbootstrap.com/
- jQuery 3.3.1
  - 官網: http://www.jquery.com/
  - 專案位址: http://github.com/jquery/jquery
- jQuery Validation 1.17.0
  - 官網: https://jqueryvalidation.org/
  - 專案位址: https://github.com/jquery-validation/jquery-validation
- Microsoft.jQuery.Unobtrusive.Validation 3.2.4
  - 用來與 ASP.NET MVC 5 表單驗證功能搭配使用的 JS 函式庫
  - 套件位址: https://www.nuget.org/packages/Microsoft.jQuery.Unobtrusive.Validation/
  - 版本說明: http://go.microsoft.com/fwlink/?LinkId=389866
- Modernizr 2.8.3
  - 官網: http://modernizr.com/

## Windows 建議設定

- [Windows 8 小技巧: 繁體中文語言如何變更預設輸入法(英文)](http://blog.miniasp.com/post/2012/06/30/Windows-8-Tips-How-to-change-default-input-method-for-languages.aspx)
  - Windows 8 之後的微軟注音輸入法，真的難用到爆炸，建議參考本文進行設定，否則 Visual Studio 2017 的開發體驗會受到影響。

## 課前學習資源

- [邊做邊學 ASP.NET MVC 4 - YouTube](https://www.youtube.com/playlist?list=PL_dAxk7-NoFt9ccYrIjFma1p8iLsQqweq)
  - 強烈建議這個系列影片可以先看過，跟著做一遍，上課會更有感覺！(本影片也適用於 ASP.NET MVC 5 版本)
- [ASP.NET MVC 5 新功能探索 - YouTube](https://www.youtube.com/playlist?list=PL_dAxk7-NoFtMR6s_aW_zAKHpIsCIzTNa)
  - 建議這個影片也可以先看過，了解一下 ASP.NET MVC 5 與 ASP.NET MVC 4 的差異之處 (其實差不多)
- [C# Fundamentals: Development for Absolute Beginners | Channel 9](https://channel9.msdn.com/Series/C-Sharp-Fundamentals-Development-for-Absolute-Beginners)
  - 如果有學員尚未接觸過 C# 程式語言，建議可以先看這個免費的教學課程。
  - 課程雖然是英文發音，但有**完整繁體中文字幕**，建議搭配中文字幕觀看！
- [HTML5 & CSS3 Fundamentals: Development for Absolute Beginners | Channel 9](https://channel9.msdn.com/Series/HTML5-CSS3-Fundamentals-Development-for-Absolute-Beginners)
  - 如果有學員不太有網頁開發經驗，建議可以先看這個免費的教學課程。
  - 課程雖然是英文發音，但有**完整繁體中文字幕**，建議搭配中文字幕觀看！
- [30 天精通 Git 版本控管](https://github.com/doggy8088/Learn-Git-in-30-days/blob/master/zh-tw/README.md)
  - 因為課程進行中的原始碼都會以 GitHub 分享給學員，各位自行實作的練習專案也建議用 Git 進行版本控管。

## 相關連結

- [ASP.NET MVC | The ASP.NET Site](https://www.asp.net/mvc)
- [The Will Will Web | ASP.NET MVC](http://blog.miniasp.com/category/ASPNET-MVC.aspx)
- [The Will Will Web | Visual Studio / C# / ASP.NET MVC / SQL Server 新手上路之學習資源整理](http://blog.miniasp.com/post/2015/07/03/Learning-Resources-for-CSharp-Visual-Studio-ASP-NET-MVC-SQL-Server.aspx)
- [ASP.NET MVC Guidance](https://docs.microsoft.com/en-us/aspnet/mvc/)
- [What's New in ASP.NET MVC 5](https://docs.microsoft.com/en-us/aspnet/mvc/mvc5)
- [What's New in ASP.NET MVC 4](https://docs.microsoft.com/en-us/aspnet/mvc/mvc4)
- [What's New in ASP.NET MVC 3](https://docs.microsoft.com/en-us/aspnet/mvc/mvc3)
- [Announcing the Release of ASP.NET MVC 5.1, Web API 2.1 and Web Pages 3.1](http://blogs.msdn.com/b/webdev/archive/2014/01/20/announcing-the-release-of-asp-net-mvc-5-1-asp-net-web-api-2-1-and-asp-net-web-pages-3-1.aspx)
- [Announcing the Release of ASP.NET MVC 5.2, Web API 2.2 and Web Pages 3.2](http://blogs.msdn.com/b/webdev/archive/2014/07/02/announcing-the-release-of-asp-net-mvc-5-2-web-api-2-2-and-web-pages-3-2.aspx)
- [Getting Started with Entity Framework 6 Code First using MVC 5](http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application)
- [Getting Started with Entity Framework 5 Code First using MVC 4](http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application)
