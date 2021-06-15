using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//构建数据访问组件

//在专业应用程序中，数据库代码并非嵌入窗口的代码隐藏类中，而是被封装到专门的类中。
//为得到更好的组件化特征，甚至可从应用程序中提取这些数据访问类，并将他们编译进单独的DLL组件中。当编写访问数据库的代码时，尤其如此
//(因为这些代码通常对性能特别敏感)，不管数据位于何处，这都是一种上乘的设计方法






//设计数据访问组件

//不管准备如何使用数据绑定(或甚至不使用)，数据访问代码总是应当位于单独的类中。为能够高效地维护、优化以及酌情重用数据访问代码并排除错误，
//这是唯一能够带来一丝机会的方法

//当创建数据类时，应遵循下面给出的几条基本指导原则：
//1.快速打开和关闭连接。
//在每个方法调用中打开数据库连接，并在方法结束之前关闭连接，这样，连接就不会无意中保持打开状态。确保在适当的时间关闭连接的一种方法是使用using代码块
//2.实现错误处理
//使用错误处理确保连接被关闭，即使已经引发了一个异常
//3.遵循无状态的设计规则
//通过参数接收方法需要的所有信息，并通过返回值返回检索到的所有数据。这样，在许多情况下可避免复杂化(例如，需要创建多线程应用程序或在服务器上
//驻留数据库组件)
//4.在某个位置保存连接字符串
//理想的情况是，保存在应用程序的配置文件中

namespace StoreDatabase
{
    //在应用程序中，为使窗口可使用StoreDB类，有如下几种选择：
    //1.当需要访问数据库时，窗口可随时创建StoreDB类的一个实例
    //2.可将StoreDB类中的方法改成静态方法
    //3.可创建StoreDB类的单一实例，并通过另一个类的静态属性使用该实例(遵循"工厂"模式)
    //前两种选择是合理的，但这两种选择都限制了灵活性。第一种选择不能用于缓存在多个窗口中使用的数据对象。即使不希望立即缓存数据，为便于以后实现，
    //这样设计应用程序也是值得的。类似地，第二种选择假定在StoreDbBased类中不需要保存任何特定于实例的状态。尽管这是一条良好的设计原则，但是可能
    //希望在内存中保存一些细节(如连接字符串)。如果将StoreDB类中的方法转换为静态方法，就会使得访问存储在不同后台数据存储中的不同Store数据库实例
    //变得更加困难
    //第三种选择最灵活。这种方式通过强制所有窗口通过单个属性进行工作，保存了交换台设计

    ////静态字段
    //private static StoreDbBased storeDbBased = new StoreDbBased();
    ////静态属性
    //public static StoreDbBased StoreDbBased
    //{
    //    get { return StoreDbBased; }
    //}

    public class StoreDbBased
    {
        // Get the connection string from the current configuration file.
        //连接字符串不是硬编码的，而是从应用程序的.confg文件的应用程序设置中检索得到的(为查看或设置应用程序设置，在Solution Explorer中
        //双击Properties节点，然后单击Settings选项卡)
        //创建方式：在项目的Properties上右击，创建设置文件，并设置参数，自动会创建App.config文件
        private string connectionString = StoreDatabase.Properties.Settings.Default.Store;

        public Product GetProduct(int ID)
        {
            SqlConnection con = new SqlConnection(connectionString);

            //在数据库中，通过名为GetProductByID的存储过程执行查询
            SqlCommand cmd = new SqlCommand("GetProductByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductID", ID);

            try
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if(reader.Read())
                {
                    // Create a Product object that wraps the current record.
                    Product product = new Product((string)reader["ModelNumber"],
                        (string)reader["ModelName"], (decimal)reader["UnitCost"],
                        (string)reader["Dscription"], (int)reader["CategoryID"],
                        (string)reader["CategoryName"], (string)reader["ProductImage"]);
                    return product;
                }
                else
                {
                    return null;
                }
            }
            //注意，现在GetProduct()方法尚未提供任何异常处理代码，因此所有异常都将会上传到调用代码。这是一种合理的设计选择，但您可能
            //希望在GetProduct()方法中捕获异常，酌情执行所需的清理或日志操作，然后重新抛出异常，通知调用代码发生了问题。这种设计模式
            //被称为"调用者通知"(caller inform)
            finally
            {
                con.Close();
            }
        }

        public ICollection<Product> GetProducts()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            ObservableCollection<Product> products = new ObservableCollection<Product>();
            try
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    // Create a Product object that wraps the current record.
                    Product product = new Product((string)reader["ModelNumber"],
                        (string)reader["ModelName"], (decimal)reader["UnitCost"],
                        (string)reader["Description"], (int)reader["CategoryID"],
                        (string)reader["CategoryName"], (string)reader["ProductImage"]);
                    // Add to collection
                    products.Add(product);
                }
            }
            finally
            {
                con.Close();
            }
            return products;
        }

        public ICollection<Product> GetProductsSlow()
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            return GetProducts();
        }

        public ICollection<Product> GetProductsFilteredWithLinq(decimal minimumCost)
        {
            // Get the full list of products.
            ICollection<Product> products = GetProducts();

            // Create a second collection with matching products.
            IEnumerable<Product> matches = from product in products where product.UnitCost >= minimumCost select product;

            return new ObservableCollection<Product>(matches.ToList());
        }

        public ICollection<Category> GetCategoriesAndProducts()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Products");
            cmd.CommandText = "GetCategories";
            adapter.Fill(ds, "Categories");

            // Set up a relation between these tables (optional).
            DataRelation relCategoryProduct = new DataRelation("CategoryProduct",
                ds.Tables["Categories"].Columns["CategoryID"],
                ds.Tables["Products"].Columns["CategoryID"]);
            ds.Relations.Add(relCategoryProduct);

            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            foreach(DataRow categoryRow in ds.Tables["Categories"].Rows)
            {
                ObservableCollection<Product> products = new ObservableCollection<Product>();
                foreach(DataRow productRow in categoryRow.GetChildRows(relCategoryProduct))
                {
                    products.Add(new Product(productRow["ModelNumber"].ToString(),
                        productRow["ModelName"].ToString(), (decimal)productRow["UnitCost"],
                        productRow["Description"].ToString()));
                }
                categories.Add(new Category(categoryRow["CategoryName"].ToString(), products));
            }
            return categories;
        }

        public ICollection<Category> GetCategories()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("GetCategories", con);
            cmd.CommandType = CommandType.StoredProcedure;

            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            try
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    // Create a Category object that wraps the current record.
                    Category category = new Category((string)reader["CategoryName"], (int)reader["CategoryID"]);

                    // Add to collection
                    categories.Add(category);
                }
            }
            finally
            {
                con.Close();
            }

            return categories;
        }
    }
}
