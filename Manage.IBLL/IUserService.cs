
using Manage.IBLL.Dto;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IUserService : IBaseService<tbl_User>
    {
        UserDto CheckLogin(string username, string pwd);
        bool ChangePassword(string userid, string newpassword);
        Manage.Common.DataGrid.UserDataGrid SearchUserInfos(int pageSize, int pageIndex, System.Linq.Expressions.Expression<Func<tbl_User, bool>> WhereLambda);
        
    }
}
