using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common
{
    public class ContentTypeMap:Dictionary<string, string>
    {
        public string this[string type]
        {
            get
            {
                return base[type];
            }
            set
            {
                base[type] = value;
            }
        }
        public void Add(DataTypeEnum type, string value)
        {
            base.Add(type.ToString(), value);
        }
        public string this[DataTypeEnum type]
        {
            get
            {
                return this[type.ToString()];
            }
            set
            {
                this[type.ToString()] = value;
            }
        }  

    }
}
