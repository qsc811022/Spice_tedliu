using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace Spice_tedliu.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items,int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text=item.GetPeopertyValue("Name"),
                       Value=item.GetPeopertyValue("Id"),
                       Selected=item.GetPeopertyValue("Id").Equals(selectedValue.ToString())

                   };
        }
    }
}
