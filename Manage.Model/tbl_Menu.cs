//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tbl_Menu
    {
        public int keyid { get; set; }
        public string C_Name { get; set; }
        public string C_Description { get; set; }
        public int C_ParentMenu { get; set; }
        public string C_Controller { get; set; }
        public string C_Action { get; set; }
        public System.DateTime C_CreatedDate { get; set; }
        public Nullable<System.DateTime> C_UpdatedDate { get; set; }
    }
}
