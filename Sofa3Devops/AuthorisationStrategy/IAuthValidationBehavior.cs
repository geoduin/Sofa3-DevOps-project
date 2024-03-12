using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.AuthorisationStrategy
{
    public interface IAuthValidationBehavior
    {
        public bool HasPermission(Member member);
    }
}
