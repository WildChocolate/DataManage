using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class UserRoleDto:tbl_User_Role
    {
        public string UserName
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }
    }
}
