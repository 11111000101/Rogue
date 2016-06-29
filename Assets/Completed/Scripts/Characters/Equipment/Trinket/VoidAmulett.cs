using UnityEngine;
using System.Collections;
using System;

namespace Completed
{
    public class VoidAmulett : Trinket
    {
        protected Skill skill;
        public override Texture2D getIcon()
        {
            throw new NotImplementedException();
        }

        public override string getItemType()
        {
            return "trinket";
        }

        public override int getRequiredActionPoints()
        {
            return skill.getRequiredActionPoints();
        }

        public override Skill getSkill()
        {
            //ap = 5;
            //cooldown = 1;
            //duration = 1;
            // Utility (Q): Block the next magic attack for 1s
            return skill;
        }
    }
}
