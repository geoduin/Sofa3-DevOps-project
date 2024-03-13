using Sofa3Devops.Domain;


namespace Sofa3Devops.AuthorisationStrategy
{
    public interface IAuthValidationBehavior
    {
        public bool HasPermission(Member member);
    }
}
