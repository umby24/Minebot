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

        public bool VerifyName(string username, string SessionID, string ServerHash)
        {

            WebClient request = new WebClient();
            String result = request.DownloadString("http://session.minecraft.net/game/joinserver.jsp?user=" + username + "&sessionId=" + SessionID + "&serverId=" + ServerHash);

            if (result == "OK")
                return true;
            else
                return false;

        }
    }
}
