﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Manage.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DATA_MANAGEEntities1 : DbContext
    {
        public DATA_MANAGEEntities1()
            : base("name=DATA_MANAGEEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_DataType> tbl_DataType { get; set; }
        public virtual DbSet<tbl_FlowStep> tbl_FlowStep { get; set; }
        public virtual DbSet<tbl_Information> tbl_Information { get; set; }
        public virtual DbSet<tbl_Menu> tbl_Menu { get; set; }
        public virtual DbSet<tbl_NameValueInfo> tbl_NameValueInfo { get; set; }
        public virtual DbSet<tbl_Role> tbl_Role { get; set; }
        public virtual DbSet<tbl_Role_Menu> tbl_Role_Menu { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }
        public virtual DbSet<tbl_User_Role> tbl_User_Role { get; set; }
        public virtual DbSet<tbl_VerifyFlow> tbl_VerifyFlow { get; set; }
    }
}
