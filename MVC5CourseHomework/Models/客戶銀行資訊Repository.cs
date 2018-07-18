using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5CourseHomework.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
		public override IQueryable<客戶銀行資訊> All()
		{
			return base.All().Where(w => w.是否已刪除 == false);
		}

		internal IQueryable<客戶銀行資訊> Search(string bankName, string account)
		{
			var data = this.All();

			if (!string.IsNullOrEmpty(bankName))
			{
				data = data.Where(w => w.銀行名稱.Contains(bankName));
			}

			if (!string.IsNullOrEmpty(account))
			{
				data = data.Where(w => w.帳戶號碼.Contains(account));
			}

			return data;
		}

		internal 客戶銀行資訊 Find(int id)
		{
			return this.All().FirstOrDefault(f => f.Id == id);
		}

		public override void Delete(客戶銀行資訊 entity)
		{
			entity.是否已刪除 = true;
		}
	}

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}