using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class ClientResponse
    {
        Wrapped.Wrapped sock;
        Form1 myform;
        byte code;
        public ClientResponse(bool outgoing, Wrapped.Wrapped socket, Form1 asdf,byte codey)
        {
            sock = socket;
            myform = asdf;
            code = codey;
            if (outgoing)
            {
                send();
            }
            else
            {
                handle();
            }
        }
        void send()
        {
            sock.writeByte(205);
            sock.writeByte(code);
        }
        void handle()
        {

        }
    }
}
