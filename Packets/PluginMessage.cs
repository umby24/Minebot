using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class PluginMessage
    {
        public PluginMessage(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readString();
            int length = socket.readShort();
            if (length != 0)
                socket.readByteArray(length);
        }
    }
}
