using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class setWindowItems
    {
        public setWindowItems(Wrapped.Wrapped socket, Form1 mainform)
        {
            byte windowid = socket.readByte();
            int count = socket.readShort();
            int asdf = 0;
            functions parser = new functions();
            if (windowid == 0) {
                for (short i = 0; i < count; i++) {
                    parser.readSlot(socket, true, mainform, i);
                    asdf++;
                }
            } else {
                while (count != 0) {
                    parser.readSlot(socket);
                    count--;
                }
            }

        }
    }
}
