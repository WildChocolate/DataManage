
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Manage.BLLContainer.Dto
{
    public class UserDto
    {
        //public UserDto(tbl_User user)
        //{
        //    this.User = user;
        //    var uroleservice = Container.GetService<IUserRoleService>();
        //    var roleservice = Container.GetService<IRoleService>();
        //    /*查找角色*/
        //    var roleIds = uroleservice.GetModels(r => r.C_UserId == this.User.key).Select(ur => ur.C_RoleId);
        //    var roles = roleservice.GetModels(r => roleIds.Contains(r.key));
        //    this.Roles = roles.ToList();
        //    /*搜索菜单*/
        //    var menuservice = Container.GetService<IMenuService>();
        //    var rmenuservice = Container.GetService<IRoleMenuService>();
        //    //角色菜单，每个角色与对应菜单的 权限
        //    var roleMenus = rmenuservice.GetModels(rm => roleIds.Contains(rm.C_RoleId)).Distinct();
        //    var menuIds = roleMenus.Select(rm => rm.key);
        //    var menus = menuservice.GetModels(m => menuIds.Contains(m.key));
        //    if (menus != null)
        //        this.Menus = menus.ToList();

        //    /*
        //     * 查找权限
        //     * 如果一个用户有两个角色，一个对a菜单有 read，一个对a菜单write,那用户权限应该为 合并后的 read+write
        //     */
        //    foreach (var menu in menus)
        //    {
        //        var rolemenu = new tbl_Role_Menu() { C_MenuId = menu.key };
        //        rolemenu.C_CanCreated = rolemenu.C_CanDelted = rolemenu.C_CanRead = rolemenu.C_CanUpdated = false;

        //        //通过菜单id 把不同角色的权限设置分到同一组
        //        var powers = roleMenus.Where(rm => rm.C_MenuId == menu.key);
        //        foreach (var p in powers)
        //        {
        //            if (p.C_CanCreated)
        //                rolemenu.C_CanCreated = p.C_CanCreated;
        //            if (p.C_CanDelted)
        //                rolemenu.C_CanDelted = p.C_CanDelted;
        //            if (p.C_CanRead)
        //                rolemenu.C_CanRead = p.C_CanRead;
        //            if (p.C_CanUpdated)
        //                rolemenu.C_CanUpdated = p.C_CanUpdated;
        //        }
        //        this.MenuPowers.Add(rolemenu);
        //    }
        //}
        //public tbl_User User
        //{
        //    get;
        //    set;
        //}
        //public List<tbl_Role_Menu> MenuPowers
        //{
        //    get;
        //    set;
        //}
        //public List<tbl_Role> Roles
        //{
        //    get;
        //    set;
        //}
        //public List<tbl_Menu> Menus
        //{
        //    get;
        //    set;
        //}
        //public static implicit operator tbl_User(UserDto userDto)
        //{
        //    return userDto.User;
        //}
    }
}