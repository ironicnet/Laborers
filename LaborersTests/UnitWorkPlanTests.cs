using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laborers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    }
}
