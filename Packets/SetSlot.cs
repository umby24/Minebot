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
            Minecraft_Net_Interaction Parser = new Minecraft_Net_Interaction();
            Parser.readSlot(socket);
        }
    }
}
