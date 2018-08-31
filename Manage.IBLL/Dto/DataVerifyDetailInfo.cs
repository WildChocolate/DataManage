using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class DataVerifyDetailInfo
    {
        public DataInfo Data
        {
            get;
            set;
        }
        public IList<DataVerifyStepInfo> Steps
        {
            get;
            set;
        }
    }
}
