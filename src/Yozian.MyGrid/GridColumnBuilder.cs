using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Yozian.MyGrid
{
    public class GridColumnBuilder<TModel>
    {
        private List<GridColumn<TModel>> columns = new List<GridColumn<TModel>>();

        internal IEnumerable<GridColumn<TModel>> GetGridColumns()
        {
            return this.columns;
        }

        internal GridColumnBuilder() { }

        public GridColumn<TModel> ColumnFor(Expression<Func<TModel, object>> expression)
        {
            MemberInfo member;
            Expression bodyExpression = expression.Body;


            if (bodyExpression.NodeType.Equals(ExpressionType.Convert)
                && bodyExpression is UnaryExpression)
            {
                Expression operand = ((UnaryExpression)expression.Body).Operand;
                member = ((MemberExpression)operand).Member;
            }
            else
            {
                member = ((MemberExpression)bodyExpression).Member;
            }

            // try to get display namq of property
            var attribute = member.GetCustomAttribute<DisplayAttribute>(false);

            var column = new GridColumn<TModel>(GridColumnType.ModelProperty)
            {
                ColumnName = attribute?.Name ?? member.Name, // default to property name
                PropertyName = member.Name,
            };

            columns.Add(column);
            return column;
        }

        public GridColumn<TModel> ColumnFor(string name, Action<TagBuilder, TModel> builder)
        {
            var column = new GridColumn<TModel>(GridColumnType.Customized)
            {
                ColumnName = name, // default to property name
                CustomizedTagBuilder = builder
            };
            columns.Add(column);
            return column;
        }

    }
}
