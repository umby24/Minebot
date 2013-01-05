using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class updateTileEntity
    {
        public updateTileEntity(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            socket.readShort();
            socket.readInt();
            socket.readByte();
            int length = socket.readShort();
            if (length != 0)
                socket.readByteArray(length);

        }
    }
}
