using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5CourseHomework.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
		public override IQueryable<客戶資料> All()
		{
			return base.All().Where(w => w.是否已刪除 == false);
		}

		internal IQueryable<客戶資料> Search (string keyword, string unNum, string telNum)
		{
			var data = this.All();

			if (!string.IsNullOrEmpty(keyword))
			{
				data = data.Where(w => w.客戶名稱.Contains(keyword));
			}

			if (!string.IsNullOrEmpty(unNum))
			{
				data = data.Where(w => w.統一編號.Contains(unNum));
			}

			if (!string.IsNullOrEmpty(telNum))
			{
				data = data.Where(w => w.電話.Contains(telNum));
			}

			return data;

		}

        internal IQueryable<string> GetCustomerName()
        {
            return this.All().Select(s => s.客戶名稱);
        }

        internal IQueryable<CustomerViewModel> GetContactBankCount(
           IQueryable<客戶聯絡人> contantData, IQueryable<客戶銀行資訊> bankData)
        {

            var data = (
                from customer in this.All()
                select new CustomerViewModel()
                {
                    Id = customer.Id,
                    客戶名稱 = customer.客戶名稱,
                    聯絡人數量 = contantData.Where(w => w.客戶Id.Equals(customer.Id)).Count(),
                    銀行帳戶數量 = bankData.Where(w => w.客戶Id.Equals(customer.Id)).Count()
                });
                

            return data;
        }

        internal 客戶資料 Find(int id)
		{
			return this.All().FirstOrDefault(f => f.Id == id);
		}

		public override void Delete(客戶資料 entity)
		{
			entity.是否已刪除 = true;
		}

    }


	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}