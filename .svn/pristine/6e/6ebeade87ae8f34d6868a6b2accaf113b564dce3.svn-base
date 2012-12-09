using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using PettiInn.DAL.Manager.Core;

namespace PettiInn.DAL.Manager.EF5
{
    public class IOCModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDALFactory>().To<DALFactory>();
        }
    }
}
