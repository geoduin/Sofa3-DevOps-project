using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.AuthorisationStrategy
{
    public class LeadDeveloperValidation : IAuthValidationBehavior
    {
        public bool HasPermission(Member member)
        {

            if (member.GetType() == typeof(Developer))
            {
                var developer = (Developer)member;
                if (developer.Seniority)
                {
                    return true;
                }

            }
            throw new UnauthorizedAccessException($"Unauthorized action: Users with {member} role are not allowed to perform this action. Only lead developers are allowed.");
        }
    }
}
