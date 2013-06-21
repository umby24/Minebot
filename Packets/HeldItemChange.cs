using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class HeldItemChange
    {
        Wrapped.Wrapped socket;
        Form1 myform;

        public HeldItemChange(Wrapped.Wrapped Socket, Form1 Myform, bool outgoing = false)
        {
            socket = Socket;
            myform = Myform;

            if (outgoing)
            {
                send();
            }
            else
            {
                handle();
            }
        }

        void handle()
        {
            myform.selectedSlot = socket.readShort();
        }

        void send()
        {
            socket.writeByte(16);
            socket.writeShort(myform.selectedSlot);
        }
    }
}
