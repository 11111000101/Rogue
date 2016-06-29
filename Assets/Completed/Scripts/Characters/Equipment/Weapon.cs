
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public abstract class Weapon : Item, ICallableObservable
    {
        //**************** Item ******************************
        public Texture2D getIcon()
        {
            return RogueUtils.getTextureFromPath(getIconPath());
        }

        public string getIconPath()
        {
            return RogueUtils.getEquipmentIconDir() + "/"
                + getItemType() + "/"
                + GetType().Name + ".png";
        }

        public abstract string getItemType();

        //************ ICallableObservable *******************

        public abstract int getRequiredActionPoints();

        private List<ICallableObserver> observer = new List<ICallableObserver>();

        public void addObserver(ICallableObserver observer)
        {
            this.observer.Add(observer);
        }

        public void removeObserver(ICallableObserver observer)
        {
            this.observer.Remove(observer);
        }

        //**************** Weapon *****************************
        public abstract float getCastTime();
        public abstract float getRange();
        public abstract int getAttackPower();
        public abstract IElement getElement();

        protected GameObject parent;

        public void setParent(GameObject parent)
        {
            this.parent = parent;
        }

        protected abstract void attack(Vector2 playerPos, Vector2 targetPos);

        public void performAttack(Vector2 playerPos, Vector2 target)
        {
            foreach (ICallableObserver obs in observer)
            {
                obs.update(this);
            }

            this.attack(playerPos, target);
        }
    }
}