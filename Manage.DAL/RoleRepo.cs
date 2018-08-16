using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL
{
    public class RoleRepo:BaseRepo<tbl_Role>,IRoleRepo
    {
        public T GetRoleByKey<T>(int Key)
        {
            var db = DBContextFactory.CreateDB();
            T t = db.Database.SqlQuery<T>(@"SELECT TOP 1 [keyid]
                                              ,[C_Name]
                                              ,[C_ParentRole]
                                              , (select C_Name from [tbl_Role] t1 where t1.keyid=t2.C_ParentRole) as ParentName
                                              ,[C_Description]
                                              ,[C_CreatedDate]
                                              ,[C_UpdatedDate]
                                          FROM [DATA_MANAGE].[dbo].[tbl_Role] t2 where keyid="+Key).FirstOrDefault();
            return t;
        }
    }
}
