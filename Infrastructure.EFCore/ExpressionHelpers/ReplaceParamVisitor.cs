using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.ExpressionHelpers
{
    public class ReplaceParamVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression param;
        private readonly Expression replacement;

        public ReplaceParamVisitor(ParameterExpression param, Expression replacement)
        {
            this.param = param;
            this.replacement = replacement;
        }

        protected override Expression VisitParameter(ParameterExpression node) => node == param ? replacement : node;
    }
}
