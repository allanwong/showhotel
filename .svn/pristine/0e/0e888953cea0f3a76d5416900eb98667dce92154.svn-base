using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PettiInn.DAL.Entities.EF5
{
    public partial class Entities : DbContext
    {
        public ObjectContext ObjectContext
        {
            get
            {
                var ctx = ((IObjectContextAdapter)this).ObjectContext;
                return ctx;
            }
        }
    }
}
