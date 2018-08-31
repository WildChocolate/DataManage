using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class DataVerifyStepInfo:BaseInfo
    {
        public string UserName
        {
            get;
            set;
        }
        public string Advice
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
            get { return this.State ? "通过" : "不通过"; }
            
        }
        public static DataVerifyStepInfo ConvertToDataVerifyStepInfo(V_DataVerifyStep item )
        {
            var info = new DataVerifyStepInfo();
            info.Key = item.DataKey;
            info.Name = item.StepName;
            info.UserName = item.StepUser;
            info.Advice = item.StepAdvice;
            info.State = item.StepState ;
            info.CreatedDate = item.CreatedDate.ToString();
            info.UpdatedDate = item.UpdatedDate != null ? item.UpdatedDate.ToString() : "----";
            return info;
        }
        public static List<DataVerifyStepInfo> ConvertToDataVerifyStepInfos(List<V_DataVerifyStep> items)
        {
            var list = new List<DataVerifyStepInfo>();
            foreach (var item in items)
            {
                list.Add(ConvertToDataVerifyStepInfo(item));
            }
            return list;
        }
    }
}
