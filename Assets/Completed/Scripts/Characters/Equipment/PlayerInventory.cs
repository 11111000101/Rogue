namespace Completed
{
    public class PlayerInventory
    {
        private Item[] bag = new Item[7];

        public const int WeaponPos = 0;
        public const int ArmorPos = 1;
        public const int AmuletPos = 2;
        public const int Inv1Pos = 3;
        public const int Inv2Pos = 4;
        public const int BinPos = 5;
        public const int ChestPos = 6;
        
        private int nofHealthPotions = 0;

        public void addHPPotion()
        {
            ++this.nofHealthPotions;
        }

        public void removeHPPotion()
        {
            --this.nofHealthPotions;
        }

        public Item getItemOnPos(int pos)
        {
            return bag[pos];
        }

        public void setItemOnPos(int pos, Item i)
        {
            this.bag[pos] = i;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < bag.Length; ++i)
            {
                s += "i: " + bag[i];
            }
            return s;
        }
    }
}
