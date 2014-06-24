using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborers.Tasks
{
    public class BuildBuildingTask : WorkPlanTask
    {
        public override UnitTaskAnimationType AnimationType
        {
            get { return UnitTaskAnimationType.Building; }
        }
        private bool _leaveBuildingOnFinish = true;

        public bool LeaveBuildingOnFinish
        {
            get { return _leaveBuildingOnFinish; }
            set { _leaveBuildingOnFinish = value; }
        }

        public override void Update(Unit unit)
        {
            if (unit.WorkingBuilding.IsBuilt)
            {
                HasFinished = true;
                if (LeaveBuildingOnFinish)
                    unit.LeaveBuilding();
            }
        }

        public override void Cancel(Unit unit)
        {
            unit.LeaveBuilding();
        }
    }
}
