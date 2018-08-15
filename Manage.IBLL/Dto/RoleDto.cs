using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class RoleDto:tbl_Role
    {
        public string ParentName
        {
            get;
            set;
        }
    }
}
