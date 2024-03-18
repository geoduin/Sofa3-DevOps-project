using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using Sofa3Devops.SprintReportExporter;
using Sofa3Devops.SprintStates;
using Sofa3Devops.SprintTemplatePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC16Tests
    {
        private Sprint Sprint {  get; set; }
        private SprintReportTemplate PdfTemplate { get; set; }
        private SprintReportTemplate WordTemplate { get; set; }

        public UC16Tests() {
            Sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(2), "Export sprint test");
            PdfTemplate = new PdfSprintReportTemplate(1, "Sprint report.", "===Scrum/Tooling===");
            WordTemplate = new WordSprintReportTemplate(1, "Sprint report word version", "===Scrum/Tooling===");
        }

        [Fact]
        public void TestExporterNotPresentInSprint()
        {
            var error = Assert.Throws<InvalidOperationException>(()=> Sprint.ExportSprintReport());

            Assert.Equal("An export formatter needs to be first determined, before sprint report can be exported.", error.Message);
        }

        [Fact]
        public void TestExporterToPDF()
        {
            Sprint.Exporter = new PDFExporter(PdfTemplate);
            var (format, result) = Sprint.ExportSprintReport();

            Assert.Equal("PDF", format);
            Assert.True(result);
        }

        [Fact]
        public void TestExporterToWord()
        {
            Sprint.Exporter = new WordExporter(WordTemplate);
            var (format, result) = Sprint.ExportSprintReport();

            Assert.Equal("Microsoft word", format);
            Assert.True(result);
        }

        [Fact]
        public void TestEffortPointsPerDeveloper()
        {
            Member rick = new Developer("Rick Sanchez", "R", "");
            Member alex = new Developer("Alexis Sanchez", "R", "");
            Member hugo = new Developer("Huga Sanchez", "R", "");
            
            BacklogItem first = new BacklogItem("", "") { 
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
            Sprint.AddBacklogItem(first);
            Sprint.AddBacklogItem(second);
            Sprint.AddBacklogItem(third);
            Sprint.AddBacklogItem(fourth);
            Sprint.AddBacklogItem(fifth);
            Sprint.AddBacklogItem(sixth);

            SprintReport sprintReport = new SprintReport(Sprint);

            var result = sprintReport.GetEffortPointsPerDeveloper();

            Assert.Equal(12, result.GetValueOrDefault(rick));
            Assert.Equal(6, result.GetValueOrDefault(hugo));
            Assert.Equal(5, result.GetValueOrDefault(alex));
        }

        [Fact]
        public void TestGenerationBurndownChart()
        {
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
                State = new Sofa3Devops.BacklogStates.FinishedState(),
                EffortPoints = 4,
                ResponsibleMember = rick,
            };
            BacklogItem third = new BacklogItem("", "")
            {
                State = new TestedState(),
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
            Sprint.AddBacklogItem(first);
            Sprint.AddBacklogItem(second);
            Sprint.AddBacklogItem(third);
            Sprint.AddBacklogItem(fourth);
            Sprint.AddBacklogItem(fifth);
            Sprint.AddBacklogItem(sixth);

            SprintReport sprintReport = new SprintReport(Sprint);

            var result = sprintReport.GetBurndownChart();

            Assert.Equal(23, result[0]);
            Assert.Equal(19, result[1]);
            Assert.Equal(19, result[2]);
            Assert.Equal(17, result[3]);
            Assert.Equal(16, result[4]);
            Assert.Equal(10, result[5]);
            Assert.Equal(6, Sprint.BacklogItems.Count);
            Assert.Equal(6, result.Count);
        }
    
    }
}
