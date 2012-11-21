using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace PettiInn.Utilities
{
    public static class NH
    {
        public static bool IsInitialized(this object property)
        {
            return NHibernateUtil.IsInitialized(property);
        }
    }
}
