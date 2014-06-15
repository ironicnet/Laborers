using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public class Unit
    {
        protected UnitWorkPlan CurrentWorkPlan;

        public void Update()
        {
            if (CurrentWorkPlan != null)
            {
                CurrentWorkPlan.Update();
            }
        }

        public void CancelWork()
        {
            if (CurrentWorkPlan != null)
            {
                CurrentWorkPlan.Cancel();
            }
        }
        public void AssignWorkPlan(UnitWorkPlan newWorkPlan)
        {
            if (CurrentWorkPlan != null)
            {
                CurrentWorkPlan.Cancel();
            }
            CurrentWorkPlan = newWorkPlan;
            CurrentWorkPlan.SetAssignedTo(this);
        }
    }
}
