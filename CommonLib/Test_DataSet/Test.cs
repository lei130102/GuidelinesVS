using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Test_DataSet
{
    public static class Test
    {
        public static void DataSetReadXml()
        {
            //注意：
            //
            //1.在表的属性中设置每个字段的属性
            //2.xsd文件中的DataSet的Name属性必须要和xml文件中的根节点名一致
            //3.xsd文件中的DataSet的NameSpace属性必须要和xml文件中的根节点中描述一致
            //4.修改store.xsd和store.xml的复制到输出目录为始终复制

            DataSet ds = new DataSet();
            //ds.ReadXmlSchema("./Test_DataSet/bookstore.xml");
            //ds.ReadXml("./Test_DataSet/bookstore.xml");
            ds.ReadXmlSchema("./Test_DataSet/store.xml");
            string xmlSchema = ds.GetXmlSchema();

            ds.ReadXml("./Test_DataSet/store.xml");
            string xml = ds.GetXml();

            int count = ds.Tables[0].Rows.Count;
        }

        public static void run()
        {
            DataSetReadXml();
        }
    }
}
