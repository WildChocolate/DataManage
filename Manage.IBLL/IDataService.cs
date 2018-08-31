using Manage.Common.Condition;
using Manage.Common.DataGrid;
using Manage.IBLL.Dto;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IDataService:IBaseService<tbl_Data>
    {
        DataVerifyGrid GetDataVerifyGrid(DataVerifyStepPager pager, UserDto userDto);
    }
}
