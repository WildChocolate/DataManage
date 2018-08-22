using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class UserRoleGrid:BaseDataGrid<UserRoleGrid>
    {
        public UserRoleGrid()
        {
            this.title.Add(new th() { field = "Key", title = "Key" , width=50});
            this.title.Add(new th() { field = "User", title = "用户", width = 50 });
            this.title.Add(new th() { field = "Role", title = "角色", width = 50 });
            this.title.Add(new th() { field = "Key", title = "Key", width = 50 });
            this.title.Add(new th() { field = "Key", title = "Key", width = 50 });
            this.title.Add(new th() { field = "Key", title = "Key", width = 50 });
        }
    }
}
