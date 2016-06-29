using System;
using UnityEngine;

namespace Completed
{
    public abstract class Armor : Item
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

        //****************************************************
        

        public abstract Skill getSkill();

        public abstract int getDefencePower();

        public abstract void defend(IAttack attack);

        public string getItemType()
        {
            return "armor";
        }
    }
}