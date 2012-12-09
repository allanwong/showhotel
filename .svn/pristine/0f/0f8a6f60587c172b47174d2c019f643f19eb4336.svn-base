using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Manager.Core;
using PettiInn.Utilities;

namespace PettiInn.DAL.Manager.EF5
{
    public class DALFactory : IDALFactory
    {
        private const string CACHE_KEY = "DALFactory.ManagerTypes";

        /// <summary>
        /// 通过反射找到指定的DAL接口类初始化DAL实例
        /// </summary>
        /// <typeparam name="TManager">DAL接口类，如ICityManager</typeparam>
        /// <returns>DAL实例</returns>
        public TManager Create<TManager>()
        {
            //通过命名空间取得所有DAL类
            var types = this.GetTypes();
            var manager = types[typeof(TManager)];

            //用反射初始化实例,并且强类型转换
            var instance = (TManager)FastActivator.Create(manager);

            return instance;
        }

        /// <summary>
        /// 得到DAL接口类-实类的字典集合
        /// </summary>
        /// <returns>IDictionary<DAL接口类, DAL实类></returns>
        public IDictionary<Type, Type> GetTypes()
        {
            var types = MemoryCache.Default[CACHE_KEY] as IDictionary<Type, Type>;

            if (types == null)
            {
                types = new Dictionary<Type, Type>();
                var managerTypes = from ass in Assembly.GetExecutingAssembly().GetTypes() where !ass.IsNested && !string.IsNullOrWhiteSpace(ass.Namespace) && ass.Namespace.Equals("PettiInn.DAL.Manager.EF5.Managers") select ass;

                Array.ForEach(managerTypes.ToArray(), t => types.Add(t.GetInterface("I" + t.Name), t));
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(5)
                };
                MemoryCache.Default.Add(CACHE_KEY, types, policy);
            }


            return types;
        }
    }
}
