﻿using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL
{
    public interface IRoleService : IBaseService<tbl_Role>
    {
        T GetRoleDtoByKey<T>(int Key);
    }
}
