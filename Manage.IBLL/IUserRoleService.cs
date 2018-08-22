using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IUserRoleService : IBaseService<tbl_User_Role>
    {
        IQueryable<V_User_Role> GetModels(System.Linq.Expressions.Expression<Func<V_User_Role, bool>> wherelambda);
        bool UpdateUserRoles(int userid, int[] Roles);
    }
}
