using Manage.IBLL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage.DALContainer;
using Manage.IDAL;
using Manage.Common;
using Manage.IBLL.Dto;

namespace Manage.BLL
{
    public class UserService:BaseService<tbl_User>, IUserService
    {
        public override void SetDAL()
        {
            //Dal = Container.GetUserRepository();
            Dal = Container.GetRepository<IUserRepo>();
        }

        public UserDto CheckLogin(string username, string pwd)
        {
            var userrepo = Dal as IUserRepo;
            var encryter = new DES();
            pwd = encryter.DesEncrypt(pwd);
            var user = userrepo.CheckLogin(username, pwd);
            
            return ConvertHelper.ConvertUserDto(user);
        }


        public bool ChangePassword(tbl_User user,string oldpassword, string newpassword)
        {
            var userrepo = Dal as IUserRepo;
            var encryter = new DES();
            oldpassword = encryter.DesEncrypt(oldpassword);
            if (user.C_PassWord == oldpassword)
            {
                //新旧密码一样，不就修改数据库了
                if (oldpassword == newpassword)
                    return true;
                newpassword = encryter.DesEncrypt(newpassword);
                return userrepo.ChangePassword(user.keyid.ToString(), newpassword);
            }
            return false;
        }


        public Manage.Common.DataGrid.UserGrid SearchUserInfos(int pageSize, int pageIndex, System.Linq.Expressions.Expression<Func<tbl_User, bool>> WhereLambda)
        {
            var userrepo = Dal as IUserRepo;

            var datagrid= userrepo.SearchUserInfos(pageSize, pageIndex, WhereLambda);
            var uilist = new List<UserInfo>();
            foreach (var row in datagrid.OriginRows)
            {
                uilist.Add(ConvertHelper.ConvertUserInfo(row));
            }
            datagrid.OriginRows = null;
            datagrid.rows = uilist;
            return datagrid;
        }
        
    }
}
