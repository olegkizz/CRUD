using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityNLayer.BLL.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeSearchString(this string search)
        {
            return search.Replace(",", "").Replace(".", "").Replace("?", "").Trim();
        }
    }
}
