﻿using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using System.Windows.Forms;
using System.IO;

namespace LBTGTAVMods
{
    public class FlareMates : Script
    {
        private readonly List<Ped> flareMen = new List<Ped>();
        private readonly string _logPath = "scripts/FlareMates.log";
        private readonly State _state = new State(3000);

        public FlareMates()
        {
            // EVENTS
            Tick += OnTick;
            KeyDown += OnKeyDown;
            _state.StateEnded += new State.StateEndedEventHandler(ClearParty);

            // START LOG FILE
            File.WriteAllText(_logPath, "script started ... \n");
        }

        ~FlareMates()
        {
            ClearParty();
        }

        private void WriteDebug(string text)
        {
            File.AppendAllText(_logPath, text + "\n");
        }

        private void OnTick(object sender, EventArgs e)
        {
            _state.Tick();
            if (Game.WasCheatStringJustEntered("flare"))
                SpawnParty();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void SpawnParty()
        {
            if (_state.IsActive())
            {
                _state.Reset();
                _state.OnEndState();
                return;
            }

            for (int i = 0; i < 20; i++)
            {
                // PREPARE LOCATION
                var distance = new Random().NextDouble() * 40;
                var position = Game.Player.Character.Position.Around((float)distance);

                // CREATE NPC
                var npc = World.CreatePed(PedHash.AviSchwartzman, position);

                // GIVE WEAPON
                if (new Random().NextDouble() < 0.5)
                    npc.Weapons.Give(WeaponHash.FlareGun, 1, true, true);
                else
                    npc.Weapons.Give(WeaponHash.Firework, 1, true, true);

                // SHOOT
                npc.Task.ShootAt(Game.Player.Character);

                // ADD TO LIST
                flareMen.Add(npc);

                // PARTICLE EFFECT
                var asset = new ParticleEffectAsset("core");
                World.CreateParticleEffectNonLooped(asset, "exp_air_molotov_lod", npc.Position);
            }
            _state.Start();
        }

        private void ClearParty()
        {
            foreach (Ped p in flareMen)
            {
                try
                {
                    // PLAY EFFECT
                    var asset = new ParticleEffectAsset("core");
                    World.CreateParticleEffectNonLooped(asset, "exp_air_molotov_lod", p.Position);

                    // DELETE
                    p.Delete();
                }
                catch (Exception ex)
                {
                    WriteDebug(ex.Message);
                    WriteDebug(ex.Source);
                    WriteDebug(ex.StackTrace);
                }
            }
            flareMen.Clear();
        }
    }
}