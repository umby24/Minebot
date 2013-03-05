using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Classes
{
    public class MapBlock
    {
       public int ID;
       public string Name;
       public int x;
       public int y;
       public int z;

       public MapBlock(int id, int X, int Y, int Z)
       {
           ID = id;
           x = X;
           y = Y;
           z = Z;
           get_name();
       }
       void get_name()
       {
           //TODO: Do this.
       }
    }
}
