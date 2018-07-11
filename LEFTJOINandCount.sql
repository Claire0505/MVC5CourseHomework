SELECT a.Id, a.客戶名稱, 
		COUNT(b.Id) AS ContactCount,
	   ISNULL(c.BankCount, 0) AS BankCount
  FROM dbo.客戶資料 a
  LEFT JOIN dbo.客戶聯絡人 b ON a.Id = b.客戶Id
  LEFT JOIN (SELECT 客戶Id, COUNT(1) AS BankCount FROM dbo.客戶銀行資訊 GROUP BY 客戶Id) c ON a.Id = c.客戶Id
GROUP BY a.Id, a.客戶名稱, c.BankCount

