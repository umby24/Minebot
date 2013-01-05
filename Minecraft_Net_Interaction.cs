using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace C_Minebot
{
    class Minecraft_Net_Interaction
    {
        public string Login(string username, string password)
        {
            WebClient request = new WebClient();
            String ServResponse = request.DownloadString("https://login.minecraft.net/?user=" + username + "&password=" + password + "&version=1337");

            switch (ServResponse)
            {
                case "Old version":
                    return "Old launcher version; contact dev.";
                case "Bad login":
                    return "Incorrect username or password.";
                default:
                    return ServResponse;
            }
        }

        public bool VerifyName(string username,string SessionID,string ServerHash)
        {
            WebClient request = new WebClient();
            String result = request.DownloadString("http://session.minecraft.net/game/joinserver.jsp?user=" + username + "&sessionId=" + SessionID + "&serverId=" + ServerHash);
            
            if (result == "OK")
            {return true;} else {return false;}

        }

        public void readMetadata(Wrapped.Wrapped socket)
        {
            do
            {
                byte item = socket.readByte();
                if (item == 127) break;
                int index = item & 0x1F;
                int type = item >> 5;

                switch (type)
                {
                    case 0:
                        socket.readByte();
                        break;
                    case 1:
                        socket.readShort();
                        break;
                    case 2:
                        socket.readInt();
                        break;
                    case 3:
                        socket.readFloat();
                        break;
                    case 4:
                        socket.readString();
                        break;
                    case 5:
                        readSlot(socket);
                        break;
                    case 6:
                        socket.readInt();
                        socket.readInt();
                        socket.readInt();
                        break;

                }
            } while (true);
        }

        public void readSlot(Wrapped.Wrapped socket)
        {
            int blockID = socket.readShort();

            if (blockID == -1)
                return;

            socket.readByte();
            socket.readShort();
            int NBTLength = socket.readShort();

            if (NBTLength == -1)
                return;

            socket.readByteArray(NBTLength);
        }
    }
}
