using UnityEngine;
using System.Collections;

namespace Completed
{
    public class RunicArmor : Armor
    {
        protected Skill skill;
        protected int defencePower = 10;
        //ap = 25;
        //time = 5;
        //armor = 10;

        public override void defend(IAttack attack)
        {
            attack.setResultingPower(System.Math.Max(attack.getAttackPower() - this.getDefencePower(), 0));
        }

        public override int getDefencePower()
        {
            return defencePower;
        }

        public override Skill getSkill()
        {
            // Defensive (F): Build up a Shield absorbing 50dmg for 20s
            return skill;
        }
    }
}
