using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;

namespace LBTGTAVMods
{
    public class Utility
    {
        public static Ped GetNearestPed()
        {
            Ped[] peds = World.GetAllPeds();
            Ped nearest = null;
            float nearestDist = float.PositiveInfinity;
            Vector3 playerLoc = Game.Player.Character.Position;

            for (int i = 0; i < peds.Length; i++)
            {
                if (!peds[i].IsHuman)
                    continue;
                if (peds[i].IsPlayer)
                    continue;
                float dist = peds[i].Position.DistanceTo(playerLoc);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = peds[i];
                }
            }

            return nearest;
        }
    }
}
