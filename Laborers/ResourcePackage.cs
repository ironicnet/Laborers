using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laborers
{
    public struct ResourcePackage
    {
        private Resource _resource;
        private float _amount;
        public ResourcePackage(Resource resource)
            : this()
        {
            _resource = resource;
        }
        public ResourcePackage(Resource resource, float amount)
            : this(resource)
        {
            _amount = amount;
        }

        public Resource Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        public float Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
