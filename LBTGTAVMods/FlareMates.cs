using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using System.Windows.Forms;
using System.IO;

namespace LBTGTAVMods
{
    public class FlareMates : EnhancedScript
    {
        private readonly List<Ped> flareMen = new List<Ped>();
        private readonly State _state = new State(3000);
        private readonly Random _random = new Random();

        public FlareMates() : base("FlareMates")
        {
            // EVENTS
            Tick += OnTick;
            KeyDown += OnKeyDown;
            _state.StateEnded += new State.StateEndedEventHandler(ClearParty);
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
                // PREPARE
                var distance = _random.NextDouble() * 40;
                var position = Game.Player.Character.Position.Around((float)distance);

                // CREATE NPC
                var npc = World.CreatePed(PedHash.AviSchwartzman, position);

                // GIVE WEAPON
                if (_random.NextDouble() < 0.8)
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
            foreach (Ped npc in flareMen)
            {
                try
                {
                    // EFFECT
                    var asset = new ParticleEffectAsset("core");
                    World.CreateParticleEffectNonLooped(asset, "exp_air_molotov_lod", npc.Position);

                    // Mark ignore
                    npc.Delete();
                }
                catch (Exception ex)
                {
                    WriteLog(ex.Message);
                    WriteLog(ex.Source);
                    WriteLog(ex.StackTrace);
                }
            }
            flareMen.Clear();
        }
    }
}
