using Manage.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL
{
    public abstract class BaseService<T> where T:class ,new()
    {
        public BaseService()
        {
            SetDAL();
        }
        public IBaseRepo<T> Dal{get;set;}
        public abstract void SetDAL();
        public bool Add(T t)
        {
            Dal.Add(t);
            return Dal.SaveChanges() ;
        }

        public bool Update(T t)
        {
            Dal.Update(t);
            return Dal.SaveChanges();
        }

        public bool Delete(T t)
        {
            Dal.Delete(t);
            return Dal.SaveChanges();
        }

        public IQueryable<T> GetModels(System.Linq.Expressions.Expression<Func<T, bool>> wherelambda)
        {
            return Dal.GetModels(wherelambda);
        }

        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, System.Linq.Expressions.Expression<Func<T, type>> OrderByLambda, System.Linq.Expressions.Expression<Func<T, bool>> WhereLambda)
        {
            return Dal.GetModelsByPage(pageSize, pageIndex, isAsc, OrderByLambda, WhereLambda);
        }
        public IEnumerable<Target> GetModelsByPage<Target>(int pageSize, int pageIndex, string sql)
        {
            //var userdal = Dal as IUserRepo;
            return Dal.GetModelsByPage<Target>(pageSize, pageIndex, sql);
        }
        public bool AddRange(IList<T> tList) 
        {
            Dal.AddRange(tList);
            return Dal.SaveChanges();
        }
        public bool RemoveRange(IList<T> tList)
        {
            Dal.RemoveRange(tList);
            return Dal.SaveChanges();
        }
        public int GetTableCount(string wherestring="") {
            return Dal.GetTableCount(wherestring);
        }
        public int GetTableCount(System.Linq.Expressions.Expression<Func<T, bool>> WhereLambda)
        {
            return Dal.GetTableCount(WhereLambda);
        }
    }
}
