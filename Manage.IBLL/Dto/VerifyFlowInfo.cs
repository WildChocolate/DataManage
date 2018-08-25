using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class VerifyFlowInfo:BaseInfo
    {
        public string Step
        {
            get { return string.Format("<a href='FlowStep?Key={0}'>设置步骤</a>", this.Key); }
        }
        public static VerifyFlowInfo ConvertToVerifyFlow(tbl_VerifyFlow item)
        {
            var info = new VerifyFlowInfo();
            info.Key = item.keyid;
            info.Name = item.C_Name;
            info.Description = item.C_Description ?? "无";
            info.CreatedDate = item.C_CreatedDate.ToString();
            info.UpdatedDate = item.C_UpdatedDate!=null?item.C_UpdatedDate.ToString():"----";
            return info;
        }
        public static List<VerifyFlowInfo> ConvertToVerifyFlows(List<tbl_VerifyFlow> items)
        {
            var list = new List<VerifyFlowInfo>();
            foreach (var item in items)
            {
                list.Add(ConvertToVerifyFlow(item));
            }
            return list;
        }
    }
}
