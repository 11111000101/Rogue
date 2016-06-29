using System;
using UnityEngine;

namespace Completed
{
    public class MeleeAttack : IAttack
    {
        protected Vector2 from;
        protected Vector2 directionFacing;

        protected IElement element = null;
        protected float damage;
        private Animation animation;

        private Weapon weapon;
        private int resultingPower;

        public MeleeAttack(Weapon weapon)
        {
            this.weapon = weapon;
            this.resultingPower = weapon.getAttackPower();
        }

        public float checkProjection(Vector2 from, Vector2 directionFacing)
        {
            this.from = from; this.directionFacing = directionFacing;

            RaycastHit2D hit = Physics2D.Raycast(from, directionFacing, weapon.getRange(), 1 << LayerMask.NameToLayer("Units"));
            if (hit.transform != null)
            {
                Character hitComponent = hit.transform.GetComponent<Character>();
                hitComponent.defend(this);
            }
            return 0f;
        }

        public int getAttackPower()
        {
            return weapon.getAttackPower();
        }

        public IElement getElement()
        {
            return weapon.getElement();
        }

        public void animate(Vector2 pos, Vector2 direction)
        {
            throw new NotImplementedException();
        }

        public void setResultingPower(int v)
        {
            this.resultingPower = v;
        }
    }
}