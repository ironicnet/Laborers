using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laborers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Laborers.Behaviors;
using Laborers.Pathfinding;
using Laborers.Behaviors.Units;
namespace Laborers.Tests
{
    [TestClass()]
    public class UnitWorkPlanTests
    {
        [TestMethod()]
        public void EmptyTasksTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();

            Assert.AreEqual(0, workplan.Tasks.Count);

        }
        [TestMethod()]
        public void AddTaskTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            var task = new Mock<WorkPlanTask>();
            workplan.AddTask(task.Object);

            Assert.AreEqual(1, workplan.Tasks.Count);
            Assert.AreEqual(task.Object, workplan.Tasks.First());

        }
        [TestMethod()]
        public void Update_EmptyTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();

            workplan.Update();
            Assert.AreEqual(0, workplan.Tasks.Count);
        }
        [TestMethod()]
        public void Update_OnlyOneTaskTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            var task = new Mock<WorkPlanTask>();
            task.Setup(t => t.Update(It.IsAny<Unit>())).Verifiable();
            workplan.AddTask(task.Object);

            workplan.Update();
            Assert.AreEqual(1, workplan.Tasks.Count);
            task.Verify();
        }
        [TestMethod()]
        public void Update_WhileTaskUnfinishedTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            var task = new Mock<WorkPlanTask>();
            bool finished = false;
            task.Setup(t => t.Update(It.IsAny<Unit>())).Verifiable();
            task.SetupGet(t => t.HasFinished).Returns(() => finished);
            workplan.AddTask(task.Object);
            workplan.Update();
            finished = true;
            workplan.Update();
            task.Verify(t => t.Update(It.IsAny<Unit>()), Times.Once());
            Assert.AreEqual(1, workplan.Tasks.Count);
        }
        [TestMethod()]
        public void Update_MoreThanOneTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            var taskA = new Mock<WorkPlanTask>();
            var taskB = new Mock<WorkPlanTask>();
            bool Afinished = false;
            bool Bfinished = false;
            taskA.Setup(t => t.Update(It.IsAny<Unit>())).Verifiable();
            taskA.SetupGet(t => t.HasFinished).Returns(() => Afinished);

            taskB.Setup(t => t.Update(It.IsAny<Unit>())).Verifiable();
            taskB.SetupGet(t => t.HasFinished).Returns(() => Bfinished);

            workplan.AddTask(taskA.Object);
            workplan.AddTask(taskB.Object);

            workplan.Update();
            Afinished = true;
            workplan.Update();

            taskA.Verify(t => t.Update(It.IsAny<Unit>()), Times.Once());
            taskB.Verify(t => t.Update(It.IsAny<Unit>()), Times.Once());
            Assert.AreEqual(2, workplan.Tasks.Count);
        }
        [TestMethod()]
        public void Update_MoreThanOneWaitUntilFinishTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            var taskA = new Mock<WorkPlanTask>();
            var taskB = new Mock<WorkPlanTask>();
            var taskC = new Mock<WorkPlanTask>();
            bool Afinished = false;
            bool Bfinished = false;
            taskA.Setup(t => t.Update(It.IsAny<Unit>())).Verifiable();
            taskA.SetupGet(t => t.HasFinished).Returns(() => Afinished);

            taskB.Setup(t => t.Update(It.IsAny<Unit>())).Verifiable();
            taskB.SetupGet(t => t.HasFinished).Returns(() => Bfinished);

            taskC.Setup(t => t.Update(It.IsAny<Unit>())).Verifiable();

            workplan.AddTask(taskA.Object);
            workplan.AddTask(taskB.Object);
            workplan.AddTask(taskC.Object);

            workplan.Update();
            workplan.Update();
            workplan.Update();
            Afinished = true;
            workplan.Update();

            taskA.Verify(t => t.Update(It.IsAny<Unit>()), Times.Exactly(3));
            taskB.Verify(t => t.Update(It.IsAny<Unit>()), Times.Once());
            taskC.Verify(t => t.Update(It.IsAny<Unit>()), Times.Never());
            Assert.AreEqual(3, workplan.Tasks.Count);
        }

        [TestMethod()]
        public void Cancel_EmptyTasks()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            workplan.Cancel();

            Assert.AreEqual(0, workplan.Tasks.Count);
        }
        [TestMethod()]
        public void Cancel_OnlyOneTaskTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            var task = new Mock<WorkPlanTask>();
            task.Setup(t => t.Cancel(It.IsAny<Unit>())).Verifiable();
            workplan.AddTask(task.Object);

            workplan.Cancel();
            Assert.AreEqual(1, workplan.Tasks.Count);
            task.Verify();
        }
        [TestMethod()]
        public void Cancel_OnlyIfUnfinishedTest()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            var task = new Mock<WorkPlanTask>();
            bool finished = false;
            task.Setup(t => t.Cancel(It.IsAny<Unit>())).Verifiable();
            task.SetupGet(t => t.HasFinished).Returns(() => finished);
            workplan.AddTask(task.Object);
            workplan.Cancel();
            finished = true;
            workplan.Cancel();
            task.Verify(t => t.Cancel(It.IsAny<Unit>()), Times.Once());
            Assert.AreEqual(1, workplan.Tasks.Count);
        }



        [TestMethod()]
        public void Update_MultipleMoveTasks()
        {
            UnitWorkPlan workplan = new UnitWorkPlan();
            IPathResolver resolver = new OpenPathResolver();
            var taskA = new Tasks.MoveToPositionTask(new Position(5,5,5), resolver);
            var taskB = new Tasks.MoveToPositionTask(new Position(5,0,3), resolver);
            var taskC = new Tasks.MoveToPositionTask(new Position(-2, 1, -1), resolver);
            Unit unit = new Unit();

            workplan.AddTask(taskA);
            workplan.AddTask(taskB);
            workplan.AddTask(taskC);

            unit.AssignWorkPlan(workplan);
            Assert.AreEqual(new Position(0, 0, 0), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(1, 1, 1), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(2, 2, 2), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(3, 3, 3), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(4, 4, 4), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(5, 5, 5), unit.Position); //Arrived
            unit.Update();
            Assert.AreEqual(new Position(5, 5, 5), unit.Position); //Finished

            unit.Update();
            Assert.AreEqual(new Position(5, 4, 4), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(5, 3, 3), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(5, 2, 3), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(5, 1, 3), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(5, 0, 3), unit.Position); //Arrived
            unit.Update();
            Assert.AreEqual(new Position(5, 0, 3), unit.Position); //Finished

            unit.Update();
            Assert.AreEqual(new Position(4, 1, 2), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(3, 1, 1), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(2, 1, 0), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(1, 1, -1), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(0, 1, -1), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(-1, 1, -1), unit.Position);
            unit.Update();
            Assert.AreEqual(new Position(-2, 1, -1), unit.Position); //Arrived
            unit.Update();
            Assert.AreEqual(new Position(-2, 1, -1), unit.Position); //Finished

            Assert.AreEqual(3, workplan.Tasks.Count);
        }
    }
}
