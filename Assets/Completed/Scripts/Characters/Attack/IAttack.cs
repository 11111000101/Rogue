using UnityEngine;
using System.Collections;
using System;

namespace Completed
{ 

    public interface IAttack
    {
        int getAttackPower();
        IElement getElement();
        void animate(Vector2 pos, Vector2 direction);
        float checkProjection(Vector2 from, Vector2 directionFacing);
        void setResultingPower(int v);
    }

}
