using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class RoleInfo
    {
        public int Key
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int ParentRole
        {
            get;
            set;
        }
        public string ParentName
        {
            get;
            set;
        }
        public string CreatedDate
        {
            get;
            set;
        }
        public string UpdatedDate
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public static RoleInfo ConvertToRoleInfo(RoleDto roledto)
        {
            return new RoleInfo() { 
                CreatedDate=roledto.C_CreatedDate+string.Empty,
                Key= roledto.keyid,
                Name=roledto.C_Name,
                Description=roledto.C_Description??"----",
                ParentRole = roledto.C_ParentRole,
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
