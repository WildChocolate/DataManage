using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class DataTypeInfo:BaseInfo
    {
        public static DataTypeInfo ConvertoToDataTypeInfo(tbl_DataType item)
        {
            var info = new DataTypeInfo();
            info.Key = item.keyid;
            info.Name = item.C_Name;
            info.Description = item.C_Description;
            info.CreatedDate = item.C_CreatedDate.ToString();
            info.UpdatedDate = item.C_UpdatedDate != null ? item.C_UpdatedDate.ToString() : "----";
            return info;
        }
        public static List<DataTypeInfo> ConvertoToDataTypeInfos(List<tbl_DataType> items)
        {
            var list = new List<DataTypeInfo>();
            foreach (var item in items)
            {
                list.Add(ConvertoToDataTypeInfo(item));
            }            
            return list;
        }
    }
}
