using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class creativeInventory {


        public creativeInventory(Wrapped.Wrapped socket, Form1 mainform) {
            functions helper = new functions();

            int slot = socket.readShort();
            Classes.Item thisitem = helper.returnSlot(socket);

            if (slot == -1) {
                ClickWindow click2 = new ClickWindow(socket, mainform, -999, 0, 0, thisitem);
            }
        }
        public creativeInventory(Wrapped.Wrapped socket, Form1 mainform, short slot, Classes.Item clicked) {
            functions helper = new functions();

            socket.SendByte(0x6b);
            socket.writeShort(slot);
            helper.writeSlot(clicked, socket);
        }
    }
}
