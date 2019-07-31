using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;

namespace Yozian.MyGrid
{
    public class Grid<TModel>
    {
        private string id;

        private string cssClass;

        private GridColumnBinder<TModel> columnBinder = new GridColumnBinder<TModel>();

        private IEnumerable<TModel> source;

        private object htmlAttributes;


        internal Grid(IEnumerable<TModel> source)
        {
            this.source = source;
        }

        public Grid<TModel> Id(string id)
        {
            this.id = id;
            return this;
        }

        public Grid<TModel> CssClass(string cssClass)
        {
            this.cssClass = cssClass;
            return this;
        }

        public Grid<TModel> DefineColumns(Action<GridColumnBinder<TModel>> binder)
        {
            binder(this.columnBinder);

            return this;
        }


        public Grid<TModel> HtmlAttributes(object htmlAttributes)
        {
            this.htmlAttributes = htmlAttributes;
            var type = typeof(TModel);

            return this;
        }

        public HtmlString Render()
        {
            if (this.source == null)
            {
                throw new Exception("Grid Soruce is NOT provided!");
            }
            var table = createTable(createThead(), createTbody());

            var writer = new StringWriter();
            table.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }


        private TagBuilder createThead()
        {
            var thead = new TagBuilder("thead");
            var tr = new TagBuilder("tr");
            var gridColumns = this.columnBinder.GetGridColumns();

            foreach (var column in gridColumns)
            {
                //thead
                var th = new TagBuilder("th");

                th.AddCssClass(column.IsColumnInDetail ? "none" : "all");
                th.InnerHtml.SetContent(column.ColumnName);
                tr.InnerHtml.AppendHtml(th);
            }

            thead.InnerHtml.AppendHtml(tr);

            return thead;
        }


        private TagBuilder createTbody()
        {
            var tbody = new TagBuilder("tbody");
            var gridColumns = this.columnBinder.GetGridColumns();
            var props = TypeDescriptor.GetProperties(this.source.FirstOrDefault());

            foreach (var model in this.source)
            {
                var tr = new TagBuilder("tr");
                foreach (var column in gridColumns)
                {
                    var td = new TagBuilder("td");
                    if (column.Type == GridColumnType.ModelProperty)
                    {
                        var prop = props[column.PropertyName];
                        var value = prop.GetValue(model);
                        column.ColumnValue = column.Formatter(value, model);
                        td.InnerHtml.SetContent(column.ColumnValue);
                    }
                    else
                    {
                        column.CustomizedTagBuilder(td, model);
                    }
                    tr.InnerHtml.AppendHtml(td);
                }
                tbody.InnerHtml.AppendHtml(tr);
            }
            return tbody;
        }

        private TagBuilder createTable(TagBuilder thead, TagBuilder thbody)
        {
            var table = new TagBuilder("table");

            table.InnerHtml.AppendHtml(thead);

            table.InnerHtml.AppendHtml(thbody);

            table.Attributes.Add("id", this.id);
            table.AddCssClass(this.cssClass ?? string.Empty);

            table.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(this.htmlAttributes));

            return table;
        }
    }
}
