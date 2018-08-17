using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class MenuDataGrid:BaseDataGrid<Manage.Model.tbl_Menu>
    {
        public MenuDataGrid() 
        {
            title = new List<th>();
            title.Add(new th { field = "Key", title = "Key", width = 80 });
            title.Add(new th { field = "Name", title = "角色名", width = 100 });
            title.Add(new th { field = "Description", title = "描述", width = 100 });
            title.Add(new th { field = "Controller", title = "控制器", width = 100 });
            title.Add(new th { field = "Action", title = "动作", width = 100 });
            title.Add(new th { field = "ParentName", title = "父级菜单", width = 100 });
            title.Add(new th { field = "CreatedDate", title = "创建时间", width = 125 });
            title.Add(new th { field = "UpdatedDate", title = "修改时间", width = 125 });
        }
    }
}
