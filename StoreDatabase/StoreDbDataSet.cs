using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDatabase
{
    public class StoreDbDataSet
    {
        public DataTable GetProducts()
        {
            return ReadDataSet().Tables[0];
        }

        public DataSet GetCategoriesAndProducts()
        {
            return ReadDataSet();
        }

        internal static DataSet ReadDataSet()
        {
            //注意：
            //
            //1.在两个表的属性中设置每个字段的属性
            //2.xsd文件中的DataSet的Name属性必须要和xml文件中的根节点名一致
            //3.xsd文件中的DataSet的NameSpace属性必须要和xml文件中的根节点中描述一致
            //4.修改store.xsd和store.xml的复制到输出目录为始终复制
            DataSet ds = new DataSet();

            ds.ReadXmlSchema("./store.xsd");//store.xsd是数据集

            //测试检查
            string xmlSchema = ds.GetXmlSchema();

            ds.ReadXml("./store.xml");

            //测试检查
            string xml = ds.GetXml();

            return ds;
        }
    }
}
