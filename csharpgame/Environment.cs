using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<Tile> TileList { get; private set; }
        public CharPlayer Player { get; private set; }
        public List<Character> NPCList { get; private set; }
        public List<Corpse> CorpseList { get; private set; }
        public List<SoundEffect> SoundFXList { get; private set; }
        public List<SpriteFont> FontList { get; private set; }
        public List<UIElement> UIElementList { get; private set; }
        public List<Text> DecayingTextList { get; private set; }
        public object GraphicsDevice { get; private set; }

        public bool DrawTradingScreen { get; set; } = false;

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
            this.CorpseList = new List<Corpse>();
            this.SoundFXList = new List<SoundEffect>();
            this.FontList = new List<SpriteFont>();
            this.UIElementList = new List<UIElement>();
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
        /// Adds a Corpse to the Environment.
        /// </summary>
        /// <param name="c">The Corpse to add</param>
        public void Add(Corpse c)
        {
            CorpseList.Add(c);
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
        public void Add(UIElement u)
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
        /// Removes an NPC from the Environment
        /// </summary>
        /// <param name="c">The Character to remove</param>
        public void Remove(Character c)
        {
            NPCList.Remove(c);
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
        public void Setup(Game1 g, CharPlayer p)
        {
            this.Game = g;
            this.Player = p;
        }

        public void DrawTiles(SpriteBatch s)
        {
            foreach (Tile t in TileList)
            {
                int distance = Tile.distanceBetween(t, Player.currentPosition);
                if ( distance <= 6)
                {
                    int TileScreenX = t.gridX * 50;
                    int TileScreenY = t.gridY * 50;
                    int PlayerGridX = Player.currentPosition.gridX;
                    int PlayerGridY = Player.currentPosition.gridY;
                    int PlayerScreenX = PlayerGridX * 50;
                    int PlayerScreenY = PlayerGridY * 50;

                    Vector2 Location = new Vector2(
                        (Game.GraphicsDevice.Viewport.Width / 2) + TileScreenX - PlayerScreenX,
                        (Game.GraphicsDevice.Viewport.Height / 2) + TileScreenY - PlayerScreenY);
                    
                    float Alpha = 1F - (distance / 7F);

                    s.Draw(t.texture, Location, Color.White * Alpha);
                }
            }
        }

        public void DrawPlayer(SpriteBatch s)
        {
            Vector2 Location = new Vector2(
                (Game.GraphicsDevice.Viewport.Width / 2) + 25,
                (Game.GraphicsDevice.Viewport.Height / 2) + 25);
            Vector2 SpriteOrigin = new Vector2(
                Player.texture.Width / 2,
                Player.texture.Height / 2);

            s.Draw(Player.texture, Location, null, Color.White, Player.rotation, SpriteOrigin, 1F, SpriteEffects.None, 0f);
        }

        public void DrawNPCs(SpriteBatch s)
        {

            foreach (Character npc in NPCList)
            {
                int distance = Tile.distanceBetween(npc.currentPosition, Player.currentPosition);
                if (distance <= 6)
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

                    float Alpha = 1F - (distance / 7F);

                    s.Draw(npc.texture, Location, null, Color.White * Alpha, npc.rotation, SpriteOrigin, 1F, SpriteEffects.None, 0f);
                    s.DrawString(FontList[0], npc.Name + "\r\n" + npc.CurrentHitpoints + "/" + npc.MaxHitpoints, TextLocation, Color.Red * Alpha);
                }
            }

        }

        public void DrawCorpses(SpriteBatch s)
        {
            foreach (Corpse c in CorpseList)
            {
                int distance = Tile.distanceBetween(c.Position, Player.currentPosition);
                if (distance <= 6)
                {
                    int CorpseGridX = c.Position.gridX;
                    int CorpseGridY = c.Position.gridY;
                    int PlayerGridX = Player.currentPosition.gridX;
                    int PlayerGridY = Player.currentPosition.gridY;
                    int CorpseScreenX = (CorpseGridX * 50) + 25;
                    int CorpseScreenY = (CorpseGridY * 50) + 25;
                    int PlayerScreenX = PlayerGridX * 50;
                    int PlayerScreenY = PlayerGridY * 50;

                    Vector2 Location = new Vector2(
                        (Game.GraphicsDevice.Viewport.Width / 2) + CorpseScreenX - PlayerScreenX,
                        (Game.GraphicsDevice.Viewport.Height / 2) + CorpseScreenY - PlayerScreenY);
                    Vector2 SpriteOrigin = new Vector2(c.Texture.Width / 2, c.Texture.Height / 2);

                    float Alpha = 1F - (distance / 7F);

                    s.Draw(c.Texture, Location, null, Color.White * Alpha, c.Rotation, SpriteOrigin, 1F, SpriteEffects.None, 0f);
                }
            }
        }

        public void DrawDecayingText(SpriteBatch s)
        {
            foreach (Text t in DecayingTextList)
            {
                s.DrawString(FontList[0], t.Contents, new Vector2(t.XPos, t.YPos), Color.Black * t.Transparency);
            }
        }

        public void DrawUIElements(SpriteBatch s)
        {
            foreach(UIElement e in UIElementList)
            {
                int ScreenX = e.Background.Item2;
                int ScreenY = e.Background.Item3;
                if(e.RenderBackground) s.Draw(e.Background.Item1, new Vector2(ScreenX, ScreenY), Color.White);

                foreach(Tuple<Texture2D,int,int> t in e.ElementImages)
                {
                    int RelativeX = ScreenX + t.Item2;
                    int RelativeY = ScreenY + t.Item3;
                    s.Draw(t.Item1, new Vector2(RelativeX, RelativeY), Color.White);
                }

                foreach(Tuple<string,int,int> t in e.ElementText)
                {
                    int RelativeX = ScreenX + t.Item2;
                    int RelativeY = ScreenY + t.Item3;
                    s.DrawString(FontList[0],t.Item1, new Vector2(RelativeX, RelativeY), Color.Red);
                }

            }

            if(DrawTradingScreen)
            {
                UIElement e = CharTrader.GetUpdatedPanel();
                int ScreenX = e.Background.Item2;
                int ScreenY = e.Background.Item3;
                if (e.RenderBackground) s.Draw(e.Background.Item1, new Vector2(ScreenX, ScreenY), Color.White);

                foreach (Tuple<Texture2D, int, int> t in e.ElementImages)
                {
                    int RelativeX = ScreenX + t.Item2;
                    int RelativeY = ScreenY + t.Item3;
                    s.Draw(t.Item1, new Vector2(RelativeX, RelativeY), Color.White);
                }

                foreach (Tuple<string, int, int> t in e.ElementText)
                {
                    int RelativeX = ScreenX + t.Item2;
                    int RelativeY = ScreenY + t.Item3;
                    s.DrawString(FontList[0], t.Item1, new Vector2(RelativeX, RelativeY), Color.Red);
                }

            }

            UIPlayerState.Update();
            UIPlayerState PlayerState = UIPlayerState.GetState();
            int StateScreenX = PlayerState.Background.Item2;
            int StateScreenY = PlayerState.Background.Item3;
            float BarPercent = PlayerState.HealthPercent;
            int BarPixelWidth = (int) (300F * BarPercent);

            s.Draw(UIPlayerState.HealthBarBackground, new Vector2(StateScreenX, StateScreenY), Color.White);
            s.Draw(UIPlayerState.HealthBar, new Vector2(StateScreenX, StateScreenY), new Rectangle(StateScreenX, StateScreenY, BarPixelWidth, 10), Color.White);

            s.Draw(UIPlayerState.GoldGraphic, new Vector2(StateScreenX + 310, StateScreenY), Color.White);
            s.DrawString(FontList[0]," x " + CharPlayer.GetPlayer().Gold, new Vector2(StateScreenX + 325, StateScreenY), Color.Gold);
        }

        public void ReadMap(String directory, List<Tuple<int, Action<Tile>>> WeightedSpawnerList)
        {
            StreamReader reader = new StreamReader(directory);
            Environment env = Environment.Current();
            int y = 0;
            string currentRow;
            while ((currentRow = reader.ReadLine()) != null)
            {
                for (int x = 0; x < currentRow.Length; x++)
                {
                    char currentTile = currentRow[x];
                    Tile ThisTile = null ;

                    if (Char.ToUpper(currentTile) == 'S')
                    {
                        ThisTile = new TileFloorStone(x, y);
                        this.Add(ThisTile);
                    }
                    if (Char.ToUpper(currentTile) == 'W')
                    {
                        ThisTile = new TileWallStone(x, y);
                        this.Add(ThisTile);
                    }
                    if (Char.ToUpper(currentTile) == 'G')
                    {
                        ThisTile = new TileWaterStagnant(x, y);
                        this.Add(ThisTile);
                    }
                    if (Char.IsLower(currentTile) && ThisTile != null)
                    {
                        //sum all the weights
                        int summedWeight = 0;
                        foreach(Tuple<int, Action<Tile>> tuple in WeightedSpawnerList)
                        {
                            int weight = tuple.Item1;
                            summedWeight = summedWeight + weight;
                        }

                        //roll a random number
                        int rand = env.Random.Next(1, summedWeight + 1);

                        //subtract weights from rand until 0 is reached
                        bool found = false;
                        for(int i=0;i<WeightedSpawnerList.Count;i++)
                        {
                            rand = rand - WeightedSpawnerList[i].Item1;
                            Console.WriteLine(rand);
                            if(rand <= 0)
                            {
                                WeightedSpawnerList[i].Item2(ThisTile); //call spawn on the randomly chosen monster
                                found = true;
                            }
                            if (found) break;
                        }
                    }
                }
                y++;
            }
        }
    }
}
