using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborers
{
    public class BaseRecipeProvider :IRecipeProvider
    {
        public ResourceList GetRequirementsForBuilding(Behaviors.Buildings.BuildingType building)
        {
            return new ResourceList() { new ResourcePackage(Resource.WorkForce, 100)};
        }
    }
}
