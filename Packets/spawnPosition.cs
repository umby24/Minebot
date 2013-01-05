using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class spawnPosition
    {
        Wrapped.Wrapped socket;
        Form1 mainform;

        public spawnPosition(Wrapped.Wrapped sock, Form1 myform)
        {
            socket = sock;
            mainform = myform;
            handle();
        }

        void handle()
        {
            int X = socket.readInt();
            int Y = socket.readInt();
            int Z = socket.readInt();
            mainform.spawnPoint = new int[3] { X, Y, Z };
        }
    }
}
