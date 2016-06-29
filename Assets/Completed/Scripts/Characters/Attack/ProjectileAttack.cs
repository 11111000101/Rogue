using UnityEngine;
using System;

namespace Completed
{
    public class ProjectileAttack : IAttack
    {
        protected IElement element = null;
        protected float damage;
        private LongRangeWeapon weapon;

        protected int resultingPower;

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

        public ProjectileAttack(LongRangeWeapon weapon)
        {
            this.resultingPower = weapon.getAttackPower();
            this.weapon = weapon;
        }

        public float checkProjection(Vector2 from, Vector2 directionFacing)
        {
            weapon.shootProjectile(from, directionFacing);

            //RaycastHit2D hit = Physics2D.Raycast(from, directionFacing, weapon.getRange(), 1 << LayerMask.NameToLayer("Units"));
            //if (hit.transform != null)
            //{
            //    Character hitComponent = hit.transform.GetComponent<Character>();
            //    hitComponent.defend(this);
            //}
            return 0f;
        }

        public void setResultingPower(int v)
        {
            this.resultingPower = v;
        }
    }
}
