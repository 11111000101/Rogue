using UnityEngine;
using System.Collections;

namespace Completed
{

    public interface Character
    {
        float getHP();
        void defend(IAttack attack);
    }
}