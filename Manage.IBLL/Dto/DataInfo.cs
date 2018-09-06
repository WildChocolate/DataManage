using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class DataInfo:BaseInfo
    {
        public int UserKey
        {
            get;
            set;
        }
        public string Path
        {
            get;
            set;
        }
        public int DataTypeKey
        {
            get;
            set;
        }
        public bool State
        {
            get;
            set;
        }
        public string StateText
        {
            get
            {
                return this.State ? "是" : "否";
            }
        }
        public string Download
        {
            get;
            set;
        }
        public static DataInfo ConvertToDataInfo(tbl_Data item)
        {
            var info = new DataInfo();
            info.Key = item.keyid;
            info.Name = item.C_Name;
            info.Description = item.C_Description;
            info.UserKey = item.C_UserId;
            info.DataTypeKey = item.C_DataTypeId;
            info.Download = string.Format("<a href='Download?Key={0}&DataType={1}'>下载</a>", item.keyid, item.C_DataTypeId);
            if (!string.IsNullOrEmpty(item.C_Path) && item.C_Path.Length > 50)
            {
                item.C_Path = item.C_Path.Substring(0, 50)+"...";
            }
            info.Path = item.C_Path;
            info.State = item.C_VerifyState;
            info.CreatedDate = item.C_CreatedDate.ToString();
            info.UpdatedDate = item.C_UpdatedDate != null ? item.C_UpdatedDate.ToString() : "----";
            return info;
        }
        public static List<DataInfo> ConvertToDataInfos(List<tbl_Data> items)
        {
            var list = new List<DataInfo>();
            foreach (var item in items)
            {
                list.Add(ConvertToDataInfo(item));
            }
            return list;
        }
    }
}
