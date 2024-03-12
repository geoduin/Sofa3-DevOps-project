using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.AuthorisationStrategy
{
    public class ScrumMasterPOValidation : IAuthValidationBehavior
    {
        private readonly List<Type> auth;

        public ScrumMasterPOValidation()
        {
            auth = new List<Type>()
            {
                typeof(ScrumMaster),
                typeof(ProductOwner)
            };
        }

        public bool HasPermission(Member member)
        {
            if (auth.Contains(member.GetType()))
            {
                return true;
            }
            throw new UnauthorizedAccessException($"Unauthorized action: Users with {member} role are not allowed to set item to testing. Only testers are allowed to move backlog-item to Testing.");
        }
    }
}
