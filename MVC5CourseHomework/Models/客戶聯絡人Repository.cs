using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5CourseHomework.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
		public override IQueryable<客戶聯絡人> All()
		{
			return base.All().Where(w => w.是否已刪除 == false);
		}

		internal IQueryable<客戶聯絡人> Search(string jobTit,string name, string phone)
		{
			var data = this.All();

			if (!string.IsNullOrEmpty(jobTit))
			{
				data = data.Where(w => w.職稱.Equals(jobTit));
			}
			if (!string.IsNullOrEmpty(name))
			{
				data = data.Where(w => w.姓名.Contains(name));
			}
			if (!string.IsNullOrEmpty(phone))
			{
				data = data.Where(w => w.手機.Contains(phone));
			}

			return data;
		}

        //在「客戶聯絡人列表」頁面新增篩選功能，可以用「職稱」欄位進行資料篩選
        internal IQueryable<string> GetJobTitle()
        {
            return this.All().Select(s => s.職稱).Distinct();
        }

        // 實作「客戶聯絡人」時，同一個客戶下的聯絡人，其 Email 不能重複。
        internal bool CheckEmail(int id, string email, int 客戶id)
		{
			bool boolResult = false;

			if (id == 0)
			{
				// 新增時檢查 E-Mail 是否重複
				boolResult = this.All().Any(w => w.Email.Equals(email) && w.客戶Id.Equals(客戶id));
			}
			else
			{
				// 編輯時檢查 E-Mail 是否重複，需排除本身 id
				boolResult = this.All().Any(w => w.Email.Equals(email) && w.客戶Id.Equals(客戶id) & w.Id != id);
			}

			return boolResult;
		}

		internal 客戶聯絡人 Find(int id)
		{
			return this.All().FirstOrDefault(f => f.Id == id);
		}

		public override void Delete(客戶聯絡人 entity)
		{
			entity.是否已刪除 = true;
		}

	   
	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}