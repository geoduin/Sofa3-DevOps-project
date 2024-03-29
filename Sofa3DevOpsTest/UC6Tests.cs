﻿using Sofa3Devops.BacklogStates;
using Sofa3Devops.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3DevOpsTest
{
    public class UC6Tests
    {
        private Member dev { get; set; }

        public UC6Tests()
        { 
            dev = new Developer("", "", "");
        }

        [Fact]
        public void TestAssignmentEmptyActivity() {
            Developer developer = new Developer("Olivier", "Olivier@onmicrosoft.net", "OlivierF");
            BacklogItem backlogItem = new BacklogItem("Task1", "");
            Activity activity = new Activity("SubTask1", "Sub", backlogItem);
            Activity activity2 = new Activity("SubTask2", "Sub", backlogItem);

            backlogItem.AddActivityToBacklogItem(activity);
            backlogItem.AddActivityToBacklogItem(activity2);

            // Act
            activity.AssignBacklogItem(developer);

            // Assert
            Assert.IsType<DoingState>(backlogItem.State);
            Assert.IsType<DoingState>(activity?.State);
            Assert.IsType<TodoState>(activity2?.State);

            Assert.Equal("Olivier", activity.ResponsibleMember!.Name);
        }
        
        [Fact]
        public void TestTodoToTodo()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "");

            backlogItem.State.SetToDo(backlogItem, dev);
            Assert.IsType<TodoState>(backlogItem.State);
        }

        [Fact]
        public void TestItemFromTodoToReadyForTesting()
        {
            BacklogItem backlogItem = new BacklogItem("Task1", "");

            var error = Assert.Throws<InvalidOperationException>(()=> backlogItem.SetItemReadyForTesting(dev));

            Assert.Equal("Item or activity must first be moved to Doing, before it can be moved to ready for testing.", error.Message);
        }
    }
}
