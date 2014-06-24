using Laborers;
using Laborers.Behaviors;
using Laborers.Behaviors.Buildings;
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
    public class BuildingBehavior : Behavior
    {
        protected Transform3D transform;
        protected ModelRenderer renderer;
        public Building Building;

        public BuildingBehavior()
            : base("BuildingBehavior")
	    {
	    }

        protected override void Initialize()
        {
            Building = new Building();
            transform = this.Owner.FindComponent<Transform3D>();
            renderer = this.Owner.FindComponent<ModelRenderer>();
            Building.Position = new Position(transform.Position.X, transform.Position.Y, transform.Position.Z);
        }
        protected override void Update(TimeSpan gameTime)
        {
            Building.Update();
            if (transform != null)
            {
                transform.Position = new Vector3(Building.Position.X, Building.Position.Y, Building.Position.Z);
            }
            if (Building.IsBuilt)
            {
                (renderer.MaterialMap.DefaultMaterial as BasicMaterial).DiffuseColor = Color.Green;
            }
            else
            {
                (renderer.MaterialMap.DefaultMaterial as BasicMaterial).DiffuseColor = Color.Red;
            }
        }

        internal void Init()
        {
            Building.Init();
        }
    }
}
