using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

using NLua;

namespace CBOT.Classes {
    public class LuaWrapper {
        public Lua luaHandler;
        mainForm MainForm;
        Dictionary<string, DateTime> Scripts;

        public LuaWrapper(mainForm mf) {
            luaHandler = new Lua();

            Scripts = new Dictionary<string, DateTime>();

            if (Directory.Exists("Lua") == false)
                Directory.CreateDirectory("Lua");

            // -- Register the functions we are exposing to Lua
            luaHandler.RegisterFunction("AddCommand", mf, mf.GetType().GetMethod("AddCommand"));
            luaHandler.RegisterFunction("SendChat", mf, mf.GetType().GetMethod("Send_Message"));
            luaHandler.RegisterFunction("puts", mf, mf.GetType().GetMethod("puts"));

            MainForm = mf;
        }
        public void Lua_Scripts_Load() {
            Scripts = new Dictionary<string, DateTime>();

            if (Directory.Exists("Lua") == false)
                Directory.CreateDirectory("Lua");

            string[] files = Directory.GetFiles("Lua", "*.lua", SearchOption.AllDirectories);

            foreach (string file in files) {
                Scripts.Add(file, File.GetLastWriteTime(file));

                luaHandler.DoFile(file);
            }

            MainForm.puts("Lua loaded");
        }

        public void Run_Lua_Function(string function, object[] args) {
            LuaFunction b = luaHandler.GetFunction(function);

            try {
                if (b != null && args != null)
                    b.Call(args);
                else if (b != null)
                    b.Call();
            } catch (NLua.Exceptions.LuaScriptException e) {
                MainForm.puts("Lua error: " + e.Message);
                MainForm.puts("---------------------");
                MainForm.puts(e.StackTrace.ToString());
                MainForm.puts("---------------------");
            }
        }

        public void Lua_Main() {
            while (true) {
                string[] files = Directory.GetFiles("Lua", "*.lua", SearchOption.AllDirectories);

                foreach (string file in files) {
                    if (Scripts.Keys.Contains(file)) {
                        if (File.GetLastWriteTime(file) != Scripts[file]) {
                            try {
                                luaHandler.DoFile(file);
                                Scripts[file] = File.GetLastWriteTime(file);
                            } catch (NLua.Exceptions.LuaScriptException e) {
                                MainForm.puts("Lua error: " + e.Message);
                                MainForm.puts("---------------------");
                                MainForm.puts(e.StackTrace.ToString());
                                MainForm.puts("---------------------");
                            }
                        }
                    } else {
                        try {
                            Scripts.Add(file, File.GetLastWriteTime(file));
                            luaHandler.DoFile(file);
                        } catch (NLua.Exceptions.LuaScriptException e) {
                            MainForm.puts("Lua error: " + e.Message);
                            MainForm.puts("---------------------");
                            MainForm.puts(e.StackTrace.ToString());
                            MainForm.puts("---------------------");
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
