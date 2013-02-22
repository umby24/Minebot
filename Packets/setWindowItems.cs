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

            functions parser = new functions();

            while (count != 0)
            {
                parser.readSlot(socket);
                count--;
            }
        }
    }
}
