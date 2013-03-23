using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot {
    class functions {

        public void readMetadata(Wrapped.Wrapped socket) {
            do {

                byte item = socket.readByte();

                if (item == 127) break;
                int index = item & 0x1F;
                int type = item >> 5;

                switch (type) {
                    case 0:
                        socket.readByte();
                        break;
                    case 1:
                        socket.readShort();
                        break;
                    case 2:
                        socket.readInt();
                        break;
                    case 3:
                        socket.readFloat();
                        break;
                    case 4:
                        socket.readString();
                        break;
                    case 5:
                        readSlot(socket);
                        break;
                    case 6:
                        socket.readInt();
                        socket.readInt();
                        socket.readInt();
                        break;

                }
            } while (true);
        }

        public void readSlot(Wrapped.Wrapped socket, bool inventory = false, Form1 Mainform = null, short slot = 500) {
            // Just in case..
            if (inventory == true & Mainform == null) {
                throw new Exception("If inventory is true, you must include a main form.");
            }
            if ((inventory == true) & slot == 500) {
                throw new Exception("If inventory is true, you must include which slot to add to.");
            }

            int blockID = socket.readShort();

            if (blockID == -1)
                return;

            byte itemCount = socket.readByte();
            short damage = socket.readShort();
            int NBTLength = socket.readShort();

            if (NBTLength == -1)
                return;

            socket.readByteArray(NBTLength);

            if (Mainform.inventory == null && inventory == true)
                Mainform.inventory = new List<Classes.Item>();

            if (inventory == true)
                Mainform.inventory.Add(new Classes.Item(blockID, itemCount, damage, slot));

        }

        public string strip_codes(string text) {
            // Strips the color codes from text.
            string smessage = text;
            if (smessage.Contains("§")) {
                smessage = smessage.Replace("§0", "");
                smessage = smessage.Replace("§1", "");
                smessage = smessage.Replace("§2", "");
                smessage = smessage.Replace("§3", "");
                smessage = smessage.Replace("§4", "");
                smessage = smessage.Replace("§5", "");
                smessage = smessage.Replace("§6", "");
                smessage = smessage.Replace("§7", "");
                smessage = smessage.Replace("§8", "");
                smessage = smessage.Replace("§9", "");
                smessage = smessage.Replace("§a", "");
                smessage = smessage.Replace("§b", "");
                smessage = smessage.Replace("§c", "");
                smessage = smessage.Replace("§d", "");
                smessage = smessage.Replace("§e", "");
                smessage = smessage.Replace("§f", "");
                smessage = smessage.Replace("§A", "");
                smessage = smessage.Replace("§B", "");
                smessage = smessage.Replace("§C", "");
                smessage = smessage.Replace("§D", "");
                smessage = smessage.Replace("§E", "");
                smessage = smessage.Replace("§F", "");
            }
            return smessage;
        }

        public int getitembyslot(int slot, Form1 mainform) {
            int item = 0;

            foreach (Classes.Item b in mainform.inventory) {
                if (b.slot == slot) {
                    item = b.itemID;
                    break;
                }
            }
            return item;
        }
    }
}
