using Manage.DALContainer;
using Manage.IBLL;
using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL
{
    public class DataTypeService:BaseService<tbl_DataType>,IDataTypeService
    {
        public override void SetDAL()
        {
            Dal = Container.GetRepository<IDateTypeRepo>();
        }
    }
}
