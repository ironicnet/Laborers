using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public class Unit
    {
        protected UnitWorkPlan CurrentWorkPlan;
        public float StepSpeed = 1f;
        public Unit()
        {
            Position = new Position();
        }

        public virtual void Update()
        {
            if (CurrentWorkPlan != null)
            {
                CurrentWorkPlan.Update();
            }
        }

        public virtual void CancelWork()
        {
            if (CurrentWorkPlan != null)
            {
                CurrentWorkPlan.Cancel();
            }
        }
        public virtual void AssignWorkPlan(UnitWorkPlan newWorkPlan)
        {
            if (CurrentWorkPlan != null)
            {
                CurrentWorkPlan.Cancel();
            }
            CurrentWorkPlan = newWorkPlan;
            CurrentWorkPlan.SetAssignedTo(this);
        }

        public Position Position { get; set; }
    }
}
