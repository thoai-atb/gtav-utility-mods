using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.Math;
using System.Windows.Forms;
using System.IO;

namespace LBTGTAVMods
{
    class EasyWeapon : EnhancedScript
    {
        public EasyWeapon() : base("EasyWeapon")
        {
            KeyDown += OnKeyDown;
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.WasCheatStringJustEntered("givethrows"))
                GiveThrowables();
            if (Game.WasCheatStringJustEntered("flash"))
                ToggleFlashLight();
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Add)
            {
                BuyAmmo();
            }
            else if(e.KeyCode == Keys.Subtract)
            {
                ToggleSuppressor();
            }
            else if(e.KeyCode == Keys.Divide)
            {
                ToggleThermalVision();
            }
            else if(e.KeyCode == Keys.Tab)
            {
                if (Game.Player.Character.CurrentVehicle != null)
                {
                    Game.Player.Character.Weapons.Select(WeaponHash.Unarmed);
                }
            }
        }

        private void BuyAmmo()
        {
            WriteLog("BuyAmmo");
            try
            {
                Weapon current = Game.Player.Character.Weapons.Current;
                int before = current.Ammo;
                current.Ammo += 5 * current.MaxAmmoInClip;
                int gain = (current.Ammo - before) / current.MaxAmmoInClip;
                Game.Player.Money -= gain;
            }
            catch (Exception)
            {
                WriteLog("Can't Buy Ammo");
            }
        }

        private void ToggleSuppressor()
        {
            WriteLog("Toggling Suppressor");
            try
            {
                Weapon current = Game.Player.Character.Weapons.Current;
                bool pre = current.Components.GetSuppressorComponent().Active;
                current.Components.GetSuppressorComponent().Active = !pre;
            }
            catch (Exception)
            {
                WriteLog("Can't Toggle");
            }
        }

        private void ToggleFlashLight()
        {
            WriteLog("Toggling FlashLight");
            try
            {
                Weapon current = Game.Player.Character.Weapons.Current;
                bool pre = current.Components.GetFlashLightComponent().Active;
                current.Components.GetFlashLightComponent().Active = !pre;
            }
            catch (Exception)
            {
                WriteLog("Can't Toggle");
            }
        }

        private void ToggleThermalVision()
        {
            Game.IsThermalVisionActive = !Game.IsThermalVisionActive;
        }

        private void GiveThrowables()
        {
            WeaponCollection tools = Game.Player.Character.Weapons;
            WeaponHash[] throwables = new WeaponHash[]
            {
                WeaponHash.Grenade,
                WeaponHash.SmokeGrenade,
                WeaponHash.Molotov,
                WeaponHash.ProximityMine
            };

            foreach (var weaponHash in throwables)
            {
                Weapon weapon = tools.Give(weaponHash, 1, false, false);
                weapon.Ammo = weapon.MaxAmmo;
            }
        }
    }
}
