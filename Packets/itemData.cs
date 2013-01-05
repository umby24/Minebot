using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class itemData
    {
        public itemData(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readShort();
            socket.readShort();
            int txtlength = socket.readShort();
            socket.readByteArray(txtlength);
        }
    }
}
