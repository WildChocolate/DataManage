using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class DataVerifyGrid:BaseDataGrid<tbl_Data>
    {
        public DataVerifyGrid()
        {
            title = new List<th>();
            title.Add(new th { field="Key", title="Key", width= 50});
            title.Add(new th { field = "Name", title = "Name", width = 100 });
            title.Add(new th { field = "Descriptin", title = "Descriptin", width = 100 });
            title.Add(new th { field = "CreatedDate", title = "CreatedDate", width = 100 });
            title.Add(new th { field = "UpdatedDate", title = "UpdatedDate", width = 100 });
            title.Add(new th { field = "Detail", title = "详情", width = 100 });
            title.Add(new th { field = "Verify", title = "操作", width = 100 });
        }
    }
}
