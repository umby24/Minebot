using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Minebot.Packets
{
    class PlayerListItem
    {
        public PlayerListItem(Wrapped.Wrapped socket, Form1 mainform)
        {
            string name = socket.readString();
            bool online = socket.readBool();
            socket.readShort();

            if (online)
            {
                mainform.add(strip_codes(name));
            }
            else
            {
                    mainform.remove(strip_codes(name));
            }
        }

        private string strip_codes(string text)
        {

            // Strips the color codes from text.
            string smessage = text;
            if (smessage.Contains("§"))
            {
                smessage = smessage.Replace("§0", "");
                smessage = smessage.Replace("§1", "");
                smessage = smessage.Replace("§2", "");
                smessage = smessage.Replace("§3", "");
                smessage = smessage.Replace("§4", "");
                smessage = smessage.Replace("§5", "");
                smessage = smessage.Replace("§6", "");
                smessage = smessage.Replace("§7", "");
                smessage = smessage.Replace("§8", "");
                smessage = smessage.Replace("§9", "");
                smessage = smessage.Replace("§a", "");
                smessage = smessage.Replace("§b", "");
                smessage = smessage.Replace("§c", "");
                smessage = smessage.Replace("§d", "");
                smessage = smessage.Replace("§e", "");
                smessage = smessage.Replace("§f", "");
                smessage = smessage.Replace("§A", "");
                smessage = smessage.Replace("§B", "");
                smessage = smessage.Replace("§C", "");
                smessage = smessage.Replace("§D", "");
                smessage = smessage.Replace("§E", "");
                smessage = smessage.Replace("§F", "");
            }
            return smessage;
        }
    }
}
