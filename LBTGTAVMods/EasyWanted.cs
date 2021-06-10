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
    class EasyWanted : EnhancedScript
    {
        public EasyWanted() : base("EasyWanted")
        {
            KeyDown += OnKeyDown;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Multiply)
                Teleport();
        }

        public void Teleport()
        {
            WriteLog("Teleported");
            Vector3 targetPosition = Game.Player.Character.GetOffsetPosition(new Vector3(0, 5, 0));
            ParticleEffectAsset asset = new ParticleEffectAsset("core");
            string effectName = "exp_grd_grenade_lod";
            World.CreateParticleEffectNonLooped(asset, effectName, targetPosition, scale:1);
        }
    }
}
