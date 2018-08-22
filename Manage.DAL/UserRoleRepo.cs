using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL
{
    public class UserRoleRepo:BaseRepo<tbl_User_Role>,IUserRoleRepo
    {

        public IQueryable<V_User_Role> GetModels(System.Linq.Expressions.Expression<Func<V_User_Role, bool>> wherelambda)
        {
            return DBContextFactory.CreateDB().Set<V_User_Role>().Where(wherelambda);
        }



        public bool UpdateUserRoles(int userid, int[] Roles)
        {
            var db = DBContextFactory.CreateDB();
            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    var deleted_ur = base.GetModels(ur => ur.C_UserId == userid);
                    if (deleted_ur != null && deleted_ur.Count() > 0)
                    {
                        db.Set<tbl_User_Role>().RemoveRange(deleted_ur);
                    }
                    foreach (var item in Roles)
                    {
                        var new_ur = new tbl_User_Role();
                        new_ur.C_UserId = userid;
                        new_ur.C_RoleId = item;
                        new_ur.C_CreatedDate = DateTime.Now;
                        base.Add(new_ur);
                    }
                    if (SaveChanges())
                    {
                        tran.Commit();
                        return true;
                    }
                    else
                        return false;
                    
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
    }
}
