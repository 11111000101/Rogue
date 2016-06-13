using UnityEngine;
using System.Collections;

namespace Completed
{ 

    public abstract class IAttack
    {
        protected IElement element = null;
        public abstract float calculateDamage(IDefence def);
    }

}
