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
            int index = this.FindIndex(r => r.Resource == resource);
            if (index > -1)
            {
                return this[index];
            }
            else
            {
                var package = new ResourcePackage(resource);
                this.Add(package);
                return package;
            }
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
            int index = this.FindIndex(r => r.Resource == resource);
            if (index == -1)
            {
                this.Add(new ResourcePackage(resource, amount));
            }
            else
            {
                this[index].Amount = amount;
            }
        }
    }
}
