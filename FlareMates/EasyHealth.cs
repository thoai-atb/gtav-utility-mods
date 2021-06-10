using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GTA;
using GTA.Math;

namespace LBTGTAVMods
{
    class EasyHealth : EnhancedScript
    {
        private readonly Random _random = new Random();
        private readonly double _healingPerTick = 0.01;

        public EasyHealth() : base("EasyHealth")
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if(_random.NextDouble() < _healingPerTick)
            {
                Game.Player.Character.Health++;
            }

            if(_random.NextDouble() < _healingPerTick && Game.Player.Character.Armor > 0)
            {
                Game.Player.Character.Armor++;
            }
        }
    }
}
