using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class Login_Response
    {

        Wrapped.Wrapped sock;
        Form1 myform;

        public Login_Response(Wrapped.Wrapped socket, Form1 asdf)
        {
            sock = socket;
            myform = asdf;
            handle();
        }

        void handle()
        {
            int entityID = sock.readInt();
            string levelType = sock.readString();
            sbyte gameMode = sock.readSByte();
            sbyte Dimension = sock.readSByte();
            sbyte Difficulty = sock.readSByte();
            sbyte Blank = sock.readSByte();
            sbyte maxPlayers = sock.readSByte();

            myform.puts("Logged in to server.");
            myform.puts("Server max players is " + Convert.ToString(maxPlayers));

            if (Dimension == -1)
                myform.puts("You are in the nether!");

            if (Dimension == 1)
                myform.puts("You are in the end!");

            switch (Difficulty)
            {
                case 0:
                    myform.puts("Server difficulty: Peaceful");
                    break;
                case 1:
                    myform.puts("Server difficulty: Easy");
                    break;
                case 2:
                    myform.puts("Server difficulty: Normal");
                    break;
                case 3:
                    myform.puts("Server difficulty: Hard");
                    break;
                default:
                    break;
            }

            myform.EntityID = entityID;
        }

    }
}
