using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomainServicesIntf
{
    public interface ISprintManager
    {
        void StartSprint(Sprint sprint, Member member);
        void CancelSprint(Sprint sprint, Member member);
        Sprint CreateSprint(DateTime start, DateTime end, string name, Member member);
        Sprint AddBacklogItem(Sprint sprint, BacklogItem backlogItem, Member member);
        void FinishSprint(Sprint sprint, Member member);
    }
}
