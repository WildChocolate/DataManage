using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class NameValueDataInfo:BaseInfo
    {
        public string Value
        {
            get;
            set;
        }
        public static NameValueDataInfo ConvertToNameValueDataInfo(tbl_NameValueData item)
        {
            var info = new NameValueDataInfo();
            info.Key = Convert.ToInt32(item.keyid);
            info.Name = item.C_Name;
            info.Value = item.C_Value;
            info.Description = item.C_Description;
            info.CreatedDate = item.C_CreatedDate.ToString();
            info.UpdatedDate = item.C_UpdatedDate != null ? item.C_UpdatedDate.ToString() : "----";
            return info;
        }
        public static List<NameValueDataInfo> ConvertToNameValueDataInfos(List<tbl_NameValueData> items)
        {
            var list = new List<NameValueDataInfo>();
            foreach (var item in items)
            {
                list.Add(ConvertToNameValueDataInfo(item));
            }
            return list;
        }
    }
}
