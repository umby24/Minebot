using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class timeUpdate
    {
        Wrapped.Wrapped socket;
        Form1 mainform;

        public timeUpdate(Wrapped.Wrapped sock, Form1 myform)
        {
            socket = sock;
            mainform = myform;
            handle();
        }

        private void handle()
        {
            mainform.worldAge = socket.readLong();
            mainform.time = socket.readLong();
            PPaL pal;
            if (mainform.location != null)
                pal = new PPaL(socket, mainform,true);
        }
    }
}
