using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class SetSlot
    {
        public SetSlot(Wrapped.Wrapped socket, Form1 mainform)
        {
            byte windowId = socket.readByte();
            short slot = socket.readShort();
            functions Parser = new functions();

            Parser.readSlot(socket,true,mainform,slot);
        }
    }
}
