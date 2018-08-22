using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public class UserGrid:BaseDataGrid<Manage.Model.tbl_User>
    {
        public UserGrid()
        {
            this.title = new List<th>();
            this.title.Add(new th { field="Key", title="Key",width=50 });
            this.title.Add(new th { field = "Name", title = "名称", width = 100 });
            this.title.Add(new th { field = "LoginName", title = "帐号", width = 100 });
            this.title.Add(new th { field = "SexString", title = "性别", width = 50 });
            this.title.Add(new th { field = "CreatedDate", title = "创建时间", width = 100 });
            this.title.Add(new th { field = "UpdatedDate", title = "修改时间", width = 100 });
        }
    }
}
