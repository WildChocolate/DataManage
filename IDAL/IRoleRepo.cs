﻿using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IDAL
{
    public interface IRoleRepo : IBaseRepo<tbl_Role>
    {
        T GetRoleByKey<T>(int Key);
    }
}
