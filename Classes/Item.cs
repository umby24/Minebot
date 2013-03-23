using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Classes {
    public class Item {
        public int itemID;
        public byte itemCount;
        public short itemDamage;
        public short slot;

        public Item(int ItemID, byte ItemCount, short ItemDamage,short Slot) {
            itemID = ItemID;
            itemCount = ItemCount;
            itemDamage = ItemDamage;
            slot = Slot;
        }
    }
}
