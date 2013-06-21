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
using C_Minebot.Packets;
using System.Runtime.Serialization;

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
        public Thread handler;
        public bool started = false;
        Dictionary<int, Type> packetTypes = new Dictionary<int, Type>();

        #region initiators
                public networkHandler(string nip, string nport, Form1 asdf) {
                    ip = nip;
                    port = Convert.ToInt32(nport);
                    myform = asdf;
                    populate();
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

                    started = true;
                    baseStream = baseSock.GetStream();
                    socket = new Wrapped.Wrapped(baseStream);

                    myform.puts("Connected to minecraft server.");
                    Handshake handshake = new Handshake(socket,myform, true);

                    // Begin handling packets (Seperate thread to prevent bottlenecks)
                    Thread handle = new Thread(handlePackets);
                    handle.Start();
                    handler = handle;

                }

                public void stop()
                {
                    handler.Abort();
                    baseStream.Close();
                    baseSock.Close();
                }
        #endregion

        #region Actual Handling
        public void handlePackets() {
            try {
                int id = 9999;
                while ((id = (int)socket.readByte()) != 9999) {
                    if (baseSock.Connected == true) {
                        if (id == 2 || id == 3 || id == 13 || id == 16 || id == 18 || id == 101 || id == 252 || id == 253 || id == 255) {
                            var packet = Activator.CreateInstance(Type.GetType(packetTypes[id].ToString()), new Object[] { socket, myform, false }); // Tried a few ways around having to do this if-statement. No go on any of them.
                        } else {
                            var packet = Activator.CreateInstance(Type.GetType(packetTypes[id].ToString()), new Object[] { socket, myform }); // New and improved handling.
                        }
                    } else {
                        baseSock.Close();
                        myform.puts("Disconnected from server.");
                        handler.Abort();
                    }
                }
            }
            catch {
                baseSock.Close();
                myform.puts("Disconnected from server.");
                handler.Abort();
            }
        }

        
        void populate() {
            packetTypes.Add(0, typeof(keepAlive));
            packetTypes.Add(1, typeof(Login_Response));
            packetTypes.Add(2, typeof(Handshake));
            packetTypes.Add(3, typeof(chatMessage));
            packetTypes.Add(4, typeof(timeUpdate));
            packetTypes.Add(5, typeof(entityEquipment));
            packetTypes.Add(6, typeof(spawnPosition));
            packetTypes.Add(8, typeof(updateHealth));
            packetTypes.Add(9, typeof(Respawn));
            packetTypes.Add(13, typeof(PPaL));
            packetTypes.Add(16, typeof(HeldItemChange));
            packetTypes.Add(17, typeof(useBed));
            packetTypes.Add(18, typeof(Animation));
            packetTypes.Add(20, typeof(SpawnNamedEntity));
            packetTypes.Add(22, typeof(CollectItem));
            packetTypes.Add(23, typeof(SpawnObj));
            packetTypes.Add(24, typeof(SpawnMob));
            packetTypes.Add(25, typeof(SpawnPainting));
            packetTypes.Add(26, typeof(SpawnExporb));
            packetTypes.Add(28, typeof(EntVelocity));
            packetTypes.Add(29, typeof(DestroyEnt));
            packetTypes.Add(30, typeof(Entity));
            packetTypes.Add(31, typeof(EntRelmove));
            packetTypes.Add(32, typeof(Entlook));
            packetTypes.Add(33, typeof(EntlookRelmove));
            packetTypes.Add(34, typeof(EntTeleport));
            packetTypes.Add(35, typeof(Enthead));
            packetTypes.Add(38, typeof(EntStatus));
            packetTypes.Add(39, typeof(AttachEnt));
            packetTypes.Add(40, typeof(EntMetadata));
            packetTypes.Add(41, typeof(EntEffect));
            packetTypes.Add(42, typeof(RemoveEntEffect));
            packetTypes.Add(43, typeof(SetExp));
            packetTypes.Add(51, typeof(ChunkData));
            packetTypes.Add(52, typeof(MultiBlockChange));
            packetTypes.Add(53, typeof(BlockChange));
            packetTypes.Add(54, typeof(BlockAction));
            packetTypes.Add(55, typeof(BlockbreakAni));
            packetTypes.Add(56, typeof(BulkChunks));
            packetTypes.Add(60, typeof(Explosion));
            packetTypes.Add(61, typeof(Sound));
            packetTypes.Add(62, typeof(Namedsound));
            packetTypes.Add(63, typeof(Particle));
            packetTypes.Add(70, typeof(changeGameState));
            packetTypes.Add(71, typeof(SpawnGlobalEntity));
            packetTypes.Add(100, typeof(OpenWindow));
            packetTypes.Add(101, typeof(CloseWindow));
            packetTypes.Add(103, typeof(SetSlot));
            packetTypes.Add(104, typeof(setWindowItems));
            packetTypes.Add(105, typeof(updateWindowProperty));
            packetTypes.Add(106, typeof(ConfirmTransaction));
            packetTypes.Add(130, typeof(updateSign));
            packetTypes.Add(131, typeof(itemData));
            packetTypes.Add(132, typeof(updateTileEntity));
            packetTypes.Add(200, typeof(incStat));
            packetTypes.Add(201, typeof(PlayerListItem));
            packetTypes.Add(202, typeof(PlayerAbilities));
            packetTypes.Add(203, typeof(TabComplete));
            packetTypes.Add(206, typeof(scoreboardObjective));
            packetTypes.Add(207, typeof(updateScore));
            packetTypes.Add(208, typeof(displayScoreboard));
            packetTypes.Add(209, typeof(teams));
            packetTypes.Add(250, typeof(PluginMessage));
            packetTypes.Add(252, typeof(EncResponse));
            packetTypes.Add(253, typeof(EncRequest));
            packetTypes.Add(255, typeof(Kick));
        }
        #endregion
    }
}
