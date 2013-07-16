using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;

namespace C_Minebot.Packets {
    class placeBlock {
        // Client to server only
        public placeBlock(int x, byte y, int z, byte dir, Item block, Wrapped.Wrapped sock) {
            functions helper = new functions();

            sock.writeByte(0x0F);
            sock.writeInt(x);
            sock.writeByte(y);
            sock.writeInt(z);
            sock.writeByte(dir);
            helper.writeSlot(block, sock);
            sock.writeByte(9);
            sock.writeByte(9);
            sock.writeByte(9);
        }

    }
}
