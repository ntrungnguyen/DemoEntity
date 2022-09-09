using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DBAccess;

namespace BusinessLayer
{
    public class CityFuncs
    {
		public MQSaleEntities GetContext()
		{
			return new MQSaleEntities();
		}
		#region City
		public  List<City> City_Select_All()
		{
			using (var db = new MQSaleEntities())
			{
				var ls = db.Cities.AsQueryable();
				if (ls != null && ls.Any())
					return ls.ToList();
				return new List<City>();
			}
		}
		public  City City_Select_ID(int id)
		{
			using (var db = GetContext())
			{
				return db.Cities.FirstOrDefault(s => s.ID == id);
			}
		}
		public  List<City> City_Select_IDs(List<string> IDs)
		{
			using (var db = GetContext())
			{
				var ls = db.Cities.AsQueryable();
				if (ls != null && ls.Any())
				{
					ls = ls.Where(s => IDs.Contains(s.ID.ToString()));
					return ls.ToList();
				}
				return new List<City>();
			}
		}
		public  List<City> City_Select_By(string ColumnName, string Value)
		{
			using (var db = GetContext())
			{
				ColumnName = ColumnName.ToLower();
				Value = Value.ToLower();
				string sql = "Select * From City Where CONVERT(nvarchar," + ColumnName + ") = '" + Value + "'";
				var ls = db.Cities.SqlQuery(sql);
				if (ls != null && ls.Any()) return ls.ToList<City>();
				return new List<City>();
			}
		}
		public  List<City> City_Select_By(string ColumnName, string Value, int PageSize, int PageIndex, out int TotalRows)
		{
			TotalRows = 0;
			using (var db = GetContext())
			{
				ColumnName = ColumnName.ToLower();
				Value = Value.ToLower();
				string sql = "Select * From City Where CONVERT(nvarchar," + ColumnName + ") = '" + Value + "'";
				var ls = db.Cities.SqlQuery(sql);
				if (ls != null && ls.Any())
				{
					TotalRows = ls.Count();
					return ls.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList<City>();
				}
				return new List<City>();
			}
		}
		public  int City_InsertUpdate(City obj)
		{
			using (var db = GetContext())
			{
				using (var db1 = GetContext())
				{
					var find = db.Cities.FirstOrDefault(s => s.ID == obj.ID);
					if (find != null) db1.Entry(obj).State = EntityState.Modified;
					else obj = db1.Cities.Add(obj);
					db1.SaveChanges();
					return obj.ID;
				}
			}
		}
		public  void City_Delete(int id)
		{
			using (var db = GetContext())
			{
				var obj = db.Cities.FirstOrDefault(s => s.ID == id);
				if (obj != null)
				{
					db.Cities.Remove(obj);
					db.SaveChanges();
				}
			}
		}
		public  void City_Delete_IDs(List<string> IDs)
		{
			using (var db = GetContext())
			{
				var ls = db.Cities.AsQueryable();
				if (ls != null && ls.Any())
				{
					ls = ls.Where(s => IDs.Contains(s.ID.ToString()));
					foreach (var item in ls)
						db.Cities.Remove(item);
					db.SaveChanges();
				}
			}
		}
		public  List<City> City_Find_KeyWord(string Keyword, int PageSize, int PageIndex, out int TotalRows)
		{
			TotalRows = 0;
			using (var db = GetContext())
			{
				if (!string.IsNullOrWhiteSpace(Keyword))
				{
					var obj = db.Cities.FirstOrDefault(s => s.ID.ToString().CompareTo(Keyword) == 0);
					if (obj != null)
					{
						List<City> ls = new List<City>();
						ls.Add(obj);
						TotalRows = 1;
						return ls;
					}
					var list = db.Cities.AsQueryable();
					list = list.Where(s => s.ID.ToString().Contains(Keyword)
					|| s.Name.ToLower().Contains(Keyword)
					);
					if (list != null && list.Any())
					{
						TotalRows = list.Count();
						return list.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList();
					}
				}
				else
				{
					var list = db.Cities.AsQueryable();
					if (list != null && list.Any())
					{
						TotalRows = list.Count();
						return list.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList();
					}
				}
				return new List<City>();
			}
		}
		public  void City_Import(List<City> list)
		{
			using (var db = GetContext())
			{
				using (DbContextTransaction transaction = db.Database.BeginTransaction())
				{
					try
					{
						db.Cities.AddRange(list);
						db.SaveChanges();
						transaction.Commit();
					}
					catch
					{
						transaction.Rollback();
					}
				}
			}
		}
		#endregion

	}
}
