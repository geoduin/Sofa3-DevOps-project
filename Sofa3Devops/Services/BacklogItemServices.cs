using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.Services
{
    public class BacklogItemServices
    {
        public static void SetToTesting(Member tester, BacklogItem item)
        {
            if (tester.GetType().Equals(typeof(Tester)) && item.Sprint!.Members.Contains(tester))
            {
                item.State.SetToTesting(item);
            }
            else
            {
                throw new UnauthorizedAccessException(
                    "Only testers that are members of the sprint can set backlog items to testing");
            }
        }

        public static void SetToToDo(Member member, BacklogItem item)
        {
            if (member.GetType().Equals(typeof(Tester)) && item.Sprint!.Members.Contains(member))
            {
                item.State.SetToDo(item);
                return;
            }
            throw new UnauthorizedAccessException(
                "Only Testers that are members of the sprint can set backlog items to to-do");
        }

        public static void SetToTested(Member tester, BacklogItem item)
        {
            if (tester.GetType().Equals(typeof(Tester)) && item.Sprint.Members.Contains(tester))
            {
                item.State.SetToTested(item);
            }
            else
            {
                throw new UnauthorizedAccessException(
                    "Only testers that are members of the sprint can set backlog items to tested");
            }

        }
    }
}
