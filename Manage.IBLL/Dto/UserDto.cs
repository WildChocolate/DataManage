using Manage.DALContainer;
using Manage.IDAL;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public class UserDto
    {
        
        public tbl_User User
        {
            get;
            set;
        }
        List<tbl_Role_Menu> menuPowers;
        public List<tbl_Role_Menu> MenuPowers
        {
            get {
                if (menuPowers == null)
                    menuPowers = new List<tbl_Role_Menu>();
                return menuPowers;
            }
            set
            {
                menuPowers = value;
            }
        }
        public List<tbl_Role> Roles
        {
            get;
            set;
        }
        public List<tbl_Menu> Menus
        {
            get;
            set;
        }
        public tbl_Role_Menu GetPower(object controller, string action)
        {
            if (this.MenuPowers != null)
            {
                var targetmenu = Menus.Where(m=> controller.GetType().Name== (m.C_Controller+"Controller")&&m.C_Action==action).FirstOrDefault();
                if (targetmenu != null)
                {
                    var power = this.MenuPowers.Where(mp => mp.C_MenuId == targetmenu.keyid).FirstOrDefault();
                    if (power != null)
                        return power;
                }
            }
            return new tbl_Role_Menu {C_CanDelted=false, C_CanCreated=false, C_CanRead=false, C_CanUpdated=false};
        }
        public static implicit operator tbl_User(UserDto userDto)
        {
            return userDto.User;
        }
    }
}
