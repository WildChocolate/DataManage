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
    public class RoleVerifyInfo:BaseInfo
    {
        public RoleVerifyInfo(int rolekey, int datatypekey, int? verifykey)
        {
            this.DataTypeKey = datatypekey;
            this.RoleKey = rolekey;
            this.VerifyKey = verifykey;
        }
        public int? RoleKey
        {
            get;
            set;
        }
        public int? VerifyKey
        {
            get;
            set;
        }
        public int? DataTypeKey
        {
            get;
            set;
        }
        public static RoleVerifyInfo ConvertToRoleVerifyInfo(tbl_DataType item,int datatypekey, int rolekey, int? verifykey)
        {
            var info = new RoleVerifyInfo(datatypekey, rolekey, verifykey);
            info.Key = item.keyid;
            info.Name = item.C_Name;
            info.CreatedDate = item.C_CreatedDate.ToString();
            info.UpdatedDate = item.C_UpdatedDate != null ? item.C_UpdatedDate.ToString() : "----";
            return info;
        }
        /// <summary>
        /// 查找角色与相应资料类型的流程， 如果此角色的此datatype 已经设置了流程，则要把相应的流程找出
        /// </summary>
        /// <param name="items"></param>
        /// <param name="rolekey"></param>
        /// <param name="datatypekey"></param>
        /// <returns></returns>
        public static List<RoleVerifyInfo> ConvertToRoleVerifyInfos(List<tbl_DataType> items, int rolekey)
        {
            var list = new List<RoleVerifyInfo>();
            //当一个role 和 一个datatype 确定的时候，如果 RoleVerify，则可以确定它的verifykey,所以这里在这里先进行查找
            var repo = Container.GetRepository<IRoleVerifyRepo>();
            //这里直接将所有的记录找出，不然后面每次都要根据 item查询一次数据库
            var roleverifyList = repo.GetModels(rv=> true).ToList();
            foreach (var item in items)
            {
                var datatypekey = item.keyid;
                int? verifykey = roleverifyList.Where(rv => rv.C_RoleId == rolekey && rv.C_DataTypeId == datatypekey).Select(rv => rv.C_VerifyId).FirstOrDefault();
                list.Add(ConvertToRoleVerifyInfo(item, rolekey, datatypekey, verifykey));
            }
            return list;
        }
    }
}
