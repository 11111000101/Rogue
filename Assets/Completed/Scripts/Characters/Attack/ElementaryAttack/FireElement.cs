using UnityEngine;
using System.Collections;
using System;

public class FireElement : IElement
{
    public override string getEffectDescription()
    {
        return "Enemy is burning and loses HP over time.";
    }

    public override string getEffectName()
    {
        return "Burning";
    }
}
