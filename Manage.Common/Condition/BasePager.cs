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
        int pageindex = 1;
        public int pageIndex
        {
            get
            {
                if (pageindex < 1)
                    pageindex = 1;
                return pageindex;
            }
            set
            {
                pageindex = value;
            }
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
