using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class RoleDataGrid:BaseDataGrid<tbl_Role>
    {
        public RoleDataGrid()
        {
            title = new List<th>();
            title.Add(new th { field = "Key", title = "Key", width = 80 });
            title.Add(new th { field = "Name", title = "角色名", width = 100 });
            title.Add(new th { field = "Description", title = "描述", width = 100 });
            title.Add(new th { field = "ParentName", title = "父角色", width = 100});
            title.Add(new th { field = "CreatedDate", title = "创建时间", width = 125 });
            title.Add(new th { field = "UpdatedDate", title = "修改时间", width = 125 });
        }

    }
}
