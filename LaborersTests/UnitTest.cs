using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laborers;
using Moq;

namespace LaborersTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Unit_WithoutWorkplan_Update()
        {
            Unit unit = new Unit();
            unit.Update();
        }
        [TestMethod]
        public void Unit_CurrentWorkPlan_Update()
        {
            Unit unit = new Unit();
            var taskMock = new Moq.Mock<UnitWorkPlan>();
            taskMock.Setup(t => t.Update()).Verifiable();
            unit.AssignWorkPlan(taskMock.Object);
            unit.Update();

            taskMock.Verify();

        }
        [TestMethod]
        public void Unit_WithoutWorkplan_Cancel()
        {
            Unit unit = new Unit();
            unit.CancelWork();
        }
        [TestMethod]
        public void Unit_CurrentWorkPlan_Cancel()
        {
            Unit unit = new Unit();
            var taskMock = new Moq.Mock<UnitWorkPlan>();
            taskMock.Setup(t => t.Cancel()).Verifiable();
            unit.AssignWorkPlan(taskMock.Object);
            unit.CancelWork();


            taskMock.Verify();
        }
        [TestMethod]
        public void Unit_ReplacingCurrentWorkPlan_Cancel()
        {
            Unit unit = new Unit();
            var workPlanA = new Moq.Mock<UnitWorkPlan>();
            var workPlanB = new Moq.Mock<UnitWorkPlan>();
            workPlanA.Setup(t => t.Cancel()).Verifiable();
            workPlanB.Setup(t => t.Cancel());
            unit.AssignWorkPlan(workPlanA.Object);
            unit.AssignWorkPlan(workPlanB.Object);


            workPlanA.Verify(wp => wp.Cancel(), Times.Once());
            workPlanB.Verify(wp => wp.Cancel(), Times.Never());
        }
    }
}
