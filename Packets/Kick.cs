using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Kick
    {
        Wrapped.Wrapped sock;
        Form1 myform;

        public Kick(bool outgoing, Wrapped.Wrapped socket, Form1 asdf)
        {
            sock = socket;
            myform = asdf;
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
            myform.puts("Kicked by server! Reason: " + sock.readString());
            myform.nh.stop();
        }

        void send()
        {
            sock.writeByte(255);
            sock.writeString("Minebot closed.");
        }
    }
}
