using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Web;

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

        public string[] newLogin(string username, string password) {
            string json = "{\"agent\": {\"name\": \"minecraft\",\"version\": 1},\"username\": \"" + username + "\",\"password\": \"" + password + "\"}";
            string accessToken = "";
            string profileID = "";

            HttpWebRequest wreq = (HttpWebRequest)WebRequest.Create("https://authserver.mojang.com/authenticate");

            wreq.Method = "POST";
            wreq.ContentType = "application/json";
            wreq.ContentLength = json.Length;

            using (Stream stream = wreq.GetRequestStream()) {
                stream.Write(Encoding.ASCII.GetBytes(json), 0, json.Length);
            }

            HttpWebResponse response = (HttpWebResponse)wreq.GetResponse();
            string code = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var root = JObject.Parse(code);

            foreach (KeyValuePair<string, JToken> app in root) {
                var appName = app.Key;
                
                switch (appName) {
                    case "accessToken":
                        accessToken = app.Value.ToString();
                        break;
                    case "selectedProfile":
                        profileID = app.Value.First.First.ToString();
                        break;
                }
            }

            return new string[] { accessToken, profileID };
        }
        public bool VerifyName(string username, string SessionID, string ServerHash)
        {

            WebClient request = new WebClient();
            String result = request.DownloadString("http://session.minecraft.net/game/joinserver.jsp?user=" + username + "&sessionId=" + HttpUtility.UrlEncode(SessionID) + "&serverId=" + ServerHash);

            if (result == "OK")
                return true;
            else
                return false;

        }
    }
}
