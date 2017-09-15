using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class Environment
    {

        private static Environment CurrentEnvironment = null;
        private static List<Environment> EnvironmentList = null;

        public Game1 game { get; private set; }
        public List<Tile> TileList { get; private set; }
        public Character Player { get; private set; }
        public List<Character> NPCList { get; private set; }
        public List<SoundEffect> SoundFXList { get; private set; }
        public List<SpriteFont> FontList { get; private set; }
        public List<Texture2D> UIElementList { get; private set; }

        public static Environment Current(Game1 game)
        {
            if(CurrentEnvironment == null)
            {
                CurrentEnvironment = new Environment(game);
                EnvironmentList.Add(CurrentEnvironment);
                return CurrentEnvironment;
            }
            return CurrentEnvironment;
        }

        public Environment(Game1 game)
        {
            this.game = game;
            this.TileList = new List<Tile>();
            this.Player = game.player;
            this.NPCList = new List<Character>();
            this.SoundFXList = new List<SoundEffect>();
            this.FontList = new List<SpriteFont>();
            this.UIElementList = new List<Texture2D>();
        }

    }
}
