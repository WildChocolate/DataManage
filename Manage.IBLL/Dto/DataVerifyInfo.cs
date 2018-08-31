using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    /// <summary>
    /// 扩展自DataInfo类，添加了两个用于属性，一个用于审核操作，一个用于查看实情
    /// </summary>
    public class DataVerifyInfo:BaseInfo
    {
        public string Detail
        {
            get {
                return string.Format("<a href='DataVerifyDetails?Key={0}'>详细</a>",this.Key);
            }
        }
        public string Verify
        {
            get;
            set;
        }
        public static DataVerifyInfo ConvertToDataVerifyInfo(tbl_Data item)
        {
            var info = new DataVerifyInfo();
            info.Key = item.keyid;
            info.Name = item.C_Name;
            info.Description = item.C_Description;
            info.CreatedDate = item.C_CreatedDate.ToString();
            info.UpdatedDate = item.C_UpdatedDate != null ? item.C_UpdatedDate.ToString() : "----";
            return info;
        }
        public static List<DataVerifyInfo> ConvertToDataVerifyInfos(IEnumerable<tbl_Data> items)
        {
            var list = new List<DataVerifyInfo> ();
            if (items != null)
            {
                foreach (var item in items)
                {
                    list.Add(ConvertToDataVerifyInfo(item));
                }
            }
            return list ;
        }
    }
}
