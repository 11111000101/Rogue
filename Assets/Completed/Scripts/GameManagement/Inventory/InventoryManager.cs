using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Completed
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance = null;             //Static instance of InventoryLoader which allows it to be accessed by any other script.

        private Dictionary<Transform, int> slotDictionary = new Dictionary<Transform, int>();
        private Dictionary<int, Transform> inversedSlotDictionary = new Dictionary<int, Transform>();

        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

            GameManager.getInstance().setInventoryManager(this);
        }

        void Start()
        {
            initializeDictionary();
            initializeInversedDictionary();

            this.dropRandomItem();
            this.loadMainWeapon();
            this.loadArmor();
            this.loadAmulet();
        }

        protected void initializeDictionary()
        {
            slotDictionary.Add(GameObject.Find("WeaponPanelSlot").transform, PlayerInventory.WeaponPos);
            slotDictionary.Add(GameObject.Find("ArmorPanelSlot").transform,  PlayerInventory.ArmorPos);
            slotDictionary.Add(GameObject.Find("AmuletPanelSlot").transform, PlayerInventory.AmuletPos);
            slotDictionary.Add(GameObject.Find("InvSlot1").transform,        PlayerInventory.Inv1Pos);
            slotDictionary.Add(GameObject.Find("InvSlot2").transform,        PlayerInventory.Inv2Pos);
            slotDictionary.Add(GameObject.Find("TrashSlot").transform,       PlayerInventory.BinPos);
            slotDictionary.Add(GameObject.Find("ChestPanelSlot").transform,  PlayerInventory.ChestPos);
        }

        protected void initializeInversedDictionary()
        {
            foreach (Transform transform in slotDictionary.Keys)
            {
                inversedSlotDictionary.Add(slotDictionary[transform], transform);
            }
        }

        protected int getTypeStoredInSlot(Transform slot)
        {
            Item i = GameManager.getInstance().getPlayerData().getInventory().getItemOnPos(slotDictionary[slot]);
            print("typeStoredInSlot: " + (i == null));
            return getTypeFromItem(i);
        }

        protected int getTypeFromItem(Item i)
        {
            int n = -1;
            if (i is Weapon) return PlayerInventory.WeaponPos;
            if (i is Armor) return PlayerInventory.ArmorPos;
            if (i is Amulet) return PlayerInventory.AmuletPos;
            print("" + n);
            return n;
        }

        public bool isItemMovementPossible(Transform previousParent, Transform transform)
        {
            print("isItemMovementPossible: " + previousParent + " " + transform);
            if (slotDictionary.ContainsKey(previousParent) && slotDictionary.ContainsKey(transform))
            {
                if (slotDictionary[transform] >= PlayerInventory.Inv1Pos)    // dragging to bag slots
                {
                    return true;
                } else if (getTypeStoredInSlot(previousParent) == slotDictionary[transform])  // item from bag to equipment
                {
                    return true;
                }
            }
            print("SlotTransform not found! " + slotDictionary.ContainsKey(previousParent) + " " + slotDictionary.ContainsKey(transform));
            return false;
        }

        public void onExitBtnClicked()
        {
            GameManager.getInstance().onClosePlayerInventory();
        }

        public void loadMainWeapon()
        {
            Item weapon = GameManager.getInstance().getPlayerData().getMainWeapon();
            if (weapon != null)
            {
                createItemForParent(GameObject.Find("WeaponPanelSlot").transform, weapon);
            }
        }

        protected void loadArmor()
        {
            Item item = GameManager.getInstance().getPlayerData().getArmor();
            if (item != null)
            {
                createItemForParent(GameObject.Find("ArmorPanelSlot").transform, item);
            }
        }

        protected void loadAmulet()
        {
            Item item = GameManager.getInstance().getPlayerData().getAmulet();
            if (item != null)
            {
                createItemForParent(GameObject.Find("AmuletPanelSlot").transform, item);
            }
        }

        public Transform getWeaponSlot()
        {
            return GameObject.Find("WeaponPanelSlot").transform;
        }

        public Transform getArmorSlot()
        {
            return GameObject.Find("ArmorPanelSlot").transform;
        }

        public Transform getAmuletSlot()
        {
            return GameObject.Find("AmuletPanelSlot").transform;
        }

        public Transform getBinSlot()
        {
            return GameObject.Find("TrashSlot").transform;
        }

        public Transform getInventory1Slot()
        {
            return GameObject.Find("InvSlot1").transform;
        }

        public Transform getInventory2Slot()
        {
            return GameObject.Find("InvSlot2").transform;
        }

        public void setTextureOnImage(Image img, Texture2D tex)
        {
            img.overrideSprite = RogueUtils.generateSpriteFromTexture(tex);
        }

        private Item drop = null;

        public void dropRandomItem()
        {
            //this.drop = ItemFactory.generateRandomItem();
            Item[] items = new Item[2];
            items[0] = new BasicSword();
            items[1] = new BasicBow();

            this.drop = items[Random.Range(0, items.Length-1)];
            createItemForParent(GameObject.Find("ChestPanelSlot").transform, this.drop);
            GameManager.getInstance().getPlayerData().getInventory().setItemOnPos(PlayerInventory.ChestPos, this.drop);
        }

        protected void createItemForParent(Transform parent, Item item)
        {
            GameObject go = new GameObject();
            go.name = "Weapon";
            go.transform.SetParent(parent);
            Image itemImage = go.AddComponent<Image>();
            itemImage.gameObject.AddComponent<CanvasGroup>();
            itemImage.gameObject.AddComponent<InventoryDragHandler>();
            itemImage.rectTransform.anchorMin = new Vector2(0, 0);
            itemImage.rectTransform.anchorMax = new Vector2(1, 1);
            itemImage.rectTransform.position = new Vector3(0, 0, 0);
            itemImage.rectTransform.sizeDelta = new Vector2();
            
            this.setTextureOnImage(itemImage, item.getIcon());
        }


        #region IHasChanged implementation
        [SerializeField] Transform slots;
        public void HasChanged(Transform previousParent, Transform newParent)
        {
            PlayerData pData = GameManager.getInstance().getPlayerData();
            int source = slotDictionary[previousParent];
            int destination = slotDictionary[newParent];

            Item i = GameManager.getInstance().getPlayerData().getInventory().getItemOnPos(source);
            GameManager.getInstance().getPlayerData().getInventory().setItemOnPos(destination, i);
            GameManager.getInstance().getPlayerData().getInventory().setItemOnPos(source, null);
        }
        #endregion
    }
}
