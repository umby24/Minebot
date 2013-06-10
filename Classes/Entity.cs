using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Classes {

    public class Entity {
        // Container class for players, mobs, ect. to be tracked from.
        public int X, Y, Z, EID;
        public string name; // If named entity.
        public Item held, boots, leggins, chestplate, helmet; // The items we can view about other entities
        public byte animation = 0, yaw, pitch;

        public Entity(int eid, string Name, int x, int y, int z, byte Yaw, byte Pitch, short item) {
            EID = eid;
            name = Name;
            X = x;
            Y = y;
            Z = z;
            yaw = Yaw;
            pitch = Pitch;
            held = new Item(item, 1, 0, 0);
        }

    }
}
