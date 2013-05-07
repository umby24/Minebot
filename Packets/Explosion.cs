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
                int x = ((int)ex) + (int)socket.readByte();
                int y = ((int)ey) + (int)socket.readByte();
                int z = ((int)ez) + (int)socket.readByte();

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

                thisChunk.updateBlock(x, y, z, 0);
                count--;
            }

            socket.readFloat();
            socket.readFloat();
            socket.readFloat();
        }
    }
}
