using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class BlockAction
    {
        public BlockAction(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readShort();
            socket.readInt();
            socket.readByte();
            socket.readByte();
            socket.readShort();
        }
    }
}
