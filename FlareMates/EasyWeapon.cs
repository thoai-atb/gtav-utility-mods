﻿using System;
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
    public class EasyWeapon : Script
    {
        private readonly string _logPath = "scripts/EasyWeapon.log";

        public EasyWeapon()
        {
            KeyDown += OnKeyDown;
            File.WriteAllText(_logPath, "script started ... \n");
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
        }

        public void WriteDebug(string text)
        {
            File.AppendAllText(_logPath, text + "\n");
        }

        private void BuyAmmo()
        {
            WriteDebug("BuyAmmo");
            try
            {
                Weapon current = Game.Player.Character.Weapons.Current;
                int before = current.Ammo;
                current.Ammo += 5 * current.MaxAmmoInClip;
                int gain = (current.Ammo - before) / current.MaxAmmoInClip;
                // Game.Player.Money += 1;
                Game.Player.Money -= gain;
            }
            catch (Exception)
            {
                WriteDebug("Can't Buy Ammo");
            }
        }

        private void ToggleSuppressor()
        {
            WriteDebug("Toggling Suppressor");
            try
            {
                Weapon current = Game.Player.Character.Weapons.Current;
                bool pre = current.Components.GetSuppressorComponent().Active;
                current.Components.GetSuppressorComponent().Active = !pre;
            }
            catch (Exception)
            {
                WriteDebug("Can't Toggle");
            }
        }
    }
}
