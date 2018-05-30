using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.IdentityNPocoStores
{
    public class IdentityConcurrencyException : Exception
    {
        public IdentityConcurrencyException()
            : base("Could not update entity, it was changed by someone else prior to your save request")
        { }
    }
}
