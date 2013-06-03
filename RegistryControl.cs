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
            try
            {
                RegistryKey regkey = Registry.CurrentUser.OpenSubKey("Software");
                regkey = regkey.OpenSubKey("VB and VBA Program Settings");
                regkey = regkey.OpenSubKey(App);
                regkey = regkey.OpenSubKey(key);

                if (regkey.GetValue(value) == null)
                { 
                    return Def; 
                }
                else { 
                    return regkey.GetValue(value); 
                }
            }
            catch
            {
                return Def;
            }

        }
        public void SaveSetting(string App, string key, string value, string content)
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("Software", true);
            regkey = regkey.OpenSubKey("VB and VBA Program Settings",true);
            regkey = regkey.OpenSubKey(App,true);
            regkey = regkey.OpenSubKey(key,true);
            regkey.SetValue(value, content);
        }
    }
}
