using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IRoleMenuService : IBaseService<tbl_Role_Menu>
    {
        IQueryable<V_Role_Menu> GetVModels(System.Linq.Expressions.Expression<Func<V_Role_Menu, bool>> wherelambda);
    }
}
