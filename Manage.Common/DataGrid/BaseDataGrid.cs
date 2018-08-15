using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.DataGrid
{
    public abstract class BaseDataGrid<T>
    {
        public List<th> title
        {
            get;
            set;
        }
        public int total;
        [NonSerialized]
        public List<T> OriginRows;
        public dynamic rows
        {
            get;
            set;
        }
    }
}
