using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums.Usres;
using Ambev.DeveloperEvaluation.Domain.Specifications.Bases;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Users;

public class ActiveUserSpecification : Specification<User>
{
    public override bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
