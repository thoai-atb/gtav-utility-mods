using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;

namespace LBTGTAVMods
{
    class QuickCheats : EnhancedScript
    {

        Random random = new Random();
        public QuickCheats() : base("QuickCheats")
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.WasCheatStringJustEntered("gimmecash"))
                Game.Player.Money += 100000;
            if (Game.WasCheatStringJustEntered("armor"))
                Game.Player.Character.Armor = 200;
            if (Game.WasCheatStringJustEntered("molotov"))
                Game.Player.Character.Weapons.Give(WeaponHash.Molotov, 25, true, true);
            if (Game.WasCheatStringJustEntered("para"))
                Game.Player.Character.Weapons.Give(WeaponHash.Parachute, 1, true, true);
            if (Game.WasCheatStringJustEntered("populate"))
                Populate();
        }

        public void Populate()
        {
            var pos = Game.Player.Character.Position;
            for(int i = 0; i<20; i++)
            {
                var ped = World.CreateRandomPed(pos.Around(10));
                ped.MarkAsNoLongerNeeded();
            }
        }
    }
}
