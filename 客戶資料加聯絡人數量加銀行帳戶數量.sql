SELECT  dbo.客戶資料.Id ,dbo.客戶資料.客戶名稱 , temp.聯絡人數量,temp.銀行帳戶數量
FROM dbo.客戶資料 
LEFT JOIN
( SELECT dbo.客戶資料.Id,COUNT(dbo.客戶聯絡人.客戶Id) AS 聯絡人數量, COUNT(dbo.客戶銀行資訊.客戶Id) AS 銀行帳戶數量
  FROM dbo.客戶資料
  LEFT JOIN dbo.客戶聯絡人 ON 客戶聯絡人.客戶Id = 客戶資料.Id
  LEFT JOIN dbo.客戶銀行資訊 ON 客戶銀行資訊.客戶Id = 客戶資料.Id
  GROUP BY 客戶資料.Id 
)
temp ON temp.Id = 客戶資料.Id


SELECT * FROM dbo.客戶資料

SELECT * FROM dbo.客戶聯絡人 WHERE 客戶Id = 6

SELECT * FROM dbo.客戶銀行資訊 WHERE	客戶Id IN (1, 3, 4, 6)

SELECT a.Id, a.客戶名稱, COUNT(1) AS ContentCount
  FROM dbo.客戶資料 a
LEFT JOIN dbo.客戶聯絡人 b ON a.Id = b.客戶Id
LEFT JOIN dbo.客戶銀行資訊 c ON a.Id = c.客戶Id
GROUP BY a.Id, a.客戶名稱