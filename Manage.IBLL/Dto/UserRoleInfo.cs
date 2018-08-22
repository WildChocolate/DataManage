using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class UserRoleInfo
    {
        public int Key
        {
            get;
            set;
        }
        public string Role
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string CreatedDate
        {
            get;
            set;
        }
        public string UpdatedDate
        {
            get;
            set;
        }
        public static UserRoleInfo ConvertToUserRoleInfo(V_User_Role userrole)
        {
            var info = new UserRoleInfo();
            info.Name = userrole.UserName;
            info.Role = userrole.RoleName;
            info.CreatedDate = userrole.C_CreatedDate.ToString();
            info.UpdatedDate = userrole.C_UpdatedDate==null?"----":userrole.C_UpdatedDate.ToString();
            return info;
        }
        public static List<UserRoleInfo> ConvertToUserRoleInfos(List<V_User_Role> userroles)
        {
            var list = new List<UserRoleInfo>();
            foreach (var item in userroles)
            {
                list.Add(ConvertToUserRoleInfo(item));
            }
            return list;
        }
    }
}
