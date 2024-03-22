using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using Sofa3Devops.SprintStates;

namespace Sofa3DevOpsTest
{
    public class UC23Tests
    {
        [Fact]
        public void SuccessfullyPostingACommentOnAThreadWillNotifySubscribers()
        {
            Member OP = new Developer("Test", "test@test.nl", "test");
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            item.State = new DoingState();
            AbstractDiscussionComponent thread = new DiscussionThread("Test", "test",item , OP);
            Member commenter = new ProductOwner("'test2", "test2@test.nl", "test2");
            Subscriber sub2 = new RegularSubscriber(new Tester("test3", "test3@test.nl", "test3"));
            thread.AddSubscriber(sub2);
            AbstractDiscussionComponent comment = new DiscussionComment("test", "test", item, commenter);
            var mockedNotificationStrategy = new Mock<INotificationStrategy>();
            thread.NotificationStrategy = mockedNotificationStrategy.Object;
            thread.AddComponent(comment); 
            mockedNotificationStrategy.Verify(m => m.SendNotification(It.IsAny<string>(), It.IsAny<string>(), thread.Subscribers), Times.Once);
        }

        [Fact]
        public void SuccessfullyReplyingToACommentWillNotifySubscribers()
        {
            Member OP = new Developer("Test", "test@test.nl", "test");
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            item.State = new DoingState();
            AbstractDiscussionComponent thread = new DiscussionThread("Test", "test", item, OP);
            Member commenter = new ProductOwner("'test2", "test2@test.nl", "test2");
            Subscriber sub2 = new RegularSubscriber(new Tester("test3", "test3@test.nl", "test3"));
            thread.AddSubscriber(sub2);
            AbstractDiscussionComponent comment = new DiscussionComment("test", "test", item, commenter);
            thread.Children.Add(comment);
            AbstractDiscussionComponent comment2 = new DiscussionComment("test2", "tes2", item, OP);
            var mockedNotificationStrategy = new Mock<INotificationStrategy>();
            comment.NotificationStrategy = mockedNotificationStrategy.Object;
            comment.AddComponent(comment2);
            mockedNotificationStrategy.Verify(m => m.SendNotification(It.IsAny<string>(), It.IsAny<string>(), comment.Subscribers), Times.Once);
        }

        [Fact]
        public void PostingCommentOnThreadWithFinishedSprintWillNotSendException()
        {
            Member OP = new Developer("Test", "test@test.nl", "test");
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            item.Sprint = sprint;
            item.State = new DoingState();
            AbstractDiscussionComponent thread = new DiscussionThread("Test", "test", item, OP);
            Member commenter = new ProductOwner("'test2", "test2@test.nl", "test2");
            Subscriber sub2 = new RegularSubscriber(new Tester("test3", "test3@test.nl", "test3"));
            thread.AddSubscriber(sub2);
            AbstractDiscussionComponent comment = new DiscussionComment("test", "test", item, commenter);
            var mockedNotificationStrategy = new Mock<INotificationStrategy>();
            thread.NotificationStrategy = mockedNotificationStrategy.Object;
            sprint.State = new Sofa3Devops.SprintStates.FinishedState();

            try
            {
                thread.AddComponent(comment);
            }
            catch (Exception e)
            {
                mockedNotificationStrategy.Verify(m => m.SendNotification(It.IsAny<string>(), It.IsAny<string>(), thread.Subscribers), Times.Never);

            }

        }


        [Fact]
        public void ReplyingToCommentOnThreadWithFinishedSprintWillThrowException()
        {
            Member OP = new Developer("Test", "test@test.nl", "test");
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            item.Sprint = sprint;
            item.State = new DoingState();
            AbstractDiscussionComponent thread = new DiscussionThread("Test", "test", item, OP);
            Member commenter = new ProductOwner("'test2", "test2@test.nl", "test2");
            Subscriber sub2 = new RegularSubscriber(new Tester("test3", "test3@test.nl", "test3"));
            thread.AddSubscriber(sub2);
            AbstractDiscussionComponent comment = new DiscussionComment("test", "test", item, commenter);
            thread.Children.Add(comment);
            sprint.State = new Sofa3Devops.SprintStates.FinishedState();
            AbstractDiscussionComponent comment2 = new DiscussionComment("test2", "tes2", item, OP);
            var mockedNotificationStrategy = new Mock<INotificationStrategy>();
            comment.NotificationStrategy = mockedNotificationStrategy.Object;

            try
            {
                thread.AddComponent(comment);
            }
            catch (Exception e)
            {
                mockedNotificationStrategy.Verify(m => m.SendNotification(It.IsAny<string>(), It.IsAny<string>(), comment.Subscribers), Times.Never);

            }
        }

        [Fact]
        public void PosterOfThreadIsSubscribedToThread()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            var tester = new Tester("test", "test", "test");
            AbstractDiscussionComponent thread =
                new DiscussionThread("test", "test", item, tester);

            var result = thread.Subscribers[tester.GetType()];
            Assert.True(result[0].NotifiedUser.Equals(tester));
        }

        [Fact]
        public void PosterOfCommentIsSubscribedToComment()
        {
            BacklogItem item = new BacklogItem("test", "test");
            Sprint sprint = new DevelopmentSprint(DateTime.Now, DateTime.MaxValue, "test");
            sprint.State = new OngoingState();
            item.Sprint = sprint;
            var tester = new Tester("test", "test", "test");
            AbstractDiscussionComponent comment =
                new DiscussionComment("test", "test", item, tester);

            var result = comment.Subscribers[tester.GetType()];
            Assert.True(result[0].NotifiedUser.Equals(tester));
        }
    }
}
