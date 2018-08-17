using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.Condition
{
    /// <summary>
    /// 搜索条件类
    /// </summary>
    public abstract class BasePager
    {
        public BasePager()
        {
            pageSize = 10;
            pageIndex = 1;
        }

        public int pageSize
        {
            get;
            set;
        }
        public int pageIndex
        {
            get;
            set;
        }
        public string DateFrom
        {
            get;
            set;
        }
        public string DateTo
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }
}
