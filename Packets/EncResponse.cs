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

            socket.writeByte(252);
            socket.writeShort((short)enckey.Length);


            for (int i = 0; i < enckey.Length; i++)
            {
                socket.writeByte(enckey[i]);
            }

            socket.writeShort((short)enctoken.Length);

            for (int i = 0; i < enctoken.Length; i++)
            {
                socket.writeByte(enctoken[i]);
            }


        }
        void handle()

        {
            short secretLength = socket.readShort();
            byte[] sharedSecret = socket.readByteArray(secretLength);
            short tokenLength = socket.readShort();
            byte[] token;
            if (tokenLength != 0)
                 token = socket.readByteArray(tokenLength);

            

            if (tokenLength == 0 && secretLength == 0) 
            
            {
                socket.EncEnabled = true;
                ClientResponse response = new ClientResponse(true, socket, form, 0);
                form.puts("Encryption enabled.");
            }
        }
    }
}
