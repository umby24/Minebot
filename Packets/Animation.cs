using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Animation
    {
        Wrapped.Wrapped Socket;
        Form1 mainform;

        public Animation(Wrapped.Wrapped socket, Form1 Mainform, bool outgoing = false)
        {
            Socket = socket;
            mainform = Mainform;

            if (outgoing)
            {

            }
            else
            {
                handle();
            }
        }

        void handle()
        {
            Socket.readInt();
            Socket.readByte();
        }

        void send()
        {
        }
    }
}
