using Manage.Common.DataGrid;
using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.DAL
{
    public class UserRepo : BaseRepo<tbl_User>, IUserRepo
    {

        public tbl_User CheckLogin(string username, string pwd)
        {
            var users = DBContextFactory.CreateDB().Set<tbl_User>();
            if (users != null && users.Count() > 0)
            {
                return users.Where(u => u.C_LoginName == username && u.C_PassWord == pwd).FirstOrDefault();
            }
            else
                return null;
        }


        public bool ChangePassword(string userid,string newpassword)
        {
            var users = DBContextFactory.CreateDB().Set<tbl_User>();
            var key = int.Parse(userid);
            var target = users.Where(u => u.keyid == key).FirstOrDefault();
            if (target != null)
            {
                target.C_PassWord = newpassword;
                target.C_UpdatedDate = DateTime.Now;
                return SaveChanges();
            }
            return false;
        }




        public UserGrid SearchUserInfos(int pageSize, int pageIndex, System.Linq.Expressions.Expression<Func<tbl_User, bool>> WhereLambda)
        {
            var datagrid = new UserGrid();
            var userlist = new List<tbl_User>();
            var userset = DBContextFactory.CreateDB().Set<tbl_User>();
            datagrid.total = userset.Where(WhereLambda).Count();

            var users = userset.Where(WhereLambda).OrderBy(u => u.keyid).Skip(pageSize * (pageIndex - 1)).Select(u => new { u.keyid, u.C_Name, u.C_LoginName, u.C_PassWord, u.C_Enabled, u.C_Sex, u.C_CreatedDate, u.C_UpdatedDate }).Take(pageSize);
                foreach (var u in users)
                {
                    userlist.Add(new tbl_User() { keyid=u.keyid, C_CreatedDate=u.C_CreatedDate, C_Enabled=u.C_Enabled, C_LoginName=u.C_LoginName, C_Name=u.C_Name, C_PassWord=u.C_PassWord, C_Sex=u.C_Sex, C_UpdatedDate=u.C_UpdatedDate });
                }

                datagrid.OriginRows = userlist;
                
            return datagrid;
        }


    }
}
