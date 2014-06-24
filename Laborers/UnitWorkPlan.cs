using Laborers.Behaviors;
using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public class UnitWorkPlan
    {
        public List<WorkPlanTask> Tasks;
        protected WorkPlanTask CurrentTask;
        protected Unit AssignedTo;

        public UnitWorkPlan()
        {
            Tasks = new List<WorkPlanTask>();
        }

        public virtual void SetAssignedTo(Unit unit)
        {
            AssignedTo = unit;
        }

        public virtual void Update()
        {
            if (Tasks.Count > 0)
            {
                if (CurrentTask == null || CurrentTask.HasFinished) CurrentTask = GetNextTask();

                if (CurrentTask!=null)
                {
                    CurrentTask.Update(AssignedTo);
                }
            }
        }

        private WorkPlanTask GetNextTask()
        {
            return Tasks.FirstOrDefault(t=>!t.HasFinished);
        }

        public void AddTask(WorkPlanTask task)
        {
            Tasks.Add(task);

            if (CurrentTask == null)
                CurrentTask = task;
        }

        public virtual void Cancel()
        {
            if (CurrentTask != null && !CurrentTask.HasFinished)
            {
                CurrentTask.Cancel(AssignedTo);
            }
        }
    }
}
