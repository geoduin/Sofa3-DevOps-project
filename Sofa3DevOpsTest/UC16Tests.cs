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

        [Fact]
        public void TestWordFormatReturner()
        {
            Member rick = new Developer("Rick Sanchez", "R", "");
            Member alex = new Developer("Alexis Sanchez", "R", "");
            Member hugo = new Developer("Huga Sanchez", "R", "");

            var exporter = new WordExporter(WordTemplate);
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

            var result = WordTemplate.BuildReport(sprintReport);
            // Assert
            Assert.Equal($"===Scrum/Tooling===\n<p> Name: Rick Sanchez, Effort points: 12</p><p> Name: Alexis Sanchez, Effort points: 5</p><p> Name: Huga Sanchez, Effort points: 6</p><p>0. 23 </p><p>1. 19 </p><p>2. 19 </p><p>3. 17 </p><p>4. 16 </p><p>5. 10 </p>===Scrum/Tooling=== - Version 1 - {DateTime.Now.Year}\n", result);
        }

        [Fact]
        public void TestPDFFormatReturner()
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

            var result = PdfTemplate.BuildReport(sprintReport);
            // Assert
            Assert.Equal($"===Scrum/Tooling===\nName: Rick Sanchez, Effort points: 12\nName: Alexis Sanchez, Effort points: 5\nName: Huga Sanchez, Effort points: 6\n0. 23\n1. 19\n2. 19\n3. 17\n4. 16\n5. 10\n===Scrum/Tooling=== - Version 1 - 2024\n", result);
        }
    }
}
