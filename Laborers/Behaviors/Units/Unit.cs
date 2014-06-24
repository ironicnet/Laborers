using Laborers.Behaviors.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Behaviors.Units
{
    public class Unit
    {
        private UnitWorkPlan _currentWorkPlan;
        public float StepSpeed = 1f;
        public float BaseBuildForce = 0.1f;
        private Building _workingBuilding = null;
        private UnitType _unitType;



        public UnitWorkPlan CurrentWorkPlan
        {
            get { return _currentWorkPlan; }
            protected set { _currentWorkPlan = value; }
        }

        public UnitType UnitType
        {
            get { return _unitType; }
            set { _unitType = value; }
        }

        public Building WorkingBuilding
        {
            get { return _workingBuilding; }
            protected set { _workingBuilding = value; }
        }
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

        public virtual void WorkInBuilding(ref Building building)
        {
            _workingBuilding = building;
            _workingBuilding.WorkingUnits.Add(this);
        }

        public virtual void LeaveBuilding()
        {
            _workingBuilding.WorkingUnits.Remove(this);
            _workingBuilding = null;
        }

        public Position Position { get; set; }

        public float GetBuildForce(BuildingType building)
        {
            return BaseBuildForce;
        }
    }
}
