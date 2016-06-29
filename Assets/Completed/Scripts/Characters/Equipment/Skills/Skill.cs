using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public abstract class Skill : ISkillObservable
    {
        #region ISkillObservable implementation
        public abstract int getRequiredActionPoints();

        private List<ISkillObserver> observer = new List<ISkillObserver>();

        public void addObserver(ISkillObserver observer)
        {
            this.observer.Add(observer);
        }

        public void removeObserver(ISkillObserver observer)
        {
            this.observer.Remove(observer);
        }
        #endregion

        public abstract bool isPermanent();
        public abstract float getCoolDownTime();
        public abstract float nextCast();

        public abstract void cast();

        public abstract Texture2D getTexture();
    }
}