using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Pathfinding
{
    public class Path
    {
        protected virtual SortedList<int, PathWaypoint> Waypoints
        {
            get;set;
        }
        public virtual KeyValuePair<int, PathWaypoint> CurrentWaypoint
        {
            get;
            protected set;
        }
        public virtual KeyValuePair<int, PathWaypoint> LastWaypoint
        {
            get
            {
                return Waypoints.Last();
            }
        }
        public Path(SortedList<int, PathWaypoint> waypoints)
        {
            Waypoints = waypoints;
            CurrentWaypoint = Waypoints.First();
        }

        public void Next()
        {
            CurrentWaypoint = Waypoints.First(w => w.Key == (CurrentWaypoint.Key+1));
        }
    }
}
