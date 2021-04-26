using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Test_ICloneable
{
    public static class Test
    {
        public static void run()
        {
            //深拷贝 对象
            Employee src = new Employee();
            src.EmployeeID = 0;
            src.Name = "张三";

            Employee dst = src.Clone() as Employee;
            if(dst != null)
            {
                dst.EmployeeID = 1;
                dst.Name = "李四";
            }

            //深拷贝 对象数组
            List<Employee> srcList = new List<Employee>();
            srcList.Add(src);
            srcList.Add(dst);

            List<Employee> dstList = new List<Employee>();
            srcList.ForEach(emp => dstList.Add(emp.Clone() as Employee));
        }
    }
}
