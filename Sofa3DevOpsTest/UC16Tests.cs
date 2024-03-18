using Sofa3Devops.Domain;
using Sofa3Devops.SprintReportExporter;
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

        public UC16Tests() {
            Sprint = new DevelopmentSprint(DateTime.Now, DateTime.Now.AddDays(2), "Export sprint test");
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
            Sprint.Exporter = new PDFExporter();
            var (format, result) = Sprint.ExportSprintReport();

            Assert.Equal("PDF", format);
            Assert.True(result);
        }

        [Fact]
        public void TestExporterToWord()
        {
            Sprint.Exporter = new WordExporter();
            var (format, result) = Sprint.ExportSprintReport();

            Assert.Equal("Microsoft word", format);
            Assert.True(result);
        }
    }
}
