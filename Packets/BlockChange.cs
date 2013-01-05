using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class BlockChange
    {
        public BlockChange(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readByte();
            socket.readInt();
            socket.readShort();
            socket.readByte();
        }

    }
}
