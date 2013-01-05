using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Playerdigging
    {
        // 0 - Start digging
        // 2 - Finish digging
        // 3 - drop item stack
        // 4 - drop item
        // 5 - shoot arrow / finish eating
        // Face:
        // 0 - -y
        // 1 - +y
        // 2 - -z
        // 3 - +z
        // 4 - -x
        // 5 - +x

        public Playerdigging(Wrapped.Wrapped socket, Form1 mainform, int option, int x, int y, int z,byte face)
        {
            socket.writeByte(14);
            socket.writeByte((byte)option);
            socket.writeInt(x);
            socket.writeByte((byte)y);
            socket.writeInt(z);
            socket.writeByte(face);
        }
        public Playerdigging(Wrapped.Wrapped socket, Form1 mainform, int option)
        {
            switch (option)
            {
                case 3:
                    socket.writeByte(14);
                    socket.writeByte((byte)option);
                    socket.writeInt(0);
                    socket.writeByte(0);
                    socket.writeInt(0);
                    socket.writeByte(0);
                    break;
                case 4:
                    socket.writeByte(14);
                    socket.writeByte((byte)option);
                    socket.writeInt(0);
                    socket.writeByte(0);
                    socket.writeInt(0);
                    socket.writeByte(0);
                    break;
                case 5:
                    socket.writeByte(14);
                    socket.writeByte((byte)option);
                    socket.writeInt(0);
                    socket.writeByte(0);
                    socket.writeInt(0);
                    socket.writeByte(255);
                    break;
                default:
                    break;
            }

        }
    }
}
