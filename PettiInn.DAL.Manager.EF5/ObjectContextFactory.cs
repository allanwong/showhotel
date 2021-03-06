﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PettiInn.DAL.Manager.EF5
{
    public class ObjectContextFactory
    {
        public static PettiInn.DAL.Entities.EF5.Entities GetCurrentObjectContext()
        {
            //从CallContext数据槽中获取EF上下文
            var objectContext = CallContext.GetData(typeof(ObjectContextFactory).FullName) as PettiInn.DAL.Entities.EF5.Entities;
            if (objectContext == null)
            {
                //如果CallContext数据槽中没有EF上下文，则创建EF上下文，并保存到CallContext数据槽中
                objectContext = new PettiInn.DAL.Entities.EF5.Entities();//当数据库替换为MySql等，只要在次出EF更换上下文即可。
                CallContext.SetData(typeof(ObjectContextFactory).FullName, objectContext);
            }
            return objectContext;
        }
    }
}
