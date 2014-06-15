using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Tasks
{
    public class Path
    {
        protected virtual SortedList<int, PathWaypoint> Waypoints
        {
            get;set;
        }
        public Path()
        {
            Waypoints = new SortedList<int, PathWaypoint>();
        }
    }
}
