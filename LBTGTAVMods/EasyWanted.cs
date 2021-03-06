using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.Math;

namespace LBTGTAVMods
{
    class EasyWanted : EnhancedScript
    {
        private readonly Random _random = new Random();

        public EasyWanted() : base("EasyWanted")
        {
            KeyDown += OnKeyDown;
            Tick += OnTick;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Multiply && Game.Player.WantedLevel > 0 && Game.Player.WantedLevel < 3)
            {
                Game.Player.Money -= Game.Player.WantedLevel * 10;
                Game.Player.WantedLevel = 0;
            }
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.WasCheatStringJustEntered("shift"))
            {
                Teleport();
            }
        }

        public void Teleport()
        {
            WriteLog("Teleporting...");

            Ped targetPed = GetRandomPed();
            if (targetPed == null)
                return;

            var pos1 = targetPed.Position;
            var pos2 = Game.Player.Character.Position;
            var vehicle1 = targetPed.CurrentVehicle;
            var seat = targetPed.SeatIndex;

            targetPed.Delete();

            if (vehicle1 != null)
                Game.Player.Character.SetIntoVehicle(vehicle1, seat);
            else
                Game.Player.Character.Position = pos1;

            ParticleEffectAsset asset = new ParticleEffectAsset("core");
            string effectName = "exp_grd_grenade_lod";
            World.CreateParticleEffectNonLooped(asset, effectName, pos1, scale: 1.5f);
            World.CreateParticleEffectNonLooped(asset, effectName, pos2, scale: 1.5f);
            Game.Player.Money -= 1000;
            World.CurrentTimeOfDay = new TimeSpan(_random.Next(24), _random.Next(60), _random.Next(60));
            Game.Player.WantedLevel = 0;
        }

        public Ped GetRandomPed()
        {
            Ped[] allPeds = World.GetAllPeds();
            List<Ped> filtered = new List<Ped>();
            foreach(Ped p in allPeds)
            {
                if (p.IsPlayer)
                    continue;
                if (p.IsDead)
                    continue;
                if (!p.IsHuman)
                    continue;
                if (p.Position.Z < 0)
                    continue;
                filtered.Add(p);
            }
            if (filtered.Count == 0)
                return null;
            int randomIdx = _random.Next(filtered.Count);
            return filtered[randomIdx];
        }
    }
}
