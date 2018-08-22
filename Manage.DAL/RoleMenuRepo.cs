using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL
{
    public  class RoleMenuRepo:BaseRepo<tbl_Role_Menu>,IRoleMenuRepo
    {
        public IQueryable<V_Role_Menu> GetVModels(System.Linq.Expressions.Expression<Func<V_Role_Menu, bool>> wherelambda)
        {
            var db = DBContextFactory.CreateDB();
            var entities = db.Set<V_Role_Menu>();
            return entities.Where(wherelambda);
        }
    }
}
