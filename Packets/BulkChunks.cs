using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class BulkChunks
    {
        public BulkChunks(Wrapped.Wrapped socket, Form1 mainform)
        {
            short columncount = socket.readShort();
            int Datalength = socket.readInt();
            socket.readByte();
            socket.readByteArray(Datalength);
            while (columncount != 0)
            {
                socket.readInt();
                socket.readInt();
                socket.readShort();
                socket.readShort();
                columncount--;
            }
        }
    }
}
