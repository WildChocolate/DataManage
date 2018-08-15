using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Model
{
    public class tbl_User_NoPhoto
    {
        public int keyid { get; set; }
        public string C_Name { get; set; }
        public string C_LoginName { get; set; }
        public string C_PassWord { get; set; }
        public bool C_Sex { get; set; }
        public bool C_Enabled { get; set; }

        public System.DateTime C_CreatedDate { get; set; }
        public Nullable<System.DateTime> C_UpdatedDate { get; set; }
    }
}
