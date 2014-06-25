using Laborers.Behaviors;
using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Tasks
{
    public class LeaveBuildingTask : WorkPlanTask
    {

        public override UnitTaskAnimationType AnimationType
        {
            get { return UnitTaskAnimationType.Walking; }
        }
        public LeaveBuildingTask()
        {
        }
        public override void Update(Unit unit)
        {
            unit.LeaveBuilding();
            HasFinished = true;
        }

        public override void Cancel(Unit unit)
        {
        }
    }
}
