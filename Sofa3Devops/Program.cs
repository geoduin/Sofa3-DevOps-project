using System.Runtime.CompilerServices;
using Sofa3Devops.Adapters;
using Sofa3Devops.Adapters.Clients;
using Sofa3Devops.BacklogStates;
using Sofa3Devops.ComponentVisitors.Composites;
using Sofa3Devops.Domain;
using Sofa3Devops.NotificationStrategy;
using Sofa3Devops.Observers;
using Sofa3Devops.SprintReportExporter;
using Sofa3Devops.SprintStates;

namespace Sofa3Devops
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sprint Sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now, "");
            Sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(2), "Export sprint test");
            Member rick = new Developer("Rick Sanchez", "R", "");
            Member alex = new Developer("Alexis Sanchez", "R", "");
            Member hugo = new Developer("Huga Sanchez", "R", "");

            BacklogItem first = new BacklogItem("", "")
            {
                State = new Sofa3Devops.BacklogStates.FinishedState(),
                EffortPoints = 4,
                ResponsibleMember = rick,
            };
            Activity activity = new Activity("", "", first)
            {
                State = new Sofa3Devops.BacklogStates.FinishedState(),
                EffortPoints = 2,
                ResponsibleMember = alex,
            };
            first.AddActivityToBacklogItem(activity);

            BacklogItem second = new BacklogItem("", "")
            {
                State = new TestedState(),
                EffortPoints = 4,
                ResponsibleMember = rick,
            };
            BacklogItem third = new BacklogItem("", "")
            {
                State = new DoingState(),
                EffortPoints = 4,
                ResponsibleMember = rick,
            };
            BacklogItem fourth = new BacklogItem("", "")
            {
                State = new Sofa3Devops.BacklogStates.FinishedState(),
                EffortPoints = 2,
                ResponsibleMember = alex,
            };
            BacklogItem fifth = new BacklogItem("", "")
            {
                State = new Sofa3Devops.BacklogStates.FinishedState(),
                EffortPoints = 1,
                ResponsibleMember = alex,
            };
            BacklogItem sixth = new BacklogItem("", "")
            {
                State = new Sofa3Devops.BacklogStates.FinishedState(),
                EffortPoints = 6,
                ResponsibleMember = hugo,
            };
            Sprint.AssignMembersToSprint(rick);
            Sprint.AssignMembersToSprint(hugo);
            Sprint.AssignMembersToSprint(alex);
            Sprint.AddBacklogItem(first);
            Sprint.AddBacklogItem(second);
            Sprint.AddBacklogItem(third);
            Sprint.AddBacklogItem(fourth);
            Sprint.AddBacklogItem(fifth);
            Sprint.AddBacklogItem(sixth);
            Sprint.Exporter = new PDFExporter();
            Sprint.ExportSprintReport();
        }
    }
}
