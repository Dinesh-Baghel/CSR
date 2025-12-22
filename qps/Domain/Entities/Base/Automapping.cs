using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public class Automapping
    {
        public static void CopyProperties<T, U>(T source, U destination)
        {
            var sourceProps = typeof(T).GetProperties();
            var destProps = typeof(U).GetProperties();

            foreach (var sProp in sourceProps)
            {
                var dProp = destProps.FirstOrDefault(p => p.Name == sProp.Name &&
                                                          p.PropertyType == sProp.PropertyType &&
                                                          p.CanWrite);
                if (dProp != null)
                {
                    dProp.SetValue(destination, sProp.GetValue(source));
                }
            }
        }
    }
}
