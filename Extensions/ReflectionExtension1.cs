using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice_tedliu.Extensions
{
    public static class ReflectionExtension1
    {
        public static string GetPeopertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item,null).ToString();
        }
    }
}
