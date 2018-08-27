using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class NameValueDataGrid : BaseDataGrid<tbl_NameValueData>
    {
        public NameValueDataGrid()
        {
            title = new List<th>();
            title.Add(new th { title="Key", field="Key", width=50});
            title.Add(new th { title = "关键字", field = "Name", width = 100 });
            title.Add(new th { title = "值", field = "Value", width = 100 });
            title.Add(new th { title = "描述", field = "Description", width = 100 });
            title.Add(new th { title = "添加时间", field = "CreatedDate", width = 100 });
            title.Add(new th { title = "修改时间", field = "UpdatedDate", width = 100 });
        }
    }
}
