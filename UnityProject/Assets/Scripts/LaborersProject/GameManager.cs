using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Laborers.Pathfinding;

namespace Laborers
{
    public static class GameManager
    {
        public static IRecipeProvider RecipeProvider { get; set; }

		public static IPathResolver PathResolver { get; set; }

        public static void Init()
        {
            
        }
    }
}
