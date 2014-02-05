using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CBOT {
    class settingsReader { // TODO: Make this better, eg. Add a ReadSetting() that allows for default values if it's not in the dict, instead of throwing errors on the server side
        string fileName = "";
        public Dictionary<string, string> settings;

        public settingsReader(string fName, bool create = false) {
            if (!File.Exists(fName) && create == false) 
                throw new FileNotFoundException("SettingsReader: Settings file not found");

            fileName = fName;
            
        }
        public void readSettings() {
            settings = new Dictionary<string, string>();

            StreamReader fileReader = new StreamReader(fileName);

            do {
                string line = fileReader.ReadLine();

                if (!line.Contains("="))
                    continue;

                string key = line.Substring(0, line.IndexOf(" "));
                string setting = line.Substring(line.IndexOf("=") + 2, line.Length - (line.IndexOf("=") + 2));

                settings.Add(key, setting);

            } while (!fileReader.EndOfStream);
            // -- Settings parsed.
            fileReader.Close();
            fileReader.Dispose();
        }
        public void saveSettings() {
            StreamWriter fileWriter = new StreamWriter(fileName);

            foreach (KeyValuePair<string, string> pair in settings) {
                fileWriter.Write(pair.Key + " = " + pair.Value + "\r\n");
            }

            fileWriter.Close();
            fileWriter.Dispose();
        }
    }
}
