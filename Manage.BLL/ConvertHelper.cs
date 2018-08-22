using Manage.IBLL.Dto;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BLL
{
    public class ConvertHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserDto ConvertUserDto(tbl_User user)
        {
            if (user == null)
                return null;
            var userDto = new UserDto();
            userDto.User = user;
            var uroleservice = new UserRoleService();
            var roleservice = new RoleService();
            /*查找角色*/
            var roleIds = uroleservice.GetModels(r => r.UserId == userDto.User.keyid).Select(ur => ur.RoleId);
            var roles = roleservice.GetModels(r => roleIds.Contains(r.keyid));
            userDto.Roles = roles.ToList();
            /*搜索菜单*/
            var menuservice = new MenuService();
            var rmenuservice = new RoleMenuService();
            //角色菜单，每个角色与对应菜单的 权限
            var roleMenus = rmenuservice.GetModels(rm => roleIds.Contains(rm.C_RoleId));
            var menuIds = roleMenus.Select(rm => rm.C_MenuId).Distinct().ToList();
            var menus = menuservice.GetModels(m => menuIds.Contains(m.keyid)).ToList();
            if (menus != null)
                userDto.Menus = menus.ToList();

            /*
             * 查找权限
             * 如果一个用户有两个角色，一个对a菜单有 read，一个对a菜单write,那用户权限应该为 合并后的 read+write
             */
            foreach (var menu in menus)
            {
                var rolemenu = new tbl_Role_Menu() { C_MenuId = menu.keyid };
                rolemenu.C_CanCreated = rolemenu.C_CanDelted = rolemenu.C_CanRead = rolemenu.C_CanUpdated = false;

                //通过菜单id 把不同角色的权限设置分到同一组
                var powers = roleMenus.Where(rm => rm.C_MenuId == menu.keyid);
                foreach (var p in powers)
                {
                    if (p.C_CanCreated)
                        rolemenu.C_CanCreated = p.C_CanCreated;
                    if (p.C_CanDelted)
                        rolemenu.C_CanDelted = p.C_CanDelted;
                    if (p.C_CanRead)
                        rolemenu.C_CanRead = p.C_CanRead;
                    if (p.C_CanUpdated)
                        rolemenu.C_CanUpdated = p.C_CanUpdated;
                }
                userDto.MenuPowers.Add(rolemenu);
            }
            return userDto;
        }

        public static UserInfo ConvertUserInfo(tbl_User user)
        {
            var userinfo = new UserInfo();
            userinfo.Key = user.keyid;
            userinfo.Name = user.C_Name;
            userinfo.LoginName = user.C_LoginName;
            userinfo.UpdatedDate = user.C_UpdatedDate+string.Empty;
            userinfo.CreatedDate = user.C_CreatedDate + string.Empty;
            userinfo.Sex = user.C_Sex;
            return userinfo;
        }
        public static UserInfo ConvertUserInfo(tbl_User_NoPhoto user)
        {
            var userinfo = new UserInfo();
            userinfo.Key = user.keyid;
            userinfo.Name = user.C_Name;
            userinfo.LoginName = user.C_LoginName;
            userinfo.UpdatedDate = user.C_UpdatedDate + string.Empty;
            userinfo.CreatedDate = user.C_CreatedDate + string.Empty;
            userinfo.Sex = user.C_Sex;
            return userinfo;
        }

    }
}
