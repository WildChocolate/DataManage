using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class VerifyFlowGrid:BaseDataGrid<tbl_VerifyFlow>
    {
        public VerifyFlowGrid()
        {
            title = new List<th>();
            title.Add(new th {field="Key", title="Key", width=80 });
            title.Add(new th { field = "Name", title = "流程名", width = 80 });
            title.Add(new th { field = "Description", title = "备注", width = 80 });
            title.Add(new th { field = "CreatedDate", title = "创建时间", width = 80 });
            title.Add(new th { field = "UpdatedDate", title = "修改时间", width = 80 });
            title.Add(new th { field = "Step", title = "流程步骤", width = 100 });
        }
    }
}
