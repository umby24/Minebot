using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBOT.Classes {
    public interface Command {
        string command { get; }
        string plugin { get; }
        string help { get; }

        void run(string command, string[] args, string Text1, string sender, mainForm mf);
    }

    public struct Hold : Command {
        public string command { get { return "+hold"; } }
        public string plugin { get { return ""; } }
        public string help { get { return "Change the bot's held block."; } }

        public void run(string command, string[] args, string Text1, string sender, mainForm mf) {
            if (!HelpingFunctions.isNumeric(args[0]))
                return;

            if (short.Parse(args[0]) < 0 || short.Parse(args[0]) > 9)
                return;

            mf.MinecraftServer.ThisPlayer.selectedSlot = (byte)short.Parse(args[0]);
            libMC.NET.Packets.Play.ServerBound.HeldItemChange hc = new libMC.NET.Packets.Play.ServerBound.HeldItemChange(ref mf.MinecraftServer);
        }
    }

    public struct Say : Command {
        public string command { get { return "+say"; } }
        public string plugin { get { return ""; } }
        public string help { get { return "Makes the bot say your message."; } }

        public void run(string command, string[] args, string Text1, string sender, mainForm mf) {
            mf.Send_Message(Text1);
        }
    }

    public struct Follow : Command {
        public string command { get { return "+follow"; } }
        public string plugin { get { return ""; } }
        public string help { get { return "Follow a player. +follow [name]"; } }

        public void run(string command, string[] args, string Text1, string sender, mainForm mf) {
            if (mf.lstPlayers.Items.Contains(args[0])) {
                foreach (libMC.NET.Entities.Entity b in mf.MinecraftServer.MinecraftWorld.Entities) {
                    if (b.playerName == args[0]) {
                        mf.Follow_ID = b.Entity_ID;
                        mf.Following = true;
                        mf.MinecraftServer.ThisPlayer.location.x = b.Location.x;
                        mf.MinecraftServer.ThisPlayer.location.y = b.Location.y;
                        mf.MinecraftServer.ThisPlayer.location.z = b.Location.z;

                        var PlayerPosition = new libMC.NET.Packets.Play.ServerBound.PlayerPositionAndLook(ref mf.MinecraftServer);
                    }
                }
            }
        }
    }
    public struct LuaRun : Command {
        public string command { get { return "+luarun"; } }
        public string plugin { get { return ""; } }
        public string help { get { return "None"; } }


        public void run(string command, string[] args, string Text1, string sender, mainForm mf) {
            try {
                mf.luaHandler.luaHandler.DoString(Text1);
            } catch {
                mf.Send_Message("Error");
            }
        }
    }

    public struct GetBlock : Command {
        public string command { get { return "+getblock"; } }
        public string plugin { get { return "";  } }
        public string help { get { return "Simple command to get the block under your feet."; } }

        public void run(string command, string[] args, string Text1, string sender, mainForm mf) {
            int blockX = (int)Math.Floor(mf.MinecraftServer.ThisPlayer.location.x);
            int blockY = (int)mf.MinecraftServer.ThisPlayer.location.y - 2;
            int blockZ = (int)mf.MinecraftServer.ThisPlayer.location.z;

            int ChunkX = (int)Math.Floor(decimal.Divide(blockX, 16));
            int ChunkZ = (int)Math.Floor(decimal.Divide(blockZ, 16));

            libMC.NET.World.Chunk thisChunk = mf.MinecraftServer.MinecraftWorld.worldChunks[mf.MinecraftServer.MinecraftWorld.GetChunk(ChunkX, ChunkZ)];
            libMC.NET.Entities.Block thisBlock = thisChunk.GetBlock(blockX, blockY, blockZ);

            if (thisBlock != null) {
                mf.Send_Message("Block is " + thisBlock.Name + ". Metadata is " + thisChunk.GetBlockMetadata(blockX, blockY, blockZ).ToString());
            }
        }
    }

    public struct ScriptedCommand : Command {
        public ScriptedCommand(string Ccommand, string Pplugin, string Hhelp) {
            _Command = Ccommand;
            _plugin = Pplugin;
            _help = Hhelp;
        }

        private string _Command, _plugin, _help;

        public string command { get { return _Command; } }
        public string plugin { get { return _plugin; } }
        public string help { get { return _help; } }


        public void run(string command, string[] args, string Text1, string sender, mainForm mf) {
            if (plugin.StartsWith("Lua:"))
                mf.luaHandler.Run_Lua_Function(plugin.Substring(4, plugin.Length - 4), new Object[] { command, args, Text1, sender, mf });
            else
                Activator.CreateInstance(Type.GetType(plugin), command, args, Text1, sender);
        }
    }

}
