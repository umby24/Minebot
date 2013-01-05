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
            socket.readByte();
            int count = socket.readShort();
            Minecraft_Net_Interaction parser = new Minecraft_Net_Interaction();
            while (count != 0)
            {
                parser.readSlot(socket);
                count--;
            }
        }
    }
}
