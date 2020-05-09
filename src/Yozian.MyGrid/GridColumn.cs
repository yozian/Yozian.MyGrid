using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Yozian.MyGrid
{
    public class GridColumn<TModel>
    {
        internal GridColumnType Type { get; private set; }

        internal string ColumnName { get; set; }

        internal string ColumnValue { get; set; }

        internal string PropertyName { get; set; }

        internal Func<object, TModel, string> Formatter { get; private set; }

        internal Action<TagBuilder, TModel> CustomizedTagBuilder { get; set; }

        internal object ThAtrributes { get; set; }

        internal object TdAtrributes { get; set; }


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

        public GridColumn<TModel> ThAttributes(object attributes)
        {
            this.ThAtrributes = attributes;
            return this;
        }


        public GridColumn<TModel> TdAttributes(object attributes)
        {
            this.TdAtrributes = attributes;
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
