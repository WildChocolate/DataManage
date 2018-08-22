using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IUserRepo : IBaseRepo<tbl_User>
    {
        tbl_User CheckLogin(string username, string pwd);
        bool ChangePassword(string userid, string newpassword);
        /// <summary>
        /// 用户信息不须返回照片列
        /// </summary>
        /// <returns></returns>
        Manage.Common.DataGrid.UserGrid SearchUserInfos(int pageSize, int pageIndex, System.Linq.Expressions.Expression<Func<tbl_User, bool>> WhereLambda);
        
    }


}
