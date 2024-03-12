using Sofa3Devops.AuthorisationStrategy;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3Devops.BacklogStates
{
    public class ReadyToTestingState : IBacklogState
    {
        private IAuthValidationBehavior? validator { get; set; }

        public void SetDoing(BacklogItem item, Member member)
        {
            throw new NotImplementedException();
        }

        public void SetToDo(BacklogItem item, Member member)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);

            item.Sprint!.SetNotificationStrategy(new ScrumMasterNotificationStrategy());
            
            item.SetBacklogState(new TodoState());
            item.Activities.ForEach(a => a.SetToTodo(member));
            item.NotifyAll($"Backlog-item: {item.Name} has been rejected for testing.", $"This backlog-item is rejected by our testers. The item is back to {item.State.GetType().Name}");
        }

        public void SetToFinished(BacklogItem item, Member member)
        {
            throw new NotImplementedException();
        }

        public void SetToReadyTesting(BacklogItem item, Member member)
        {
            throw new NotImplementedException();
        }

        public void SetToTested(BacklogItem item, Member member)
        {
            throw new InvalidOperationException();
        }

        public void SetToTesting(BacklogItem item, Member member)
        {
            validator = new TesterValidation();
            validator.HasPermission(member);
            item.SetBacklogState(new TestingState());
        }
    }
}
