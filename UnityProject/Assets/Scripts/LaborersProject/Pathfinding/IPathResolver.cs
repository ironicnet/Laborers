using Laborers.Behaviors;
using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Pathfinding
{
    public interface IPathResolver
    {
        Path Resolve(Unit unit, Position TargetPosition, Tasks.MoveToPositionTask moveToPositionTask);
    }
}
