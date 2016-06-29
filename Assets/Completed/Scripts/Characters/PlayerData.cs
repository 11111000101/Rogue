using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Completed
{

    public class PlayerData : Character, ICallableObserver
    {
        private int MaxLife = 10;
        private int CurrentNofLifes = 10;

        public const float MaxHP = 100;
        public const float MaxAP = 100;
        public const float MovementCost = 1;

        private float ap = MaxAP;
        private float hp = 100;

        private PlayerInventory inventory;

        public PlayerData()
        {
            inventory = new PlayerInventory();
            this.setMainWeapon(new BasicBow());
        }

        public void setMainWeapon(Weapon weapon)
        {
            Item i = inventory.getItemOnPos(PlayerInventory.WeaponPos);
            if (i != null)
            {
                Weapon w = i as Weapon;
                w.removeObserver(this);
            }
            weapon.addObserver(this);
            weapon.setParent(GameObject.FindGameObjectWithTag("Player"));
            inventory.setItemOnPos(PlayerInventory.WeaponPos, weapon);
        }

        public void update(ICallableObservable observable)
        {
            this.ap -= observable.getRequiredActionPoints();
        }

        public Weapon getMainWeapon()
        {
            return inventory.getItemOnPos(PlayerInventory.WeaponPos) as Weapon;
        }

        public Amulet getAmulet()
        {
            return inventory.getItemOnPos(PlayerInventory.AmuletPos) as Amulet;
        }

        public Armor getArmor()
        {
            return inventory.getItemOnPos(PlayerInventory.ArmorPos) as Armor;
        }

        public void updateAP(float deltaTime)
        {
            ap = Math.Min(ap + deltaTime, MaxAP);
            UserInterface.getInstance().updateAP();
        }

        public void playerMoved()
        {
            this.ap -= MovementCost;
            UserInterface.getInstance().updateAP();
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
            int attackPower = attack.getAttackPower();
            if (this.getArmor() != null)
            {
                attackPower -= this.getArmor().getDefencePower();
            }
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

        public int getCurrentNofLifes()
        {
            return this.CurrentNofLifes;
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

        //#region IHasChanged implementation
        //[SerializeField] Transform slots;
        //public void HasChanged()
        //{
        //    int source = -1;
        //    int destination = -1;
        //    // find emptyslot & newly filled slot
        //    foreach (Transform slotTransform in slots)
        //    {
        //        GameObject item = slotTransform.GetComponent<Slot>().item;

        //        if (InventoryManager.instance.getAmuletSlot() == slotTransform)
        //        {
        //            if (this.getAmulet() == null && item != null)
        //            {
        //                destination = PlayerInventory.AmuletPos;
        //            } else if (this.getAmulet() != null && item == null)
        //            {
        //                source = PlayerInventory.AmuletPos;
        //            }
        //        }
        //        else if (InventoryManager.instance.getArmorSlot() == slotTransform)
        //        {
        //            if (this.getArmor() == null && item != null)
        //            {
        //                destination = PlayerInventory.ArmorPos;
        //            }
        //            else if (this.getAmulet() != null && item == null)
        //            {
        //                source = PlayerInventory.ArmorPos;
        //            }
        //        }
        //        else if (InventoryManager.instance.getWeaponSlot() == slotTransform)
        //        {
        //            if (this.getMainWeapon() == null && item != null)
        //            {
        //                destination = PlayerInventory.WeaponPos;
        //            }
        //            else if (this.getMainWeapon() != null && item == null)
        //            {
        //                source = PlayerInventory.WeaponPos;
        //            }
        //        }
        //        else if (InventoryManager.instance.getInventory1Slot() == slotTransform)
        //        {
        //            if (this.getInventory().getItemOnPos(PlayerInventory.Inv1Pos) == null && item != null)
        //            {
        //                destination = PlayerInventory.Inv1Pos;
        //            }
        //            else if (this.getInventory().getItemOnPos(PlayerInventory.Inv1Pos) != null && item == null)
        //            {
        //                source = PlayerInventory.Inv1Pos;
        //            }
        //        }
        //        else if (InventoryManager.instance.getInventory2Slot() == slotTransform)
        //        {
        //            if (this.getInventory().getItemOnPos(PlayerInventory.Inv2Pos) == null && item != null)
        //            {
        //                destination = PlayerInventory.Inv2Pos;
        //            }
        //            else if (this.getInventory().getItemOnPos(PlayerInventory.Inv2Pos) != null && item == null)
        //            {
        //                source = PlayerInventory.Inv2Pos;
        //            }
        //        }
        //        else if (InventoryManager.instance.getBinSlot() == slotTransform)
        //        {
        //            if (this.getInventory().getItemOnPos(PlayerInventory.Inv2Pos) == null && item != null)
        //            {
        //                destination = PlayerInventory.Inv2Pos;
        //            }
        //            else if (this.getInventory().getItemOnPos(PlayerInventory.Inv2Pos) != null && item == null)
        //            {
        //                source = PlayerInventory.Inv2Pos;
        //            }
        //        }
        //    }
        //    if (source != -1 && destination != -1) // if source and target are found, move item data in inventory according to view
        //    {
        //        MonoBehaviour.print("movingData...");
        //        this.getInventory().setItemOnPos(destination, this.getInventory().getItemOnPos(source));
        //    }
        //}
        //#endregion
    }

}
