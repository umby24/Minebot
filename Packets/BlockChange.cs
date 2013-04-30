using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class BlockChange
    {
        public BlockChange(Wrapped.Wrapped socket, Form1 Mainform)
        {
            int chunkX, chunkZ;

            int x = socket.readInt();
            byte y = socket.readByte();
            int z = socket.readInt();
            short blockID = socket.readShort();
            socket.readByte();

            chunkX = (int)Math.Floor(decimal.Divide(x, 16));
            chunkZ = (int)Math.Floor(decimal.Divide(z, 16));

            Classes.Chunk thisChunk = null;
            Classes.MapBlock thisblock = null;

            foreach (Classes.Chunk b in Mainform.Chunks) {
                if (b.x == chunkX & b.z == chunkZ) {
                    thisChunk = b;
                    break;
                }
            }

            foreach (Classes.MapBlock b in thisChunk.tBlocks) {
                if (b.x == x & b.y == y & b.z == z) {
                    thisblock = b;
                    break;
                }
            }

            if (thisblock == null) 
                thisChunk.tBlocks.Add(new Classes.MapBlock((int)blockID, x, (int)y, z, chunkX, chunkZ));
             else 
                thisChunk.tBlocks[thisChunk.tBlocks.IndexOf(thisblock)] = new Classes.MapBlock((int)blockID, x, (int)y, z, chunkX, chunkZ); 

        }

    }
}
