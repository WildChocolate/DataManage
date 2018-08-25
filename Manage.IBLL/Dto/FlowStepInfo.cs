using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class FlowStepInfo:BaseInfo
    {
        public int VerifyKey
        {
            get;
            set;
        }
        public int Step
        {
            get;
            set;
        }
        public int StepRole
        {
            get;
            set;
        }
        public static tbl_FlowStep ConvertTotblFlowStep(FlowStepInfo info)
        {
            var item = new tbl_FlowStep();
            item.C_Name = info.Name;
            item.C_VerifyId = info.VerifyKey;
            item.C_RoleId = info.StepRole;
            item.C_Description = info.Description;
            item.C_Step = info.Step;
            item.C_CreatedDate = DateTime.Now;
            return item;
        }
        public static IList<tbl_FlowStep >ConvertTotblFlowSteps(IList<FlowStepInfo> infos)
        {
            var list = new List<tbl_FlowStep>();
            foreach (var info in infos)
            {
                list.Add(ConvertTotblFlowStep(info));
            }
            return list;
        }
    }
}
