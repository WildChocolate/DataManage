using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class RoleMenuInfo:BaseInfo
    {
        public string Role
        {
            get;
            set;
        }
        public string Menu
        {
            get;
            set;
        }
        public string CanReaded
        {
            get;
            set;
        }
        public string CanUpdated
        {
            get;
            set;
        }
        public string CanDeleted
        {
            get;
            set;
        }
        public string CanCreated
        {
            get;
            set;
        }
        public static RoleMenuInfo ConvertToRoleMenuInfo(V_Role_Menu rolemenu)
        {
            var info = new RoleMenuInfo();
            info.Key = rolemenu.keyid;
            info.Role = rolemenu.MenuName;
            info.Menu = rolemenu.MenuName;
            info.CanCreated = rolemenu.C_CanCreated ? "是" : "否";
            info.CanDeleted = rolemenu.C_CanDelted ? "是" : "否";
            info.CanReaded = rolemenu.C_CanRead ? "是" : "否";
            info.CanUpdated = rolemenu.C_CanUpdated ? "是" : "否";
            return info;
        }
        public static List<RoleMenuInfo> ConvertToRoleMenuInfos(List<V_Role_Menu> rolemenus)
        {
            var infos = new List<RoleMenuInfo>();
            foreach(var item in rolemenus){
                infos.Add(ConvertToRoleMenuInfo(item));
            }
            return infos;
        }
    }
}
