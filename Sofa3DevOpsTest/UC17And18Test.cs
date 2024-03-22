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
            AbstractDiscussionComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            AbstractDiscussionComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            thread.AddComponent(reply);

            Assert.Equal(thread.Children[0], reply);
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
            AbstractDiscussionComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            sprint.State = new FinishedState();
            AbstractDiscussionComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));


            Assert.Throws<InvalidOperationException>(() => thread.AddComponent(reply));
        }

        [Fact]
        public void CreatingThreadWillAddThreadToBacklogItem()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            var thread = new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            Assert.Same(thread, item.Threads[0]);
        }

        [Fact]
        public void CreatingThreadWillAddThreadToPostingMember()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            var poster = new Tester("test", "test", "test");
            var thread = new DiscussionThread("test", "test", item, poster);
            Assert.Same(thread, poster.PostedDiscussionForumComponents[0]);
        }

        [Fact]
        public void CommentingOnThreadWillAddCommentToPostingMember()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            AbstractDiscussionComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            var replyMember = new Tester("test", "test", "test");
            AbstractDiscussionComponent reply =
                new DiscussionComment("test", "test", item, replyMember);
            thread.AddComponent(reply);

            Assert.Equal(reply, replyMember.PostedDiscussionForumComponents[0]);
        }

        [Fact]
        public void AddingAThreadToAThreadThrowsException()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            AbstractDiscussionComponent thread1 =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            AbstractDiscussionComponent thread2 =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            Assert.Throws<InvalidOperationException>(() => thread1.AddComponent(thread2));
        }

        [Fact]
        public void CanCommentToCommentIfSprintIsNotFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            AbstractDiscussionComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            AbstractDiscussionComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            AbstractDiscussionComponent reply2 =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            reply.AddComponent(reply2);

            Assert.Equal(reply.Children[0], reply2);
        }

        [Fact]
        public void CanNotCommentToCommentIfSprintIsFinished()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            AbstractDiscussionComponent thread =
                new DiscussionThread("test", "test", item, new Tester("test", "test", "test"));
            AbstractDiscussionComponent reply =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));
            thread.AddComponent(reply);
            sprint.State = new FinishedState();

            AbstractDiscussionComponent reply2 =
                new DiscussionComment("test", "test", item, new Tester("test", "test", "test"));

            Assert.Throws<InvalidOperationException>(() => reply.AddComponent(reply2));
        }
    }
}
