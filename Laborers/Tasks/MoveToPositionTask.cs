using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborers.Tasks
{
    public class MoveToPositionTask : WorkPlanTask
    {
        protected virtual IPathResolver PathResolver { get; set; }
        protected virtual Path Path { get; set; }

        public MoveToPositionTask(Position targetPosition, IPathResolver resolver)
        {
            TargetPosition = targetPosition;
            PathResolver = resolver;
        }
        public override void Update(Unit unit)
        {
            if (Path == null)
            {
                CalculatePath(unit);
            }
        }

        public override void Cancel(Unit unit)
        {
            
        }

        public Position TargetPosition { get; protected set; }

        protected virtual void CalculatePath(Unit unit)
        {
            this.Path = PathResolver.Resolve(unit, TargetPosition, this);
        }

    }
}
