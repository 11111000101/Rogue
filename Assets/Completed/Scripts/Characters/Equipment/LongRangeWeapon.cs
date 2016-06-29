
using UnityEngine;

namespace Completed
{
    public abstract class LongRangeWeapon : Weapon
    {
        public abstract Texture2D getProjectileTexture();
        public abstract void shootProjectile(Vector2 from, Vector2 direction);

        protected GameObject createProjectileSprite()
        {
            GameObject go = new GameObject();

            //Find the Player GameObject using it's tag and store a reference to its transform component.

            Texture2D tex = RogueUtils.getTextureFromPath(RogueUtils.getEquipmentIconDir() + "/"
                + "bow/"
                + "arrows/"
                + "BasicArrow.png");

            go.AddComponent<SpriteRenderer>().sprite = RogueUtils.generateSpriteFromTexture(getProjectileTexture());
            go.AddComponent<Rigidbody2D>();
            RectTransform goTransform = go.AddComponent<RectTransform>();
            goTransform.sizeDelta = new Vector2(0, 0);
            goTransform.anchorMin = new Vector2(0, 0.45f);
            goTransform.anchorMax = new Vector2(1, 0.55f);
            go.AddComponent<BasicProjectile>().setWeapon(this);
            go.layer = LayerMask.NameToLayer("Units");
            go.tag = "Projectile";
            go.GetComponent<SpriteRenderer>().sortingLayerName = "Projectile";

            return go;
        }
    }
}
