using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class OpenWindow
    {
        public OpenWindow(Wrapped.Wrapped socket, Form1 mainform)
        {
            int meh = socket.readByte();
            byte invType = socket.readByte();
            socket.readString();
            socket.readByte();
            bool useTitle = socket.readBool();

            if (invType == 11)
                socket.readInt();
        }
    }
}
