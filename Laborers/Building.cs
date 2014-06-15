using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public class Building
    {
        private Position _position = new Position();

        public virtual Position Position
        {
            get { return _position; }
            set { _position = value; }
        }


        public virtual void Update()
        {
            if (!IsBuilt)
            {
                UpdateConstruction();
            }
        }

        public virtual void UpdateConstruction()
        {
            IsBuilt = true;
        }

        public virtual bool IsBuilt { get; set; }


        public virtual ResourceList Requirements { get; set; }

        public virtual void UpdateRequirements(IRecipeProvider recipeProvider)
        {
            Requirements = recipeProvider.GetRequirementsForBuilding(this);
        }

        public void CheckRequirements(IRecipeProvider recipeProvider)
        {
            if (Requirements == null)
            {
                Requirements = recipeProvider.GetRequirementsForBuilding(this);
            }
        }

        public virtual void ConstructionComplete()
        {
            IsBuilt = true;
        }
    }
}
