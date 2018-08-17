using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class MenuDto:Manage.Model.tbl_Menu
    {
        public string ParentMenu
        {
            get;
            set;
        }
    }
}
