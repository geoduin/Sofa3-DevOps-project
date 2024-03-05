﻿using System.Runtime.CompilerServices;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //EmailAdapter handler = new EmailAdapter(new EmailClient());
            //TesterNotificationStrategy test = new TesterNotificationStrategy(handler);
            //Member tester = new Tester("henk","henk@henk.nl", "realHenk");
            //Member productOwner = new ProductOwner("ingrid","ingrid@ingrid.nl", "realIngrid");
            //List<Member> members = new List<Member>();
            //members.Add(tester);
            //members.Add(productOwner);
            //Sprint sprint = new ReleaseSprint(DateTime.Now, DateTime.MaxValue, "henk");
            //sprint.Members = members;
            //sprint.Notification = new SprintCancelStrategy(new EmailAdapter(new EmailClient()));
            //BacklogItem backlogItem = new BacklogItem("test", "test");
            //backlogItem.State = new DoingState();
            //backlogItem.Sprint = sprint;
            //backlogItem.NotificationStrategy = test;
            ////backlogItem.State.SetToReadyTesting(backlogItem);

            //sprint.State = new OngoingState();
            //sprint.State.SetToCanceled(sprint);

            //Member test = new ProductOwner("test", "test", "test");
            Console.WriteLine(typeof(Tester));


        }
    }
}
