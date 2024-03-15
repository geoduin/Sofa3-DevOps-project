using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sofa3Devops.Domain;
using Sofa3Devops.SprintStates;

namespace Sofa3DevOpsTest
{
    public class UC17And18Test
    {
        [Fact]
        public void CanCreateThreadIfSprintIsNotFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            var exception = Record.Exception(() => new DiscussionThread("test", "test", item, new Tester("test", "test", "test")));
            Assert.Null(exception);
        }
        [Fact]
        public void CanCommentOnThreadIfSprintIsNotFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            DiscussionForumComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            DiscussionForumComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            thread.AddComponent(reply);

            Assert.Equal(thread.GetChild(0), reply);
        }

        [Fact]
        public void CanCommentToCommentIfSprintIsNotFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            DiscussionForumComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            DiscussionForumComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            DiscussionForumComponent reply2 =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            reply.AddComponent(reply2);

            Assert.Equal(reply.GetChild(0), reply2);
        }

        [Fact]
        public void CanNotCreateThreadIfSprintIsFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new FinishedState();
            item.Sprint = sprint;
            Assert.Throws<InvalidOperationException>(() =>
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test")));
        }

        [Fact]
        public void CanNotCommentOnThreadIfSprintIsFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            DiscussionForumComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            sprint.State = new FinishedState();
            DiscussionForumComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));


            Assert.Throws<InvalidOperationException>(() => thread.AddComponent(reply));
        }

        [Fact]
        public void CanNotCommentToCommentIfSprintIsFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            DiscussionForumComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            DiscussionForumComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            thread.AddComponent(reply);
            sprint.State = new FinishedState();

            DiscussionForumComponent reply2 =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));

            Assert.Throws<InvalidOperationException>(() => reply.AddComponent(reply2));
        }
    }
}
