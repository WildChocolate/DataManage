using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Manage.Model;

namespace Manage.DAL
{
    public class DBContextFactory
    {
        public static DbContext CreateDB()
        {
            var context = CallContext.GetData("DbContext") as DbContext ;
            if (context == null)
            {
                context = new DATA_MANAGEEntities1();
                CallContext.SetData("DbContext", context);
            }
            return context;
        }
    }
}
