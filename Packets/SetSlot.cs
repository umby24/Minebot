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
            socket.readByte();
            socket.readShort();
            functions Parser = new functions();
            Parser.readSlot(socket);
        }
    }
}
