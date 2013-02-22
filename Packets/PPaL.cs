using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class PPaL
    {
        // Player position and look
        Wrapped.Wrapped Socket;
        Form1 mainform;

        public PPaL(bool outgoing, Wrapped.Wrapped socket, Form1 Mainform)
        {
            Socket = socket;
            mainform = Mainform;

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
            //Lets try doing this the fast way.
            //byte[] myloc = new byte[42];
            //myloc[0] = 13;
            //Array.Copy(BitConverter.GetBytes(mainform.location[0]), 0, myloc, 1, 8);
            //Array.Copy(BitConverter.GetBytes(mainform.location[1]), 0, myloc, 9, 8);
            //Array.Copy(BitConverter.GetBytes(mainform.location[3]), 0, myloc, 17, 8);
            //Array.Copy(BitConverter.GetBytes(mainform.location[2]), 0, myloc, 25, 8);
            //Array.Copy(BitConverter.GetBytes(mainform.position[0]), 0, myloc, 33, 4);
            //Array.Copy(BitConverter.GetBytes(mainform.position[1]), 0, myloc, 37, 4);
            //myloc[41] = BitConverter.GetBytes(mainform.onground)[0];
            //Socket._stream.Write(myloc, 0, 42);
            Socket.writeByte(13);
            Socket.writeDouble(mainform.location[0]);
            Socket.writeDouble(mainform.location[1]);
            Socket.writeDouble(mainform.location[3]);
            Socket.writeDouble(mainform.location[2]);
            Socket.writeFloat(mainform.position[0]);
            Socket.writeFloat(mainform.position[1]);
            Socket.writeBool(mainform.onground);
        }

        void handle()
        {
            // Server is setting our location. Handle, then respond..polietly.

            double X = Socket.readDouble();
            double stance = Socket.readDouble();
            double Y = Socket.readDouble();
            double Z = Socket.readDouble();
            float yaw = Socket.readFloat();
            float pitch = Socket.readFloat();
            bool onground = Socket.readBool();

            mainform.location = new double[4] { X, Y -.5, Z, stance };
            mainform.position = new float[2] { yaw, pitch };
            mainform.onground = onground;

            PPaL response = new PPaL(true, Socket, mainform);
        }

    }
}
