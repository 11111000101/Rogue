using UnityEngine;
using System;

namespace Completed
{
    public class IronAmulett : Trinket
    {
        protected Skill skill;

        public override Texture2D getIcon()
        {
            throw new NotImplementedException();
        }

        public override string getItemType()
        {
            throw new NotImplementedException();
        }

        public override int getRequiredActionPoints()
        {
            // 5
            return getSkill().getRequiredActionPoints();
        }

        public override Skill getSkill()
        {
            //cooldown = 1;
            //duration = 1;
            // Utility (Q): Block the next melee attack for 1s
            return skill;
        }
        

    }
}
