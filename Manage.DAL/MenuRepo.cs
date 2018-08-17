using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL
{
    public class MenuRepo:BaseRepo<tbl_Menu>, IMenuRepo
    {
        public T GetMenuBykey<T>(int Key)
        {
            var sql = @"SELECT top 1 [keyid]
                              ,[C_Name]
                              ,[C_Description]
                              ,[C_ParentMenu]
                              ,( select C_Name from tbl_menu t1 where t1.keyid=t2.C_ParentMenu)ParentName
                              ,[C_Controller]
                              ,[C_Action]
                              ,[C_CreatedDate]
                              ,[C_UpdatedDate]
                          FROM [DATA_MANAGE].[dbo].[tbl_Menu] t2";
            return base.GetDtoByKey<T>(Key, sql);
        }
    }
}
