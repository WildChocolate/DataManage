using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IBaseRepo<T> where T:class, new()
    {
        void Add(T t);
        void Update(T t);
        void Delete(T t);
        IQueryable<T> GetModels(Expression<Func<T, bool>> wherelambda);
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda);
        IEnumerable<Target> GetModelsByPage<Target>(int pageSize, int pageIndex, string sql);

        Target GetDtoByKey<Target>(int Key, string sql);

        void AddRange(IList<T> tList);
        void RemoveRange(IList<T> tList);
        int GetTableCount(string wherestring="");
        int GetTableCount(Expression<Func<T, bool>> WhereLambda);
        /// <summary>
        /// 一个业务中有可能涉及到对多张表的操作,那么可以将操作的数据,打上相应的标记,最后调用该方法,将数据一次性提交到数据库中,避免了多次链接数据库。
        /// </summary>
        bool SaveChanges();

    }
}
