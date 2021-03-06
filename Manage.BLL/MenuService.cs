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
    public class MenuService : BaseService<tbl_Menu>, IMenuService
    {
        public override void SetDAL()
        {
            Dal = DALContainer.Container.GetRepository<IMenuRepo>();
        }

        public IBLL.Dto.MenuDto GetMenuDtoByKey(int Key)
        {
            var menudal = Dal as IMenuRepo;
            return menudal.GetMenuBykey<IBLL.Dto.MenuDto>(Key);
        }
    }
}
