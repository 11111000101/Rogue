using UnityEngine;
using System;

namespace Completed
{
    public class BasicBow : LongRangeWeapon
    {
        private float castTime = 0.5f;
        private float range = 1f;
        private int requiredActionPoints = 1;
        private int attackPower = 20;
        private IElement element = null;

        public override int getAttackPower()
        {
            return attackPower;
        }

        public override float getCastTime()
        {
            return castTime;
        }

        public override IElement getElement()
        {
            return element;
        }

        public override Texture2D getProjectileTexture()
        {
            return RogueUtils.getTextureFromPath(RogueUtils.getEquipmentIconDir() + "/"
                + "bow/"
                + "arrows/"
                + "BasicArrow.png");
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
            return "bow";
        }

        protected override void attack(Vector2 playerPos, Vector2 targetPos)
        {
            IAttack attack = new ProjectileAttack(this);
            attack.checkProjection(playerPos, targetPos);
        }

        public override void shootProjectile(Vector2 from, Vector2 direction)
        {
            GameObject projectile = createProjectileSprite();

            projectile.transform.position = from;
            projectile.GetComponent<BasicProjectile>().setDirection(direction);
            projectile.GetComponent<BasicProjectile>().setParent(parent);
        }
    }
}