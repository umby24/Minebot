using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;

namespace C_Minebot.Packets
{
    class MultiBlockChange
    {
        public MultiBlockChange(Wrapped.Wrapped socket, Form1 mainform)
        {
            int chunkX = socket.readInt();
            int chunkZ = socket.readInt();
            int blocks = socket.readShort();
            int datasize = socket.readInt();
            byte[] data = socket.readByteArray(datasize);
            Chunk thischunk = null;

            foreach (Chunk b in mainform.Chunks) {
                if (b.x == chunkX & b.z == chunkZ) {
                    thischunk = b;
                    break;
                }
            }

            if (thischunk == null)
                throw new Exception("Attempted to access an uninitilized chunk.");

            // The below parsing method is thanks to redstone_bot, translated from ruby to C# by myself.

            for (int i = 0; i < blocks - 1; i++) {
                byte[] blockData = new byte[4];
                Array.Copy(data, (i * 4), blockData, 0, 4);

                int z = (blockData[0] & 0x0F);
                int x = (blockData[0] >> 4) & 0x0F;
                int y = (blockData[1]);
                int blockId = (blockData[2] << 4) | ((blockData[3] & 0xF0) >> 4);
                int metaData = blockData[3] & 0xF;

                // X,Z Are rel. to Chunk, so convert to chunk coords.. (also, this part down is my own code again)
                x = (chunkX * 16) + x;
                z = (chunkZ * 16) + z;

                thischunk.updateBlock(x, y, z, blockId);
            }
        }
    }
}
