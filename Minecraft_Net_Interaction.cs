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
        public string Login(string username,string password)
        {
            WebRequest request = WebRequest.Create("https://login.minecraft.net/");
            request.Method = "POST";
            Byte[] byteArray = Encoding.UTF8.GetBytes("user=" + username + "&password=" + password + "&version=1337");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string ServResponse = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
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
            String result = request.DownloadString("http://session.minecraft.net/game/joinserver.ksp?user=" + username + "&sessionId=" + SessionID + "&serverId=" + ServerHash);
            
            if (result == "OK")
            {return true;} else {return false;}

        }

    }
}
