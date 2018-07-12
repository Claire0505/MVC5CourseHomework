select A.Id, A.[客戶名稱], 
       COUNT(DISTINCT B.Id) AS ContantCount,  
       COUNT(DISTINCT C.ID) AS BankCount 
  from [客戶資料] A 
  left join dbo.[客戶聯絡人] B ON B.[客戶Id] = A.Id 
  left join dbo.[客戶銀行資訊] C ON C.[客戶Id] = A.Id 
Group by A.Id, A.[客戶名稱]