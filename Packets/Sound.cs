using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Sound
    {
        public Sound(Wrapped.Wrapped socket, Form1 Mainform)
        {
            socket.readInt();
            socket.readInt();
            socket.readByte();
            socket.readInt();
            socket.readInt();
            socket.readBool();
        }
    }
}
