using Laborers.Behaviors.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers.Behaviors.Buildings
{
		public class Building
		{
				private List<Unit> _workingUnits = new List<Unit> ();
				private Position _position = new Position ();
				private ResourceList _storage = new ResourceList ();

				public BuildingType BuildingType {
						get;
						protected set;
				}

				public virtual bool IsBuilt {
						get;
						protected set;
				}

				public virtual ResourceList Requirements {
						get;
						protected set;
				}

				public ResourceList Storage {
						get { return _storage; }
						protected set { _storage = value; }
				}

				public virtual Position Position {
						get { return _position; }
						set { _position = value; }
				}

				public List<Unit> WorkingUnits {
						get { return _workingUnits; }
						set { _workingUnits = value; }
				}

				public virtual void Init ()
				{
						CheckRequirements (GameManager.RecipeProvider);
				}

				public virtual void Update ()
				{
						if (!IsBuilt) {
								UpdateConstruction ();
						}
				}

				public virtual void UpdateConstruction ()
				{
						CheckRequirements (GameManager.RecipeProvider);
						if (Requirements.GetPackage (Resource.WorkForce).Amount > 0) {
								if (WorkingUnits.Count > 0) {
										float workforce = WorkingUnits.Sum (unit => unit.GetBuildForce (this.BuildingType));
										Storage.AddAmount (Resource.WorkForce, workforce);
								}
								if (Storage.GetPackage (Resource.WorkForce).Amount >= Requirements.GetPackage (Resource.WorkForce).Amount) {
										IsBuilt = true;
								}
						} else {
								IsBuilt = true;
						}
				}

				public virtual void UpdateRequirements (IRecipeProvider recipeProvider)
				{
						Requirements = recipeProvider.GetRequirementsForBuilding (this.BuildingType);
				}

				public void CheckRequirements (IRecipeProvider recipeProvider)
				{
						if (Requirements == null) {
								UpdateRequirements (recipeProvider);
						}
				}

				public virtual void ConstructionComplete ()
				{
						IsBuilt = true;
				}

		}
}
