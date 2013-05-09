using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C_Minebot.Classes;

namespace C_Minebot.Packets
{
    class Explosion
    {
        public Explosion(Wrapped.Wrapped socket, Form1 mainform)
        {
            mainform.puts("Explosion");
            double ex = socket.readDouble();
            double ey = socket.readDouble();
            double ez = socket.readDouble();
            socket.readFloat();
            int count = socket.readInt();

            while (count != 0)
            {
                int x = (int)socket.readByte();
                int y = (int)socket.readByte();
                int z = (int)socket.readByte();

                // Convert to Absolute coords

                x = (int)ex + x;
                y = (int)ey + y;
                z = (int)ez + z;

                decimal ChunkX = decimal.Divide(x, 16);
                decimal ChunkZ = decimal.Divide(z, 16);

                ChunkX = Math.Floor(ChunkX);
                ChunkZ = Math.Floor(ChunkZ);

                Chunk thisChunk = null;

                foreach (Classes.Chunk b in mainform.Chunks) {
                    if (b.x == ChunkX & b.z == ChunkZ) {
                        thisChunk = b;
                        break;
                    }
                }

                if (thisChunk != null)
                   thisChunk.updateBlock(x, y, z, 0);

                count--;
            }

            socket.readFloat();
            socket.readFloat();
            socket.readFloat();
        }
    }
}
