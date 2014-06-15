using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laborers.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laborers;
using Moq;
using Moq.Protected;

namespace Laborers.Tasks.Tests
{
    [TestClass()]
    public class MoveToPositionTests
    {
        [TestMethod()]
        public void Instance()
        {
            Position targetPosition = new Position();
            var resolver = new Mock<IPathResolver>();
            MoveToPositionTask task = new MoveToPositionTask(targetPosition, resolver.Object);


            Assert.IsNotNull(task);
            Assert.IsInstanceOfType(task, typeof(WorkPlanTask));
        }
        [TestMethod()]
        public void Instance_TargetPosition()
        {
            Position targetPosition = new Position();
            var resolver = new Mock<IPathResolver>();
            MoveToPositionTask task = new MoveToPositionTask(targetPosition, resolver.Object);



            Assert.AreEqual(targetPosition, task.TargetPosition);
        }
        [TestMethod()]
        public void Instance_CalculatePath()
        {
            Position targetPosition = new Position();
            var resolver = new Mock<IPathResolver>();
            
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            var pathSet = false;
            task.Protected().Setup("CalculatePath", ItExpr.IsAny<Unit>()).Callback(()=> pathSet = true);
            task.CallBase = true;

            task.Object.Update(new Unit());

            Assert.AreEqual(true, pathSet);
            task.Verify();
        }


        [TestMethod()]
        public void Instance_Update()
        {
            Position targetPosition = new Position();
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();

            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            var pathSet = false;
            task.CallBase = true;

            task.Object.Update(unit);
        }
        [TestMethod()]
        public void Instance_UpdateWithPath()
        {
            Position targetPosition = new Position();
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();
            

            
            var waypoints = new SortedList<int, PathWaypoint>();
            waypoints.Add(0, new PathWaypoint());
            waypoints.Add(1, new PathWaypoint());
            waypoints.Add(2, new PathWaypoint());

            var path = new Mock<Path>();
            path.Protected().SetupGet<SortedList<int, PathWaypoint>>("Waypoints").Returns(waypoints);
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.Protected().SetupGet<IPathResolver>("PathResolver").Returns(resolver.Object);
            resolver.Setup(r => r.Resolve(It.Is<Unit>(u=>u==unit), It.IsAny<Position>(), It.Is<MoveToPositionTask>(t=>t==task.Object))).Returns(path.Object);
            
            task.CallBase = true;
            //This should calculate the path
            task.Object.Update(unit);
            //This doesn't
            task.Object.Update(unit);

            task.Protected().Verify("CalculatePath", Times.Once(), unit);

            resolver.Verify();
            task.Verify();
            path.Verify();
        }
        [TestMethod()]
        public void Instance_UpdatePositionWithPath()
        {
            Position targetPosition = new Position();
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();



            var waypoints = new SortedList<int, PathWaypoint>();
            waypoints.Add(0, new PathWaypoint());

            var path = new Mock<Path>();
            path.Protected().SetupGet<SortedList<int, PathWaypoint>>("Waypoints").Returns(waypoints);
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.Protected().SetupGet<IPathResolver>("PathResolver").Returns(resolver.Object);
            resolver.Setup(r => r.Resolve(It.Is<Unit>(u => u == unit), It.IsAny<Position>(), It.Is<MoveToPositionTask>(t => t == task.Object))).Returns(path.Object);

            task.CallBase = true;
            //This should move towards the waypoint
            task.Object.Update(unit);
            //This should keep moving towards the waypoint
            task.Object.Update(unit);
            //This should have reached waypoint

            task.Protected().Verify("CalculatePath", Times.Once(), unit);
            task.VerifySet((t) => t.HasFinished, Times.Once);

            

            resolver.Verify();
            task.Verify();
            path.Verify();
        }
    }
}
