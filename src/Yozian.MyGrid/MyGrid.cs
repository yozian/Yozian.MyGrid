using System;
using System.Collections.Generic;
using System.Text;

namespace Yozian.MyGrid
{
    public static class MyGrid
    {

        public static Grid<TModel> NewGrid<TModel>(IEnumerable<TModel> source)
        {
            return new Grid<TModel>(source);
        }

    }
}
