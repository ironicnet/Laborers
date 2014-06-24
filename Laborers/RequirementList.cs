using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public class ResourceList : List<ResourcePackage>
    {
        public ResourcePackage GetPackage(Resource resource)
        {
            var package = this.FirstOrDefault(r => r.Resource == resource);
            package.Resource = resource;
            return package;
        }


        public void AddAmount(Resource resource, float amount)
        {
            var package = this.GetPackage(resource);
            SetAmount(resource, package.Amount + amount);
        }
        public void SubstractAmount(Resource resource, float amount)
        {
            this.AddAmount(resource, amount * -1);
        }
        public void SetAmount(Resource resource, float amount)
        {
            if (!this.Any(r => r.Resource == resource))
            {
                this.Add(new ResourcePackage(resource, amount));
            }
            else
            {
                var package = this.First(r => r.Resource == resource);
                package.Amount = amount;
            }
        }
    }
}
