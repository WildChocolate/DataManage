﻿using Manage.IBLL;
using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL
{
    public class RoleMenuService : BaseService<tbl_Role_Menu>, IRoleMenuService
    {
        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<IRoleMenuRepo>();
        }

        public IQueryable<V_Role_Menu> GetVModels(System.Linq.Expressions.Expression<Func<V_Role_Menu, bool>> wherelambda)
        {
            var rmdal = Dal as IRoleMenuRepo;
            return rmdal.GetVModels(wherelambda);
        }
    }
}
