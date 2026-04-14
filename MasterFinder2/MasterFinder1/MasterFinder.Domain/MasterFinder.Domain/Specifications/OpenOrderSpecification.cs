using System.Linq.Expressions;
using MasterFinder.Domain.Entities;
using MasterFinder.Domain.Enums;

namespace MasterFinder.Domain.Specifications
{
    public class OpenOrdersSpecification : ISpecification<Order>
    {
        public Expression<Func<Order, bool>> Criteria => o => o.Status == OrderStatus.Open;
        public List<Expression<Func<Order, object>>> Includes => new();
        public Expression<Func<Order, object>>? OrderBy => o => o.CreatedAt;
        public Expression<Func<Order, object>>? OrderByDescending => null;
        public int Take => 0;
        public int Skip => 0;
        public bool IsPagingEnabled => false;
    }
}