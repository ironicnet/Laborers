using Laborers.Behaviors;
using Laborers.Behaviors.Units;
using Laborers.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Tasks
{
    public class MoveToPositionTask : WorkPlanTask
    {
        public override UnitTaskAnimationType AnimationType
        {
            get { return UnitTaskAnimationType.Walking; }
        }
        protected virtual Laborers.Pathfinding.Path Path { get; set; }

        public MoveToPositionTask(Position targetPosition)
        {
            TargetPosition = targetPosition;
        }
        public override void Update(Unit unit)
        {
            if (!HasFinished)
            {
                if (Path == null)
                {
                    CalculatePath(unit);
                }
                if (ArrivedToDestination(unit))
                {
                    HasFinished = true;
                    Finish(unit);
                }
                else
                {                    
                    MoveTowards(unit, Path.CurrentWaypoint.Value.Position);
                    if (ArrivedTo(unit.Position, Path.CurrentWaypoint.Value.Position) && Path.CurrentWaypoint.Key< Path.LastWaypoint.Key)
                    {

                        Path.Next();
                    }
                }
            }
        }

        protected virtual void Finish(Unit unit)
        {
        }

        private void MoveTowards(Unit unit, Position targetPosition)
        {
            float x = unit.Position.X;
            float y = unit.Position.Y;
            float z = unit.Position.Z;

            var xDiff = targetPosition.X - unit.Position.X;
            var yDiff = targetPosition.Y - unit.Position.Y;
            var zDiff = targetPosition.Z - unit.Position.Z;

            var xStep = Math.Min(unit.StepSpeed, Math.Abs(xDiff));
            var yStep = Math.Min(unit.StepSpeed, Math.Abs(yDiff));
            var zStep = Math.Min(unit.StepSpeed, Math.Abs(zDiff));

            if (xDiff != 0) x += (xStep * (xDiff > 0 ? 1 : -1));
            if (yDiff != 0) y += (yStep * (yDiff > 0 ? 1 : -1));
            if (zDiff != 0) z += (zStep * (zDiff > 0 ? 1 : -1));

            unit.Position = new Position(x, y, z);
        }

        protected virtual bool ArrivedToDestination(Unit unit)
        {
            return ArrivedTo(unit.Position, Path.LastWaypoint.Value.Position);
        }
        protected virtual bool ArrivedTo(Position unitPosition, Position position)
        {
            return (unitPosition.Equals(position));
        }

        public override void Cancel(Unit unit)
        {
            
        }

        public Position TargetPosition { get; protected set; }

        protected virtual void CalculatePath(Unit unit)
        {
            this.Path = GameManager.PathResolver.Resolve(unit, TargetPosition, this);
        }

    }
}
