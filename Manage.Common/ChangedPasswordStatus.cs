using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common
{
    public class ChangedPasswordStatus
    {
        public ChangedPasswordStatus()
        {
            Url = Message = string.Empty;
        }
        public bool Status
        {
            get;
            set;

        }
        public string Url
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
    }
}
