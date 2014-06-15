using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public abstract class WorkPlanTask
    {
        public abstract void Update(Unit unit);
        public abstract void Cancel(Unit unit);

        public virtual bool HasFinished { get; protected set; }
    }
}
