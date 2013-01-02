using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Packets;

namespace C_Minebot
{
    class packetHandler
    {
        Wrapped.Wrapped sock;
        Form1 mainform;
        bool logg;
        public packetHandler(int id,Wrapped.Wrapped socket,Form1 mform,bool log)
        {
            sock = socket;
            mainform = mform;
            logg = log;
            Handle(id);
        }
        void Handle(int id)
        {
            if (logg)
            {
                mainform.puts("p" + Convert.ToString(id));
            }
            switch (id)
            {
                case 0:
                    mainform.puts("Need to parse keepalive.");
                    break;
                case 1:
                    Login_Response response = new Login_Response(sock, mainform);
                    break;
                case 2:
                    Handshake myhand = new Handshake(false, sock, mainform);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 13:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;
                case 23:
                    break;
                case 24:
                    break;
                case 25:
                    break;
                case 26:
                    break;
                case 28:
                    break;
                case 29:
                    break;
                case 30:
                    break;
                case 31:
                    break;
                case 32:
                    break;
                case 33:
                    break;
                case 34:
                    break;
                case 35:
                    break;
                case 38:
                    break;
                case 39:
                    break;
                case 40:
                    break;
                case 41:
                    break;
                case 42:
                    break;
                case 43:
                    break;
                case 51:
                    break;
                case 52:
                    break;
                case 53:
                case 54:
                case 55:
                case 56:
                case 60:
                case 61:
                case 62:
                case 70:
                case 71:
                case 100:
                case 101:
                case 103:
                case 104:
                case 105:
                case 106:
                case 107:
                case 130:
                case 131:
                case 132:
                case 200:
                case 201:
                case 202:
                case 203:
                case 250:
                case 252:
                    EncResponse Response = new EncResponse(false, sock, mainform);
                    break;
                case 253:
                    EncRequest Request = new EncRequest(false, sock, mainform);
                    break;
                case 255:
                    Kick kicked = new Kick(false, sock, mainform);
                    break;
                default:
                    mainform.puts("new packet found! " + Convert.ToString(id));
                    break;
            }
        }

    }
}
