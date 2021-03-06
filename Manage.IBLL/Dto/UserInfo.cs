﻿using Manage.Common;
using Manage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.IBLL.Dto
{
    public  class UserInfo:BaseInfo
    {


        public string LoginName { get; set; }
        public bool Sex { get; set; }
        public string PassWord { get; set; }
        public System.Web.HttpPostedFileBase Photo { get; set; }

        
        public string SexString
        {
            get
            {
                return Sex ? "男" : "女";
            }
        }
        public string Operation
        {
            get;
            set;
        }
        public static UserInfo ConvertUserInfo(tbl_User_NoPhoto user)
        {
            var encryter = new DES();
            var userinfo = new UserInfo();
            userinfo.Key = user.keyid;
            userinfo.Name = user.C_Name + string.Empty;
            userinfo.PassWord = encryter.DesDecrypt(user.C_PassWord);
            userinfo.LoginName = user.C_LoginName + string.Empty;
            userinfo.Sex = user.C_Sex;
            userinfo.CreatedDate = user.C_CreatedDate.ToString();
            if (user.C_UpdatedDate == null)
                userinfo.UpdatedDate = "----";
            else
                userinfo.UpdatedDate = user.C_UpdatedDate.ToString();
            return userinfo;
        }
        public static UserInfo ConvertUserInfo(tbl_User user)
        {
            var encryter = new DES();
            var userinfo = new UserInfo();
            userinfo.Key = user.keyid;
            userinfo.Name = user.C_Name+string.Empty;
            userinfo.PassWord = encryter.DesDecrypt(user.C_PassWord);
            userinfo.LoginName = user.C_LoginName+string.Empty;
            userinfo.Sex = user.C_Sex;
            userinfo.CreatedDate = user.C_CreatedDate+string.Empty;
            if (user.C_UpdatedDate == null)
                userinfo.UpdatedDate = "----";
            else
                userinfo.UpdatedDate = user.C_UpdatedDate.ToString();
            return userinfo;
        }
        public static IList<UserInfo> ConvertUserInfos(IList<tbl_User> users)
        {
            var userinfos = new List<UserInfo>();
            foreach (var u in users)
            {
                userinfos.Add(ConvertUserInfo(u));
            }
            return userinfos;
        }
        public static IList<UserInfo> ConvertUserInfos(IList<tbl_User_NoPhoto> users)
        {
            var userinfos = new List<UserInfo>();
            foreach (var u in users)
            {
                userinfos.Add(ConvertUserInfo(u));
            }
            return userinfos;
        }
    }

}
