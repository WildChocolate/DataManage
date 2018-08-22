using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IRoleMenuRepo : IBaseRepo<tbl_Role_Menu>
    {
        IQueryable<V_Role_Menu> GetVModels(Expression<Func<V_Role_Menu, bool>> wherelambda);
    }
}
