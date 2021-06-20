using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace LBTGTAVMods
{
    class QuickCheats : EnhancedScript
    {
        public QuickCheats() : base("QuickCheats")
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.WasCheatStringJustEntered("ineedcash"))
                Game.Player.Money += 100000;
            if (Game.WasCheatStringJustEntered("armor"))
                Game.Player.Character.Armor = 200;
            if (Game.WasCheatStringJustEntered("molotov"))
                Game.Player.Character.Weapons.Give(WeaponHash.Molotov, 25, true, true);
            if (Game.WasCheatStringJustEntered("ride"))
            {
                Vehicle nearest = World.GetClosestVehicle(Game.Player.Character.Position, 5);
                if(nearest != null)
                {
                    Game.Player.Character.Task.EnterVehicle(nearest, VehicleSeat.Passenger);
                }
            }
            if (Game.WasCheatStringJustEntered("repair"))
                if(Game.Player.Character.CurrentVehicle != null)
                {
                    Game.Player.Character.CurrentVehicle.Repair();
                }
        }
    }
}
