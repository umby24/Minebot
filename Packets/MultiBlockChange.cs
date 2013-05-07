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

            for (int i = 0; i < blocks - 1; i++) {
              //  Array.Reverse(data);
                int blockdata = BitConverter.ToInt32(data, (i * 4)); // Convert to int (4 bytes) for bitwise operations.
                int blockid = (int)(blockdata & 0x0000fff0) >> 8;
                int y = (blockdata & 0x00ff0000) >> 16;
                int z = (blockdata & 0x0f000000) >> 24;
                int x = (int)(blockdata & 0xf0000000) >> 28;

                y = y * 16;
                z = (chunkZ * 16) + z;
                x = (chunkX * 16) + x;

                if (y != 64)
                  throw new Exception("Whatup");

            }
        }
    }
}
