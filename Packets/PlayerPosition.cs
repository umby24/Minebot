using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class PlayerPosition
    {
        public PlayerPosition(Wrapped.Wrapped Socket, Form1 Mainform)
        {
            Socket.writeByte(11);
            Socket.writeDouble(Mainform.location[0]);
            Socket.writeDouble(Mainform.location[1]);
            Socket.writeDouble(Mainform.location[3]);
            Socket.writeDouble(Mainform.location[2]);
            Socket.writeBool(Mainform.onground);
        }
    }
}
