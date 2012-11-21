using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PettiInn.DAL.Manager.Core
{
    public interface IDALFactory
    {
        /// <summary>
        /// 通过反射找到指定的DAL接口类初始化DAL实例
        /// </summary>
        /// <typeparam name="TManager">DAL接口类，如ICityManager</typeparam>
        /// <returns>DAL实例</returns>
        TManager Create<TManager>();

        /// <summary>
        /// 得到DAL接口类-实类的字典集合
        /// </summary>
        /// <returns>DAL接口类,实类键值集合</returns>
        IDictionary<Type, Type> GetTypes();
    }
}
