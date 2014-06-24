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
using Laborers.Behaviors;
using Laborers.Pathfinding;

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
            resolver.Setup(r => r.Resolve(It.IsAny<Unit>(), It.IsAny<Position>(), It.IsAny<MoveToPositionTask>()))
                    .Returns<Unit, Position, MoveToPositionTask>((u, p, t) =>
                    {
                        SortedList<int, PathWaypoint> list = new SortedList<int, PathWaypoint>();
                        list.Add(0, new PathWaypoint() { Position = u.Position });
                        return new Path(list);
                    });
            
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.CallBase = true;

            task.Object.Update(new Unit());

            resolver.Verify();
        }


        [TestMethod()]
        public void Instance_Update()
        {
            Position targetPosition = new Position();
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();
            resolver.Setup(r=>r.Resolve(It.IsAny<Unit>(), It.IsAny<Position>(), It.IsAny<MoveToPositionTask>()));

            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.Protected().Setup<bool>("ArrivedToDestination",ItExpr.IsAny<Unit>()).Returns(true);
            task.CallBase = true;

            task.Object.Update(unit);
        }
        [TestMethod()]
        public void Instance_UpdateWithPath()
        {
            Position targetPosition = new Position(1, 0, 0);
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();
            

            
            var waypoints = new SortedList<int, PathWaypoint>();
            waypoints.Add(0, new PathWaypoint() { Position = new Position(0, 0, 0) });
            waypoints.Add(1, new PathWaypoint() { Position = targetPosition });

            var path = new Path(waypoints);
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.Protected().SetupGet<IPathResolver>("PathResolver").Returns(resolver.Object);
            resolver.Setup(r => r.Resolve(It.IsAny<Unit>(), It.IsAny<Position>(), It.Is<MoveToPositionTask>(t=>t==task.Object))).Returns(path);
            
            task.CallBase = true;
            //This should calculate the path
            task.Object.Update(unit);
            //This doesn't
            task.Object.Update(unit);

            task.Protected().Verify("CalculatePath", Times.Once(), unit);

            resolver.Verify();
            task.Verify();
        }
        [TestMethod()]
        public void Instance_UpdatePositionForwardWithPath()
        {
            Position targetPosition = new Position(3, 0, 0);
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();



            var waypoints = new SortedList<int, PathWaypoint>();
            waypoints.Add(0, new PathWaypoint() { Position = new Position(0, 0, 0) });
            waypoints.Add(1, new PathWaypoint() { Position = new Position(1, 0, 0) });
            waypoints.Add(2, new PathWaypoint() { Position = new Position(2, 0, 0) });
            waypoints.Add(3, new PathWaypoint() { Position = new Position(3, 0, 0) });

            var path = new Path(waypoints);
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.Protected().SetupGet<IPathResolver>("PathResolver").Returns(resolver.Object);
            resolver.Setup(r => r.Resolve(It.Is<Unit>(u => u == unit), It.IsAny<Position>(), It.Is<MoveToPositionTask>(t => t == task.Object))).Returns(path);

            task.CallBase = true;
            //This should move towards the waypoint
            task.Object.Update(unit);
            //This should keep moving towards the waypoint
            task.Object.Update(unit);
            //This should keep moving towards the waypoint
            task.Object.Update(unit);

            //This should have reached waypoint and set it to finished
            task.Object.Update(unit);
            //This shouldn't do anything
            task.Object.Update(unit);

            task.Protected().Verify("CalculatePath", Times.Once(), unit);
            task.Protected().VerifySet<bool>("HasFinished", Times.Once(), false);
            task.Protected().VerifySet<bool>("HasFinished", Times.Once(), true);
            Assert.AreEqual(targetPosition.X, unit.Position.X);

            

            resolver.Verify();
            task.Verify();
        }

        [TestMethod()]
        public void Instance_UpdatePositionBackwardWithPath()
        {
            Position targetPosition = new Position(-3, 0, 0);
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();



            var waypoints = new SortedList<int, PathWaypoint>();
            waypoints.Add(0, new PathWaypoint() { Position = new Position(0, 0, 0) });
            waypoints.Add(1, new PathWaypoint() { Position = new Position(-1, 0, 0) });
            waypoints.Add(2, new PathWaypoint() { Position = new Position(-2, 0, 0) });
            waypoints.Add(3, new PathWaypoint() { Position = new Position(-3, 0, 0) });

            var path = new Path(waypoints);
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.Protected().SetupGet<IPathResolver>("PathResolver").Returns(resolver.Object);
            resolver.Setup(r => r.Resolve(It.Is<Unit>(u => u == unit), It.IsAny<Position>(), It.Is<MoveToPositionTask>(t => t == task.Object))).Returns(path);

            task.CallBase = true;
            //This should move towards the waypoint
            task.Object.Update(unit);
            //This should keep moving towards the waypoint
            task.Object.Update(unit);
            //This should keep moving towards the waypoint
            task.Object.Update(unit);

            //This should have reached waypoint and set it to finished
            task.Object.Update(unit);
            //This shouldn't do anything
            task.Object.Update(unit);

            task.Protected().Verify("CalculatePath", Times.Once(), unit);
            task.Protected().VerifySet<bool>("HasFinished", Times.Once(), false);
            task.Protected().VerifySet<bool>("HasFinished", Times.Once(), true);
            Assert.AreEqual(targetPosition.X, unit.Position.X);



            resolver.Verify();
            task.Verify();
        }
        [TestMethod()]
        public void Instance_UpdatePositionBackwardWithPathMultiple()
        {
            Position targetPosition = new Position(-2, 1, 2);
            var unit = new Unit();
            var resolver = new Mock<IPathResolver>();



            var waypoints = new SortedList<int, PathWaypoint>();
            PathWaypoint waypoint0 =new PathWaypoint() { Position = new Position(0, 0, 0) };
            PathWaypoint waypoint1 =new PathWaypoint() { Position = new Position(-1, 1, 1) };
            PathWaypoint waypoint2 = new PathWaypoint() { Position = new Position(-2, 1, 2) };

            waypoints.Add(0, waypoint0);
            waypoints.Add(1, waypoint1);
            waypoints.Add(2, waypoint2);

            var path = new Path(waypoints);
            var task = new Mock<MoveToPositionTask>(targetPosition, resolver.Object);
            task.Protected().SetupGet<IPathResolver>("PathResolver").Returns(resolver.Object);
            resolver.Setup(r => r.Resolve(It.Is<Unit>(u => u == unit), It.IsAny<Position>(), It.Is<MoveToPositionTask>(t => t == task.Object))).Returns(path);

            task.CallBase = true;
            //This should move towards the waypoint
            task.Object.Update(unit);
            Assert.AreEqual(waypoint0.Position.X, unit.Position.X);
            Assert.AreEqual(waypoint0.Position.Y, unit.Position.Y);
            Assert.AreEqual(waypoint0.Position.Z, unit.Position.Z);
            //This should keep moving towards the waypoint
            task.Object.Update(unit);
            Assert.AreEqual(waypoint1.Position.X, unit.Position.X);
            Assert.AreEqual(waypoint1.Position.Y, unit.Position.Y);
            Assert.AreEqual(waypoint1.Position.Z, unit.Position.Z);
            //This should keep moving towards the waypoint
            task.Object.Update(unit);
            Assert.AreEqual(waypoint2.Position.X, unit.Position.X);
            Assert.AreEqual(waypoint2.Position.Y, unit.Position.Y);
            Assert.AreEqual(waypoint2.Position.Z, unit.Position.Z);

            //This should have reached waypoint and set it to finished
            task.Object.Update(unit);
            //This shouldn't do anything
            task.Object.Update(unit);

            task.Protected().Verify("CalculatePath", Times.Once(), unit);
            task.Protected().VerifySet<bool>("HasFinished", Times.Once(), false);
            task.Protected().VerifySet<bool>("HasFinished", Times.Once(), true);
            Assert.AreEqual(targetPosition.X, unit.Position.X);
            Assert.AreEqual(targetPosition.Y, unit.Position.Y);
            Assert.AreEqual(targetPosition.Z, unit.Position.Z);

            resolver.Verify();
            task.Verify();
        }
    }
}
