using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets {
    class ClickWindow {
        //Client-to-Server only

        public ClickWindow(Wrapped.Wrapped socket, Form1 MainForm, short slot, byte mouse, byte mode, Classes.Item clicked) {
            socket.writeByte(0x66);
            socket.writeByte(0);
            socket.writeShort(slot);
            socket.writeByte(mouse);
            socket.writeShort(13);
            socket.writeByte(mode);
            //TODO: Implement slot data writing
            socket.writeShort((short)clicked.itemID);
            socket.writeByte(clicked.itemCount);
            socket.writeShort(clicked.itemDamage);
            socket.writeShort(-1);

            MainForm.inventory.Remove(clicked);
        }
    }
}
