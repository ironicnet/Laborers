using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public interface IRecipeProvider
    {
        ResourceList GetRequirementsForBuilding(Building building);
    }
}
