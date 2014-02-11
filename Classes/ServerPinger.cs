using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

using libMC.NET.Client;
using libMC.NET.Network;

namespace CBOT.Classes {
    class ServerPinger {
        public MinecraftClient SP;

        public string serverName;
        public string[] PingResponse;
        public string[] Players;
        public int msPing;

        public Image favicon;

        public ServerPinger(string ip, int port,string server) {
            serverName = server;

            PingResponse = new string[6];
            SP = new MinecraftClient(ip, port, "Minebot", "None", false);
            
        }

        void SP_PingResponseReceived(string VersionName, int ProtocolVersion, int MaxPlayers, int OnlinePlayers, string[] PlayersSample, string MOTD, Image Favicon) {
            PingResponse[0] = OnlinePlayers.ToString();
            PingResponse[1] = MaxPlayers.ToString();
            PingResponse[2] = MOTD;
            PingResponse[3] = ProtocolVersion.ToString();
            PingResponse[4] = VersionName.ToString();


            if (PlayersSample != null) {
                Players = PlayersSample.ToArray();
            }

            if (Favicon != null) {
                favicon = Favicon;
            }
        }

        void SP_MsPingReceived(int smsPing) {
            msPing = smsPing;

            if (PingComplete != null)
                PingComplete(this);

            SP.Disconnect();
        }
        
        public void ping() {
            SP.ServerState = 1; // -- Set to ping state
            SP.MsPingReceived += SP_MsPingReceived;
            SP.PingResponseReceived += SP_PingResponseReceived;
            SP.ErrorMessage += SP_errorMessage;
            SP.Connect();
        }

        void SP_errorMessage(object sender, string message) {
            PingResponse[0] = "0";
            PingResponse[1] = "0";
            PingResponse[2] = "0";
            PingResponse[3] = "0";
            PingResponse[4] = "Error " + message;
            msPing = 9001;

            if (PingComplete != null)
                PingComplete(this);

            SP.Disconnect();
        }

        public delegate void PingCompleteHandler(object Pinger);
        public event PingCompleteHandler PingComplete;
    }

}
