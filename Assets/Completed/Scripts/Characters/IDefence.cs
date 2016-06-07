using UnityEngine;
using System.Collections;

namespace Completed
{
    public abstract class IDefence
    {
        IElement element { get; set; }
        public abstract void defend(IAttack attack);
    }
}