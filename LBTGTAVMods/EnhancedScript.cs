using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GTA;

namespace LBTGTAVMods
{
    public abstract class EnhancedScript : Script
    {
        private readonly string _logPath;

        public EnhancedScript(string scriptName)
        {
            _logPath = "scripts/LBTGTAVMods.log/";
            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);
            _logPath += scriptName + ".log";
            File.WriteAllText(_logPath, "Script Started ... \n");
        }
        public void WriteLog(string text)
        {
            File.AppendAllText(_logPath, text + "\n");
        }
    }
}
