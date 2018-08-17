using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public abstract class BaseInfo
    {
        public int Key
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string CreatedDate
        {
            get;
            set;
        }
        public string UpdatedDate
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
    }
}
