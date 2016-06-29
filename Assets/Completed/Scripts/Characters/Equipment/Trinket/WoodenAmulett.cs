using UnityEngine;
using System.Collections;
using System;

namespace Completed
{
    public class WoodenAmulett : Trinket
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
            // 5
            return getSkill().getRequiredActionPoints();
        }

        public override Skill getSkill()
        {
            return skill;
        }
    }
}