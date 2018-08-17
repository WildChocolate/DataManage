using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IMenuRepo : IBaseRepo<tbl_Menu>
    {
        T GetMenuBykey<T>(int Key);
    }
}
