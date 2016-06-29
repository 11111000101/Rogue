using UnityEngine;
using System.Collections;
using System;

namespace Completed
{

    public class PlayerData : Character
    {
        private int MaxLife = 10;
        private int CurrentNofLifes = 10;

        public const float MaxAP = 100;
        public const float MovementCost = 1;

        private float ap = MaxAP;
        private float hp = 100;
        private IWeapon weapon { get; set; }
        private IArmor shield { get; set; }

        private PlayerInventory inventory;

        public void updateAP(float deltaTime)
        {
            ap = Math.Min(ap + deltaTime, MaxAP);
            UserInterface.getInstance().updateAP();
        }

        public void playerMoved()
        {
            this.ap -= MovementCost;
        }

        public float getHP()
        {
            return this.hp;
        }

        public float getAP()
        {
            return this.ap;
        }

        // defend is called after an being hit
        // calculate damage and substract it from hp
        public void defend(IAttack attack)
        {
            --this.hp;
            UserInterface.getInstance().updateHP();
            GameManager.getInstance().CheckForGameOver();
        }

        internal bool hasEnoughAPToMove()
        {
            return this.getAP() - MovementCost >= 0;
        }

        public void lost()
        {
            --CurrentNofLifes;
            if (this.CurrentNofLifes > 0)
            {
                this.hp = 100;
                this.ap = MaxAP;
            }
        }
        
        public void onGameWon()
        {
            --this.MaxLife;
            this.hp = 100;
            this.ap = MaxAP;
            this.clearInventory();
        }

        public int getMaxLife()
        {
            return this.MaxLife;
        }

        public void clearInventory()
        {
            this.inventory = new PlayerInventory();
        }

        public PlayerInventory getInventory()
        {
            return this.inventory;
        }

        public void replenishHP()
        {
            this.hp = 100;
        }

        public void replenishAP()
        {
            this.ap = MaxAP;
        }
    }

}
