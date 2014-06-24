using Laborers.Behaviors;
using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborers.Tasks
{
    public class LeaveBuildingTask : WorkPlanTask
    {
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
