using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EntMetadata
    {
        public EntMetadata(Wrapped.Wrapped socket, Form1 mainform)
        {
            socket.readInt();
            functions parser = new functions();
            parser.readMetadata(socket);
        }
    }
}
