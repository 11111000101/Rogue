using UnityEngine;
using System.Collections;

namespace Completed
{
    public class FlameSword : Weapon
    {
        private float castTime = 1f;
        private float range = 1f;
        private int requiredActionPoints = 1;
        private int attackPower = 15;
        private IElement element;

        protected override void attack(Vector2 playerPos, Vector2 target)
        {
            IAttack attack = new MeleeAttack(this);
            attack.checkProjection(playerPos, target);
        }

        public override int getAttackPower()
        {
            return this.attackPower;
        }

        public override float getCastTime()
        {
            return castTime;
        }

        public override IElement getElement()
        {
            return this.element;
        }

        public override float getRange()
        {
            return range;
        }

        public override int getRequiredActionPoints()
        {
            return requiredActionPoints;
        }

        public override string getItemType()
        {
            return "sword";
        }
    }
}