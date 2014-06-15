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
using WaveEngine.Framework.Graphics;
using WaveEngine.Materials;

namespace LaborersWaveGameProject
{
    public class UnitBehavior : Behavior
    {
        protected Transform3D transform;
        public Unit Unit;

        public UnitBehavior ():base("UnitBehavior")
	    {
	    }

        protected override void Initialize()
        {
            Unit = new Unit();
            Unit.StepSpeed = 1f;
            transform = this.Owner.Components.FirstOrDefault(t => t.GetType() == typeof(Transform3D)) as Transform3D; 
        }

        protected override void Update(TimeSpan gameTime)
        {
            Unit.Update();
            if (transform != null)
            {
                transform.Position = new Vector3(Unit.Position.X, Unit.Position.Y, Unit.Position.Z);
            }
        }
    }
}
