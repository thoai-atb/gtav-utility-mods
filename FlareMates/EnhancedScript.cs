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
            _logPath = "scripts/" + scriptName + ".log";
            File.WriteAllText(_logPath, "Script Started ... \n");
        }
        public void WriteDebug(string text)
        {
            File.AppendAllText(_logPath, text + "\n");
        }
    }
}
