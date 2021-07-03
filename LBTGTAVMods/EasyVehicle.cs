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
    class EasyVehicle : EnhancedScript
    {
        private readonly Random random = new Random();

        public EasyVehicle () : base("EasyVehicle")
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        public void OnTick(object sender, EventArgs e)
        {
            CheckCheats();
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        public void CheckCheats()
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
            if (Game.WasCheatStringJustEntered("vehicle"))
            {
                SpawnRandomVehicle();
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

        public void SpawnRandomVehicle()
        {
            Array allHashes = Enum.GetValues(typeof(VehicleHash));
            VehicleHash type = (VehicleHash)allHashes.GetValue(random.Next(allHashes.Length));
            var vehicle = World.CreateVehicle(type, Game.Player.Character.Position.Around(5));
            vehicle.MarkAsNoLongerNeeded();
            Game.Player.Money -= 1000;
        }

        public void Lucid()
        {
            var curV = Game.Player.Character.CurrentVehicle;
            var pos = Game.Player.Character.Position;
            var direction = Game.Player.Character.Heading;
            Vehicle vehicle = World.CreateVehicle(VehicleHash.Oppressor2, pos, direction);
            Game.Player.Character.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            vehicle.IsRadioEnabled = false;
            vehicle.MarkAsNoLongerNeeded();
            if (curV != null)
            {
                curV.PetrolTankHealth = -1;
                curV.Position = curV.GetOffsetPosition(new Vector3(0, 0, -1));
            }
        }
    }
}
