using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace C_Minebot
{
    class RegistryControl
    {
        //This will allow for interops with VB minebot, and keep settings out of the range
        //of your average user.

        public object GetSetting(string App,string key,string value,object Def)
        {
            object thisKey = Registry.GetValue("HKEY_CURRENT_USER\\Software\\VB and VBA Program Settings\\" + App + "\\" + key, value, Def);

            if (thisKey == null)
                return Def;
            else
                return thisKey;
        }

        public void SaveSetting(string App, string key, string value, string content)
        {
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\VB and VBA Program Settings\\" + App + "\\" + key, value, content);
        }
    }
}
