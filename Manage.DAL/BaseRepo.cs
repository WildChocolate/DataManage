using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.Model;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Manage.DAL
{
    public class BaseRepo<T> where T : class, new()
    {
        DbContext dbContext = DBContextFactory.CreateDB();
        public void Add(T t)
        {
            dbContext.Set<T>().Add(t);
        }
        public virtual void Update(T t)
        {
            dbContext.Set<T>().Attach(t);
            dbContext.Entry<T>(t).State = EntityState.Modified;
        }
        public void Delete(T t)
        {
            dbContext.Set<T>().Remove(t);
        }
        public IQueryable<T> GetModels(Expression<Func<T, bool>> wherelambda)
        {
            return dbContext.Set<T>().Where(wherelambda);
        }
        public IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc,
    Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda)
        {
            //是否升序
            if (isAsc)
            {
                //return dbContext.Set<T>().Where(WhereLambda).OrderBy(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return dbContext.Set<T>().Where(WhereLambda.Compile()).AsQueryable().OrderBy(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                //return dbContext.Set<T>().Where(WhereLambda).OrderByDescending(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return dbContext.Set<T>().Where(WhereLambda.Compile()).AsQueryable().OrderByDescending(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }
        public IEnumerable<Target> GetModelsByPage<Target>(int pageSize, int pageIndex, string sql)
        {
            //var users = DBContextFactory.CreateDB().Set<tbl_User_NoPhoto>().SqlQuery(sql).ToArray();
            var entities = DBContextFactory.CreateDB().Database.SqlQuery<Target>(sql).ToList().Skip(pageSize*(pageIndex-1)).Take(pageSize);
            return entities;

        }
        
        public int GetTableCount(string wherestring="")
        {
            var tbName = typeof(T).Name;
            var sql = "select count(1) from "+tbName +wherestring;
            var cnt = dbContext.Database.SqlQuery<int>(sql).SingleOrDefault();
            return cnt;
        }
        public int GetTableCount(Expression<Func<T, bool>> wherelambda)
        {
            var cnt = dbContext.Set<T>().Where(wherelambda.Compile()).Count();
            return cnt;
        }
        /// <summary>
        ///  通过 keyid 得到特定的dto 定型
        /// </summary>
        /// <typeparam name="Target">返回类型</typeparam>
        /// <param name="Key"> keyid </param>
        /// <param name="sql">对应 Target 类型的sql， sql的列必须 大于或者等于 Targer 类中的属性</param>
        /// <returns></returns>
        public Target GetDtoByKey<Target>(int Key, string sql)
        {
            return dbContext.Database.SqlQuery<Target>(sql+" where keyid="+Key).FirstOrDefault();
        }
        public void AddRange(IList<T> tList) 
        {
            dbContext.Set<T>().AddRange(tList);
        }
        public void RemoveRange(IList<T> tList)
        {
            dbContext.Set<T>().RemoveRange(tList);
        }

        public bool SaveChanges()
        {
            return dbContext.SaveChanges() > 0;
        }
    }
}
