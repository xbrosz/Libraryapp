using System.Linq.Expressions;

namespace Infrastructure.EFCore
{
    public class ReplaceParamVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _param;
        private readonly Expression _replacement;

        public ReplaceParamVisitor(ParameterExpression param, Expression replacement)
        {
            _param = param;
            _replacement = replacement;
        }

        protected override Expression VisitParameter(ParameterExpression node) => node == _param ? _replacement : node;
    }
}
