using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class DataInfoGrid:BaseDataGrid<tbl_Data>
    {
        public DataInfoGrid()
        {
            this.title = new List<th>();
            title.Add(new th { field="Key", title="Key", width=50});
            title.Add(new th { field = "Name", title = "名称", width = 50 });
            title.Add(new th { field = "Description", title = "描述", width = 50 });
            title.Add(new th { field = "Path", title = "路径", width = 100 });
            title.Add(new th { field = "StateText", title = "已审核", width = 50 });
            title.Add(new th { field = "CreatedDate", title = "创建时间", width = 100 });
            title.Add(new th { field = "UpdatedDate", title = "修改时间", width = 100 });
        }
    }
}
