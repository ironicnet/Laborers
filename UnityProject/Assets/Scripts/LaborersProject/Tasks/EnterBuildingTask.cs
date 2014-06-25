using Laborers.Behaviors;
using Laborers.Behaviors.Buildings;
using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Tasks
{
    public class EnterBuildingTask : WorkPlanTask
    {
        private Building _targetBuilding;

        public override UnitTaskAnimationType AnimationType
        {
            get { return UnitTaskAnimationType.Walking; }
        }

        public Building TargetBuilding
        {
            get { return _targetBuilding; }
            set { _targetBuilding = value; }
        }
        public EnterBuildingTask(ref Building building)
        {
            TargetBuilding = building;
        }
        public override void Update(Unit unit)
        {
            unit.WorkInBuilding(ref _targetBuilding);
            HasFinished = true;
        }

        public override void Cancel(Unit unit)
        {
        }
    }
}
