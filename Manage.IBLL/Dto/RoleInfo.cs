using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class RoleInfo:BaseInfo
    {
        public bool IsTop
        {
            get { return ParentKey == 0; }
        }
        public int ParentKey
        {
            get;
            set;
        }
        public string ParentName
        {
            get;
            set;
        }
        public string RoleFlow
        {
            get
            {
                return string.Format("<a href='{0}'>设置流程</a>", "/Role/RoleVerifyFlow?Key=" + this.Key);
            }
        }

        public static RoleInfo ConvertToRoleInfo(RoleDto roledto)
        {
            return new RoleInfo() { 
                CreatedDate=roledto.C_CreatedDate+string.Empty,
                Key= roledto.keyid,
                Name=roledto.C_Name,
                Description=roledto.C_Description??"----",
                ParentKey = roledto.C_ParentRole,
                ParentName = roledto.ParentName??"****",
                UpdatedDate = roledto.C_UpdatedDate == null ? "----" : roledto.C_UpdatedDate.ToString()
            };
        }
        public static List<RoleInfo> ConvertToRoleInfos(IEnumerable<RoleDto> roledtos)
        {
            var list = new List<RoleInfo>();
            foreach (var item in roledtos)
            {
                var dto = ConvertToRoleInfo(item);
                list.Add(dto);
            }
            return list;
        }
    }

}
