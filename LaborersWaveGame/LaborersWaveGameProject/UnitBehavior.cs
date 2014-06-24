using Laborers;
using Laborers.Behaviors;
using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Diagnostic;
using WaveEngine.Framework.Graphics;
using WaveEngine.Materials;
using Laborers.Tasks;

namespace LaborersWaveGameProject
{
    public class UnitBehavior : Behavior
    {
        protected WorkPlanTask _lastTask;
        protected WorkPlanTask _currentTask;
        protected UnitTaskAnimationType _lastAnimationType;
        protected UnitTaskAnimationType _currentAnimationType;
        protected Transform3D Transform { get; set; }
        protected ModelRenderer Renderer { get; set; }
        public Unit Unit;

        public UnitBehavior ():base("UnitBehavior")
        {
	    }

        protected override void Initialize()
        {
            Unit = new Unit();
            Transform = this.Owner.FindComponent<Transform3D>();
            Renderer = this.Owner.FindComponent<ModelRenderer>();
            Unit.StepSpeed = 0.02f;
            Unit.Position = new Position(Transform.Position.X, Transform.Position.Y, Transform.Position.Z);
        }

        protected override void Update(TimeSpan gameTime)
        {
            Unit.Update();
            if (Transform != null)
            {
                Transform.Position = new Vector3(Unit.Position.X, Unit.Position.Y, Unit.Position.Z);
            }
            Labels.Add("Unit position", Transform.Position.ToString());
            if (Unit.CurrentWorkPlan != null && Unit.CurrentWorkPlan.CurrentTask != null)
            {
                var task = Unit.CurrentWorkPlan.CurrentTask;
                if (task != _currentTask)
                {
                    _lastTask = _currentTask;
                    _currentTask = task;
                    _lastAnimationType = _currentAnimationType;
                    _currentAnimationType = task.AnimationType;
                }
            }
            else
            {
                _lastTask = _currentTask;
                _currentTask = null;
                _lastAnimationType = _currentAnimationType;
                _currentAnimationType = UnitTaskAnimationType.Idle;
            }

            BasicMaterial unitMaterial = (Renderer.MaterialMap.DefaultMaterial as BasicMaterial);

            switch (_currentAnimationType)
            {
                case UnitTaskAnimationType.Idle:
                    unitMaterial.DiffuseColor = Color.BlueViolet;
                    break;
                case UnitTaskAnimationType.Walking:
                    unitMaterial.DiffuseColor = Color.PaleVioletRed;
                    break;
                case UnitTaskAnimationType.Building:
                    unitMaterial.DiffuseColor = Color.Red;
                    break;
            }
        }

    }
}
