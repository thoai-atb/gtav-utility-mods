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
                if(vehicle != null)
                    vehicle.IsRadioEnabled = false;
            }
            if (Game.WasCheatStringJustEntered("para"))
            {
                WriteLog("Give Parachute");
                Game.Player.Character.Weapons.Give(WeaponHash.Parachute, 1, true, true);
            }
            if (Game.WasCheatStringJustEntered("hem"))
            {
                Game.Player.Character.RemoveHelmet(false);
            }
            if (Game.WasCheatStringJustEntered("wonder"))
            {
                // Game.Player.Character.Money -= 100; -> This line doesn't work
                Array allHashes = Enum.GetValues(typeof(VehicleHash));
                VehicleHash type = (VehicleHash) allHashes.GetValue(random.Next(allHashes.Length));
                var vehicle = World.CreateVehicle(type, Game.Player.Character.Position.Around(5));
                vehicle.MarkAsNoLongerNeeded();
            }
            if (Game.WasCheatStringJustEntered("cover"))
            {
                Random random = new Random();
                List<Vehicle> list = new List<Vehicle>();
                for(int i = 0; i<20; i++)
                {
                    var radius = random.NextDouble() * 20 + 10;
                    list.Add(World.CreateVehicle(VehicleHash.Rubble, Game.Player.Character.Position.Around((float) radius)));
                }
                foreach(var v in list)
                {
                    v.MarkAsNoLongerNeeded();
                }
            }
            if (Game.WasCheatStringJustEntered("getin"))
            {
                WriteLog("cheat get in");
                Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                WriteLog(vehicle.ToString());
                if (vehicle != null)
                {
                    Ped p = GetNearestPed();
                    WriteLog(p.ToString());
                    if (p != null)
                    {
                        Game.Player.Character.SetIntoVehicle(vehicle, VehicleSeat.Passenger);
                        p.Task.EnterVehicle(vehicle, VehicleSeat.Driver);
                    }
                }
            }
        }

        private Ped GetNearestPed()
        {
            Ped[] peds = World.GetAllPeds();
            Ped nearest = null;
            float nearestDist = float.PositiveInfinity;
            Vector3 playerLoc = Game.Player.Character.Position;

            for(int i = 0; i < peds.Length; i++)
            {
                if (!peds[i].IsHuman)
                    continue;
                if (peds[i].IsPlayer)
                    continue;
                float dist = peds[i].Position.DistanceTo(playerLoc);
                if(dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = peds[i];
                }
            }

            return nearest;
        }
    }
}
