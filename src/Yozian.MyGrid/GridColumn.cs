using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Yozian.MyGrid
{
    public class GridColumn<TModel>
    {
        internal GridColumnType Type { get; private set; }

        internal string ColumnName { get; set; }

        internal string ColumnValue { get; set; }

        internal string PropertyName { get; set; }

        internal bool IsColumnInDetail { get; private set; }


        internal Func<object, TModel, string> Formatter { get; private set; }

        internal Action<TagBuilder, TModel> CustomizedTagBuilder { get; set; }


        internal GridColumn(GridColumnType type)
        {
            this.Type = type;
            this.Formatter = (v, m) =>
            {
                if (v == null)
                {
                    return string.Empty;
                }
                return v.ToString();
            };
        }

        public GridColumn<TModel> Name(string name)
        {
            this.ColumnName = name;
            return this;
        }

        public GridColumn<TModel> InDetail()
        {
            this.IsColumnInDetail = true;
            return this;
        }

        public GridColumn<TModel> Format(Func<object, TModel, string> formatter)
        {
            if (this.Type == GridColumnType.Customized)
            {
                throw new Exception("Column current work with Customized mode, plz use BuildTag instead");
            }
            this.Formatter = formatter;
            return this;
        }

        public GridColumn<TModel> BuildTag(Action<TagBuilder, TModel> build)
        {
            if (this.Type == GridColumnType.ModelProperty)
            {
                throw new Exception("Column current binding to model's property, plz use Format instead");
            }
            this.CustomizedTagBuilder = build;
            return this;
        }

    }
}
