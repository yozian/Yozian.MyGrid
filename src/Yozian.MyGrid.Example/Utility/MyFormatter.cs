using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yozian.MyGrid.Example.Utility
{
    public static class MyFormatter
    {


        public static string DateTime<T>(object value, T model)
        {
            return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
