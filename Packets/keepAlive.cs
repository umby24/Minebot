using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class keepAlive
    {
        Wrapped.Wrapped sock;
        Form1 myform;

        public keepAlive(Wrapped.Wrapped socket, Form1 asdf)
        {
            sock = socket;
            myform = asdf;
            handle();
        }

        private void handle()
        {
            int ID = sock.readInt();
            send(ID);
        }

        private void send(int ID)
        {
            sock.writeByte(0);
            sock.writeInt(ID);
        }
    }
}
