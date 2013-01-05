using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class PlayerLook
    {
        public PlayerLook(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.writeByte(12);
            socket.writeFloat(mainform.position[0]);
            socket.writeFloat(mainform.position[1]);
            socket.writeBool(mainform.onground);
        }
    }
}
