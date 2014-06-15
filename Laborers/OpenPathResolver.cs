using Laborers.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public class OpenPathResolver : IPathResolver
    {
        public Path Resolve(Unit unit, Position targetPosition, Tasks.MoveToPositionTask moveToPositionTask)
        {
            SortedList<int, PathWaypoint> waypoints = new SortedList<int, PathWaypoint>();
            var xTotalDiff = Math.Abs(unit.Position.X - targetPosition.X) / unit.StepSpeed;
            var yTotalDiff = Math.Abs(unit.Position.Y - targetPosition.Y) / unit.StepSpeed;
            var zTotalDiff = Math.Abs(unit.Position.Z - targetPosition.Z) / unit.StepSpeed;
            int maxWaypoints = (int)Math.Max(xTotalDiff, Math.Max(yTotalDiff, zTotalDiff));
            Position lastPosition = null;
            for (var i = 0; i < maxWaypoints; i++)
            {
                PathWaypoint waypoint = new PathWaypoint();

                float x = (lastPosition ?? unit.Position).X;
                float y = (lastPosition ?? unit.Position).Y;
                float z = (lastPosition ?? unit.Position).Z;

                var xDiff = targetPosition.X - x;
                var yDiff = targetPosition.Y - y;
                var zDiff = targetPosition.Z - z;

                var xStep = Math.Min(unit.StepSpeed, Math.Abs(xDiff));
                var yStep = Math.Min(unit.StepSpeed, Math.Abs(yDiff));
                var zStep = Math.Min(unit.StepSpeed, Math.Abs(zDiff));

                if (xDiff != 0) x += (xStep * (xDiff > 0 ? 1 : -1));
                if (yDiff != 0) y += (yStep * (yDiff > 0 ? 1 : -1));
                if (zDiff != 0) z += (zStep * (zDiff > 0 ? 1 : -1));

                waypoint.Position = new Position(x, y, z);
                waypoints.Add(i, waypoint);

                lastPosition = waypoint.Position;
            }
            return new Path(waypoints);
        }
    }
}
