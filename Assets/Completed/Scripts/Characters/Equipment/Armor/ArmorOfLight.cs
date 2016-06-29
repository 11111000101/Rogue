using UnityEngine;
using System.Collections;
using System;

namespace Completed
{
    public class ArmorOfLight : Armor
    {
        int defencePower = 5;
        //ap = 50;
        //time = 5;
        // Defensive (F): Build up a shield negating all incoming dmg for 2s and healing you for every hit for 10HP

        public override void defend(IAttack attack)
        {
            throw new NotImplementedException();
        }

        public override int getDefencePower()
        {
            return defencePower;
        }

        public override Skill getSkill()
        {
            throw new NotImplementedException();
        }
    }
}
