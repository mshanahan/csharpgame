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
        private static List<Environment> EnvironmentList = new List<Environment>();

        public Game1 Game { get; private set; }
        public List<Tile> TileList { get; set; }
        public Character Player { get; private set; }
        public List<Character> NPCList { get; private set; }
        public List<SoundEffect> SoundFXList { get; private set; }
        public List<SpriteFont> FontList { get; private set; }
        public List<Texture2D> UIElementList { get; private set; }
        public List<Text> DecayingTextList { get; private set; }

        public static Environment Current(Game1 game)
        {
            if(CurrentEnvironment == null)
            {
                CurrentEnvironment = new Environment(game);
                EnvironmentList.Add(CurrentEnvironment);
                return CurrentEnvironment;
            }
            CurrentEnvironment.Game = game;
            CurrentEnvironment.Player = game.player;
            return CurrentEnvironment;
        }

        public static Environment Current()
        {
            if (CurrentEnvironment == null)
            {
                CurrentEnvironment = new Environment();
                EnvironmentList.Add(CurrentEnvironment);
                return CurrentEnvironment;
            }
            return CurrentEnvironment;
        }

        public Environment(Game1 game)
        {
            this.Game = game;
            this.TileList = new List<Tile>();
            this.Player = game.player;
            this.NPCList = new List<Character>();
            this.SoundFXList = new List<SoundEffect>();
            this.FontList = new List<SpriteFont>();
            this.UIElementList = new List<Texture2D>();
            this.DecayingTextList = new List<Text>();
        }

        public Environment()
        {
            this.TileList = new List<Tile>();
            this.Player = null;
            this.NPCList = new List<Character>();
            this.SoundFXList = new List<SoundEffect>();
            this.FontList = new List<SpriteFont>();
            this.UIElementList = new List<Texture2D>();
            this.DecayingTextList = new List<Text>();
        }

        /// <summary>
        /// Adds a Tile to the Enviromnet.
        /// </summary>
        /// <param name="t">The Tile to add</param>
        public void Add(Tile t)
        {
            TileList.Add(t);
        }

        /// <summary>
        /// Adds an NPC to the Environment.
        /// </summary>
        /// <param name="c">The Character to add</param>
        public void Add(Character c)
        {
            NPCList.Add(c);
        }

        /// <summary>
        /// Adds a sound effect to the Environment
        /// </summary>
        /// <param name="s">The SoundEffect to add</param>
        public void Add(SoundEffect s)
        {
            SoundFXList.Add(s);
        }

        /// <summary>
        /// Adds a font to the Environment
        /// </summary>
        /// <param name="f">The SpriteFont to add</param>
        public void Add(SpriteFont f)
        {
            FontList.Add(f);
        }

        /// <summary>
        /// Adds a UI Element to the Environment
        /// </summary>
        /// <param name="u">The Texture2D to add</param>
        public void Add(Texture2D u)
        {
            UIElementList.Add(u);
        }

        /// <summary>
        /// Adds a decaying text to the Environment
        /// </summary>
        /// <param name="t">The Text to add</param>
        public void Add(Text t)
        {
            DecayingTextList.Add(t);
        }

        /// <summary>
        /// Removes a decaying text from the Environment
        /// </summary>
        /// <param name="t">The Text to remove</param>
        public void Remove(Text t)
        {
            DecayingTextList.Remove(t);
        }

    }
}
