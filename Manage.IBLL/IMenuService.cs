﻿using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IMenuService : IBaseService<tbl_Menu>
    {
        Dto.MenuDto GetMenuDtoByKey(int Key);
    }
}
