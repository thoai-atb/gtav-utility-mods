using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;

namespace LBTGTAVMods
{
    class EasyVehicle : EnhancedScript
    {
        private readonly Random random = new Random();

        public EasyVehicle () : base("EasyVehicle")
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.WasCheatStringJustEntered("ride"))
            {
                Vehicle nearest = World.GetClosestVehicle(Game.Player.Character.Position, 5);
                if (nearest != null)
                {
                    Game.Player.Character.Task.EnterVehicle(nearest, VehicleSeat.Passenger);
                }
            }
            if (Game.WasCheatStringJustEntered("repair"))
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    Game.Player.Character.CurrentVehicle.Repair();
                }
            }
            if (Game.WasCheatStringJustEntered("off"))
            {
                WriteLog("Turn radio off");
                var vehicle = Game.Player.Character.CurrentVehicle;
                if (vehicle != null)
                    vehicle.IsRadioEnabled = false;
            }
            if (Game.WasCheatStringJustEntered("hem"))
            {
                Game.Player.Character.RemoveHelmet(false);
            }
            if (Game.WasCheatStringJustEntered("wonder"))
            {
                // Game.Player.Character.Money -= 100; -> This line doesn't work
                Array allHashes = Enum.GetValues(typeof(VehicleHash));
                VehicleHash type = (VehicleHash)allHashes.GetValue(random.Next(allHashes.Length));
                var vehicle = World.CreateVehicle(type, Game.Player.Character.Position.Around(5));
                vehicle.MarkAsNoLongerNeeded();
            }
            if (Game.WasCheatStringJustEntered("drive"))
            {
                Vehicle vehicle = World.GetClosestVehicle(Game.Player.Character.Position, 5);
                if (vehicle != null)
                {
                    Ped p = Utility.GetNearestPed();
                    if (p != null)
                    {
                        p.Task.EnterVehicle(vehicle, VehicleSeat.Driver);
                    }
                }
            }
        }
    }
}
