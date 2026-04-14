using System.Linq.Expressions;
using MasterFinder.Domain.Entities;

namespace MasterFinder.Domain.Specifications
{
    public class ExecutorsBySpecializationSpecification : ISpecification<Executor>
    {
        private readonly string _specialization;

        public ExecutorsBySpecializationSpecification(string specialization)
        {
            _specialization = specialization;
        }

        public Expression<Func<Executor, bool>> Criteria =>
            e => e.Specialization.Value.Contains(_specialization);
        public List<Expression<Func<Executor, object>>> Includes => new();
        public Expression<Func<Executor, object>>? OrderBy => e => e.UserName.Value;
        public Expression<Func<Executor, object>>? OrderByDescending => null;
        public int Take => 0;
        public int Skip => 0;
        public bool IsPagingEnabled => false;
    }
}