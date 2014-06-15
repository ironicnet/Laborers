using Laborers;
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

namespace LaborersWaveGameProject
{
    public class UnitBehavior : Behavior
    {
        protected Transform3D Transform { get; set; }
        public Unit Unit;

        public UnitBehavior ():base("UnitBehavior")
        {
	    }

        protected override void Initialize()
        {
            Unit = new Unit();
            Unit.StepSpeed = 0.02f;
            Transform = this.Owner.Components.FirstOrDefault(t => t.GetType() == typeof(Transform3D)) as Transform3D;
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

        }
    }
}
