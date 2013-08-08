using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace C_Minebot.Classes {
    class Importer {
        Form1 mainform;
        string Name;
        List<string> blocks = new List<string>();
        int x;
        int y;
        int z;

        public Importer(Form1 MainForm, string name, int sx, int sy, int sz) {
            mainform = MainForm;
            Name = name;
            x = sx;
            y = sy;
            z = sz;
        }
        public void import() {
            if (!File.Exists("imports\\" + Name + ".Cbot"))
                return;

            Packets.chatMessage cm = new Packets.chatMessage(mainform.nh.socket, mainform, "Importing...", true);
            StreamReader fileReader = new StreamReader("imports\\" + Name + ".Cbot");
            string temp = "";
            functions help = new functions();
            int mx;
            int myy;
            int myz;
            int mtype;
            blocks.Clear();

            while (fileReader.EndOfStream == false) {
                blocks.Add(fileReader.ReadLine());
            }
            fileReader.Close();

            cm = new Packets.chatMessage(mainform.nh.socket, mainform, "File Loaded!", true);

            for (int i = 0; i < blocks.Count; i++) {
                temp = blocks[i];

                mx = int.Parse(temp.Substring(0, temp.IndexOf(",")));
                temp = temp.Substring(temp.IndexOf(",") + 1, temp.Length - (temp.IndexOf(",") + 1));
                myz = int.Parse(temp.Substring(0, temp.IndexOf(",")));
                temp = temp.Substring(temp.IndexOf(",") + 1, temp.Length - (temp.IndexOf(",") + 1));
                myy = int.Parse(temp.Substring(0, temp.IndexOf(",")));
                temp = temp.Substring(temp.IndexOf(",") + 1, temp.Length - (temp.IndexOf(",") + 1));
                mtype = int.Parse(temp.Substring(0,temp.IndexOf(":")));

                if (mtype != 0) {
                    help.moveTo(myz + x, myy + y + 1, mx + z, mainform);
                   
                    //mainform.location[0] = myz + x;
                    //mainform.location[1] = (myy + y) + 1;
                    //mainform.location[2] = mx + z;
                    //mainform.location[3] = mainform.location[1] + 1;
                    Packets.PlayerPosition pp = new Packets.PlayerPosition(mainform.nh.socket, mainform);
                    Packets.creativeInventory ci = new Packets.creativeInventory(mainform.nh.socket,mainform,40,ctos(mtype));
                   // System.Threading.Thread.Sleep(100);
                    Packets.placeBlock myblock = new Packets.placeBlock(myz + x, (byte)(myy + y), mx + z, 3, ctos(mtype), mainform.nh.socket);
                }

            }

        }
        Classes.Item ctos(int blockID) {
            // Convert Creative block ID to Survival Block ID

            switch (blockID) {
                case 21:
                    return new Item(35,1,14,0);
                case 22:
                    return new Item(35,1,1,0);
                case 23:
                     return new Item(35,1,4,0);
                case 24:
                     return new Item(35,1,5,0);
                case 25:
                     return new Item(35,1,13,0);
                case 26:
                     return new Item(35,1,3,0);
                case 27:
                     return new Item(35,1,9,0);
                case 28:
                     return new Item(35,1,11,0);
                case 29:
                      return new Item(35,1,10,0);
                case 30:
                      return new Item(35,1,12,0);
                case 31:
                      return new Item(35,1,8,0);
                case 32:
                      return new Item(35,1,2,0);
                case 33:
                      return new Item(35,1,6,0);
                case 34:
                      return new Item(35,1,15,0);
                case 35:
                      return new Item(35,1,7,0);
                case 36:
                      return new Item(35,1,0,0);
                   
            }
            return new Item(blockID,1,0,0);
        }
    }
}
