using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class MenuInfo:BaseInfo
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
        public string Controller
        {
            get;
            set;
        }
        public string Action
        {
            get;
            set;
        }
        public static MenuInfo ConvertToMenuInfo(MenuDto dto)
        {
            var info = new MenuInfo();
            info.Key = dto.keyid;
            info.Name = dto.C_Name;
            info.Description = dto.C_Description;
            info.ParentKey = dto.C_ParentMenu;
            info.Controller = dto.C_Controller;
            info.Action = dto.C_Action;
            info.ParentName = dto.ParentMenu ?? "无";
            info.CreatedDate = dto.C_CreatedDate + string.Empty;
            info.UpdatedDate = dto.C_UpdatedDate + string.Empty;
            return info;
        }
        public static List<MenuInfo> ConvertToMenuInfos(IEnumerable<MenuDto> dtos) 
        {
            var list = new List<MenuInfo>();
            foreach (var item in dtos)
            {
                list.Add(ConvertToMenuInfo(item));
            }
            return list;
        }

    }
}
