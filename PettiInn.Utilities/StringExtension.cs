using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PettiInn.Utilities
{
    public static class StringExtension
    {
        public static T As<T>(this object o)
        {
            T result = default(T);

            try
            {
                if (o is IConvertible)
                {
                    result = (T)Convert.ChangeType(o, typeof(T));
                }
            }
            catch (Exception ex)
            {
                throw new InvalidCastException(string.Format("无法将类型\"{0}\"转换为\"{1}\",值:{2}", o.GetType().FullName, typeof(T).FullName, (o != null) ? o.ToString() : "null"), ex);
            }

            return result;
        }
    }
}
