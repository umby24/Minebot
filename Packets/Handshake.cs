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

        public Handshake(bool outgoing, Wrapped.Wrapped socket,Form1 asdf)
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
            sock.writeByte((byte)60); // 47
            sock.writeString(myform.username);
            sock.writeString(myform.nh.ip);
            sock.writeInt(myform.nh.port);
        }

    }
}
