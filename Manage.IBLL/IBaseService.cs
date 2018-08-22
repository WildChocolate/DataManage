using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IBaseService<T> where T:class, new ()
    {
        bool Add(T t);
        bool Update(T t);
        bool Delete(T t);
        IQueryable<T> GetModels(Expression<Func<T, bool>> wherelambda );
        IQueryable<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc, Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda);
        IEnumerable<Target> GetModelsByPage<Target>(int pageSize, int pageIndex, string sql);

        bool AddRange(IList<T> tList);
        int GetTableCount(string wherestring="");
    }
}
