using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot
{
    class Handshake
    {
        Wrapped.Wrapped sock;
        Form1 myform;

        public Handshake(Wrapped.Wrapped socket, Form1 asdf, bool outgoing = false)
        {
            sock = socket;
            myform = asdf;
            if (outgoing) {            
                send();
            } else {
                handle();
            }
        }

        void handle()
        {
            // Currently C -> S only.
        }

        void send()
        {
            sock.writeByte((byte)2);
            sock.writeByte((byte)78); // 47  -- Update to 1.6.4
            sock.writeString(myform.username);
            sock.writeString(myform.nh.ip);
            sock.writeInt(myform.nh.port);
        }

    }
}
