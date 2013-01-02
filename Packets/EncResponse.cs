using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class EncResponse
    {
        Wrapped.Wrapped socket;
        Form1 form;
        byte[] enctoken;
        byte[] enckey;

        public EncResponse(bool outgoing, Wrapped.Wrapped sock, Form1 myform)
        {
            socket = sock;
            form = myform;
            if (outgoing)
            {
                send();
            }
            else
            {
                handle();
            }
        }
        public EncResponse(bool outgoing, Wrapped.Wrapped sock, Form1 myform,byte[] Etoken,byte[] Ekey)
        {
            socket = sock;
            form = myform;
            enctoken = Etoken;
            enckey = Ekey;
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
            int written = 0;
            socket.writeByte(252);
            socket.writeShort((short)enckey.Length);
            form.puts(Convert.ToString(enckey.Length));
            for (int i = 0; i < enckey.Length; i++)
            {
                written++;
                socket.writeByte(enckey[i]);
            }
            //socket.writeByte(0);
            socket.writeShort((short)enctoken.Length);
            form.puts(Convert.ToString(enctoken.Length));
            form.puts(Convert.ToString(written));
            written = 0;
            for (int i = 0; i < enctoken.Length; i++)
            {
                written++;
                socket.writeByte(enctoken[i]);
            }
           // socket.writeByte(0);
            form.nh.logging = true;
            form.puts(Convert.ToString(written));
            form.puts("Wrote response.");

        }
        void handle()

        {
            byte[] zeros = socket.readByteArray(4);
                form.puts("Received encryption response; Enabling encryption now!");
                socket.EncEnabled = true;
                ClientResponse response = new ClientResponse(true, socket, form, 0);
                form.puts("Sent client response!");
            
        }
    }
}
