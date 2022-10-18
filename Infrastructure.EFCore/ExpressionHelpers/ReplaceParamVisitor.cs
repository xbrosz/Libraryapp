using System.Linq.Expressions;

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
