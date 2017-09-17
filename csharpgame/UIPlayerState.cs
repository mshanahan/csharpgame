using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class UIPlayerState : UIElement
    {
        private static UIPlayerState State { get; set; }
        public static Texture2D HealthBarBackground { get; set; }
        public static Texture2D HealthBar { get; set; }
        public float HealthPercent { get; set; }

        public UIPlayerState()
        {
            Environment env = Environment.Current();
            int PosX = (env.Game.GraphicsDevice.Viewport.Width / 2) - 150;
            int PosY = (env.Game.GraphicsDevice.Viewport.Height) - 150;
            this.Background = new Tuple<Texture2D, int, int>(UIPlayerState.HealthBarBackground, PosX, PosY);
        }

        public static UIPlayerState GetState()
        {
            if(UIPlayerState.State == null)
            {
                UIPlayerState.State = new UIPlayerState();
            }
            return UIPlayerState.State;
        }

        public static void Update()
        {
            Environment env = Environment.Current();
            UIPlayerState.GetState().HealthPercent = (float) env.Player.CurrentHitpoints / env.Player.MaxHitpoints;
            if (UIPlayerState.GetState().HealthPercent <= 0F) UIPlayerState.GetState().HealthPercent = 0F;
        }
    }
}
