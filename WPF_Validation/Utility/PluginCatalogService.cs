using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Validation.Utility
{
    /// <summary>
    /// 插件管理服务
    /// </summary>
    public class PluginCatalogService
    {
        private static PluginCatalogService _instance;
        private static object _objLock = new object();
        public static PluginCatalogService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PluginCatalogService();
                        }
                    }
                }
                return _instance;
            }
        }

        private CompositionContainer _container;
        public CompositionContainer Container
        {
            get
            {
                return _container;
            }
        }

        private PluginCatalogService()
        {
            Assembly ass = Assembly.GetExecutingAssembly();
            AssemblyCatalog asscata = new AssemblyCatalog(ass);

            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(asscata);

            _container = new CompositionContainer(catalog);
        }

        /// <summary>
        /// 从指定的路径读取插件并注册
        /// </summary>
        /// <param name="dirPath"></param>
        public void RegisitCatalog(string dirPath)
        {
            DirectoryCatalog dirCatalog = new DirectoryCatalog(dirPath);

            AggregateCatalog catalog = _container.Catalog as AggregateCatalog;
            catalog.Catalogs.Add(dirCatalog);
        }

        public void RegisitCatalog(Assembly assembly)
        {
            AssemblyCatalog assCatalog = new AssemblyCatalog(assembly);

            AggregateCatalog catalog = _container.Catalog as AggregateCatalog;
            catalog.Catalogs.Add(assCatalog);
        }

        /// <summary>
        /// 根据类型查找插件。如果查找到的插件不止一个，那么默认取第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetPlugin<T>(string type)
        {
            T t = default(T);
            IEnumerable<Lazy<T, IPluginMetadata>> part = _container.GetExports<T, IPluginMetadata>();
            Lazy<T, IPluginMetadata> obj = part.FirstOrDefault(c => c.Metadata.Type.Contains(type));
            if (obj != null && obj.Value != null)
            {
                t = obj.Value;
            }
            return t;
        }

        /// <summary>
        /// 获取指定类型的插件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindPlugin<T>()
        {
            Lazy<T> obj = null;
            try
            {
                obj = _container.GetExport<T>();
            }
            catch (Exception ex)
            {
            }
            return obj.Value;
        }

        /// <summary>
        /// 根据协定名获取插件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindPlugin<T>(string name)
        {
            T obj = _container.GetExportedValueOrDefault<T>(name);
            return obj;
        }

        /// <summary>
        /// 根据协定名获取插件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<T> FindPluginSet<T>(string name)
        {
            IEnumerable<T> obj = _container.GetExportedValues<T>(name);
            return obj;
        }
    }
}
