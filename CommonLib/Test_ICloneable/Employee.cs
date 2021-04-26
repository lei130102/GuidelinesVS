using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Test_ICloneable
{
    /// <summary>
    /// 为了实现Employee对象的深拷贝，让类型实现ICloneable接口
    /// </summary>
    public class Employee : ICloneable
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            string format = "Employee ID:{0}\n"
                + "Name:{1}\n";

            return string.Format(format, EmployeeID, Name);
        }

        public object Clone()
        {
            //注意这里new了一个新对象并返回他
            Employee cloned = new Employee();

            cloned.EmployeeID = this.EmployeeID;
            cloned.Name = this.Name;

            return cloned;
        }
    }
}
