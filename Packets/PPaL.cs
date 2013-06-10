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

            mainform.location = new double[4] { X, Y, Z, stance };
            mainform.position = new float[2] { yaw, pitch };
            mainform.onground = onground;

            PPaL response = new PPaL(true, Socket, mainform);
        }

    }
}
