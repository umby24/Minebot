using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using Wrapped;
using System.Threading;

namespace C_Minebot
{
    public class networkHandler
    {
        public string ip;
        public int port;
        public Form1 myform;
        TcpClient baseSock;
        NetworkStream baseStream;
        public Wrapped.Wrapped socket;
        public bool logging = false;

        public networkHandler(string nip, string nport, Form1 asdf) {
            ip = nip;
            port = Convert.ToInt32(nport);
            myform = asdf;
        }

        public void start()
        {
            try
            {
                baseSock = new TcpClient(ip, port);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                MessageBox.Show("Error connecting: " + e.Message);
                return;
            }
            baseStream = baseSock.GetStream();
            socket = new Wrapped.Wrapped(baseStream);
            //Connect.. 
            myform.puts("Connected to minecraft server.");
            Handshake handshake = new Handshake(true, socket,myform);
            myform.puts("Handshake sent!");
            //Begin handling packets (Seperate thread to prevent bottlenecks)
            Thread handle = new Thread(handlePackets);
            handle.Start();
            myform.puts("Networking thread started.");
        }

        void handlePackets()
        {
            while (baseSock.Connected == true)
            {
                if (baseStream.DataAvailable == true)
                {
                    int id = (int)socket.readByte();
                    packetHandler ph = new packetHandler(id, socket, myform,logging);
                }
            }
            baseSock.Close();
            myform.puts("Disconnected from server.");
        }
    }
}
