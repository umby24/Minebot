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
            Socket.writeDouble(Math.Round(Mainform.location[0],1));
            Socket.writeDouble(Math.Round(Mainform.location[1],1));
            Socket.writeDouble(Math.Round(Mainform.location[3],1));
            Socket.writeDouble(Math.Round(Mainform.location[2],1));
            Socket.writeBool(Mainform.onground);
        }
    }
}
