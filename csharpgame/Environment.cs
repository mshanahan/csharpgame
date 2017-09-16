using Microsoft.Xna.Framework;
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
        public object GraphicsDevice { get; private set; }

        public Random Random = new Random();

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

        /// <summary>
        /// Sets up the Environment with a Game and a Player.
        /// </summary>
        /// <param name="g">The Game</param>
        /// <param name="p">The Player</param>
        public void Setup(Game1 g, Character p)
        {
            this.Game = g;
            this.Player = p;
        }

        public void DrawTiles(SpriteBatch s)
        {
            foreach (Tile t in TileList)
            {
                int TileScreenX = t.gridX * 50;
                int TileScreenY = t.gridY * 50;

                int PlayerGridX = Player.currentPosition.gridX;
                int PlayerGridY = Player.currentPosition.gridY;
                int PlayerScreenX = PlayerGridX * 50;
                int PlayerScreenY = PlayerGridY * 50;

                Vector2 Location = new Vector2((Game.GraphicsDevice.Viewport.Width / 2) + TileScreenX - PlayerScreenX, (Game.GraphicsDevice.Viewport.Height / 2) + TileScreenY - PlayerScreenY);
                s.Draw(t.texture, Location, Color.White);
            }
        }

        public void DrawNPCs(SpriteBatch s)
        {
            
            foreach(Character npc in NPCList)
            {
                int NPCGridX = npc.currentPosition.gridX;
                int NPCGridY = npc.currentPosition.gridY;
                int PlayerGridX = Player.currentPosition.gridX;
                int PlayerGridY = Player.currentPosition.gridY;

                int NPCScreenX = (NPCGridX * 50) + 25;
                int NPCScreenY = (NPCGridY * 50) + 25;
                int PlayerScreenX = PlayerGridX * 50;
                int PlayerScreenY = PlayerGridY * 50;

                Vector2 Location = new Vector2(
                    (Game.GraphicsDevice.Viewport.Width / 2) + NPCScreenX - PlayerScreenX,
                    (Game.GraphicsDevice.Viewport.Height / 2) + NPCScreenY - PlayerScreenY);
                Vector2 TextLocation = new Vector2(
                    (Game.GraphicsDevice.Viewport.Width / 2) + NPCScreenX - PlayerScreenX - 25,
                    (Game.GraphicsDevice.Viewport.Height / 2) + NPCScreenY - PlayerScreenY + 25);
                Vector2 SpriteOrigin = new Vector2(npc.texture.Width / 2, npc.texture.Height / 2);

                s.Draw(npc.texture, Location, null, Color.White, npc.rotation, SpriteOrigin, 1F, SpriteEffects.None, 0f);
                s.DrawString(FontList[0], npc.Name + "\r\n" + npc.CurrentHitpoints + "/" + npc.MaxHitpoints, TextLocation, Color.Red);
            }

        }
    }
}
