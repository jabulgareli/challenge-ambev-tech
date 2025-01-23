namespace Ambev.DeveloperEvaluation.Domain.Specifications.Bases
{
    public class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _first;
        private readonly ISpecification<T> _second;

        public OrSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _first.IsSatisfiedBy(entity) || _second.IsSatisfiedBy(entity);
        }
    }
}
