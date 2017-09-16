using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace csharpgame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Environment env;
        public Character player;

        bool arrowKeyPressed = false;
        bool characterSheetPressed = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
            graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
            Content.RootDirectory = "Content";
            env = Environment.Current();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            //Environment env = Environment.Current();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // LOADING: Tile Images
            Texture2D StoneFloorTile = Content.Load<Texture2D>("Graphics/StoneFloorTile");
            Texture2D StoneFloorTileV2 = Content.Load<Texture2D>("Graphics/StoneFloorTileVar2");
            Texture2D StoneFloorTileV3 = Content.Load<Texture2D>("Graphics/StoneFloorTileVar3");

            List<Texture2D> StoneFloorVariants = new List<Texture2D>();
            StoneFloorVariants.Add(StoneFloorTile);
            StoneFloorVariants.Add(StoneFloorTileV2);
            StoneFloorVariants.Add(StoneFloorTileV3);
            TileFloorStone.Textures = StoneFloorVariants;

            Texture2D StoneWallTile = Content.Load<Texture2D>("Graphics/StoneWallTile");
            TileWallStone.Texture = StoneWallTile;

            //LOADING: Character Images
            Texture2D playerImage = Content.Load<Texture2D>("Graphics/PlayerToken");

            Texture2D goblinImage = Content.Load<Texture2D>("Graphics/GoblinToken");
            CharGoblin.GoblinImage = goblinImage;
            Texture2D goblinCorpseImage = Content.Load<Texture2D>("Graphics/GoblinDead");
            CharGoblin.GoblinDeathImage = goblinCorpseImage;

            //LOADING: Sound Effects
            SoundEffect thunk = Content.Load<SoundEffect>("SoundFX/thunk");
            env.Add(thunk);

            //LOADING: misc
            Texture2D beigeCard = Content.Load<Texture2D>("Graphics/BeigeCard");
            env.Add(beigeCard);

            //LOADING: Fonts
            SpriteFont arial = Content.Load<SpriteFont>("Arial");
            env.Add(arial);

            //for (int i = 0; i < 50; i++)
            //{
            //    for (int j = 0; j < 50; j++)
            //    {
            //        int percentile = env.Random.Next(1, 101);
            //        Tile t;
            //        if (i == 0 || i == 49 || j == 0 || j == 49)
            //        {
            //            t = new Tile(Tile.Type.StoneWall, StoneWallTile, i, j);
            //        }
            //        else
            //        {
            //            t = new Tile(Tile.Type.StoneFloor, StoneFloorVariants, i, j);
            //        }
            //        env.Add(t);
            //    }
            //}

            Tuple<char, Tile.Type, List<Texture2D>> StoneFloor = new Tuple<char, Tile.Type, List<Texture2D>>('S', Tile.Type.Floor, StoneFloorVariants);
            env.ReadMap("Content/Maps/prototype.txt", null);

            Tile randomTile = env.TileList[env.Random.Next(0, env.TileList.Count)];
            player = new Character(env.TileList[1]);
            player.Name = "Player";
            player.Attack = 0;
            player.Damage = 2;
            player.Armor = 10;
            player.CurrentHitpoints = 10;
            player.MaxHitpoints = 10;
            player.setPlayer();
            player.texture = playerImage;
            player.DeathTexture = playerImage;
            env.Setup(this, player);

            for (int i = 0; i < 20; i++)
            {
                randomTile = env.TileList[env.Random.Next(0, env.TileList.Count)];
                Character enemy = new CharGoblin(randomTile);
                enemy.behavior = Character.Behavior.Wandering;
                env.Add(enemy);
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C))
            {
                characterSheetPressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Microsoft.Xna.Framework.Input.Keys.C))
            {
                characterSheetPressed = false;
            }


            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                if (!arrowKeyPressed)
                {
                    player.Move(0, -1);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                if (!arrowKeyPressed)
                {
                    player.Move(0, 1);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                if (!arrowKeyPressed)
                {
                    player.Move(1, 0);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                if (!arrowKeyPressed)
                {
                    player.Move(-1, 0);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }

            if (
                Keyboard.GetState().IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Left)
                && Keyboard.GetState().IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Right)
                && Keyboard.GetState().IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Up)
                && Keyboard.GetState().IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                arrowKeyPressed = false;
            }

            // TODO: Add your update logic here

            foreach (Text t in env.DecayingTextList)
            {
                t.Decay();
            }

            List<Text> marked = new List<Text>();
            foreach (Text t in env.DecayingTextList)
            {
                if (t.Kill == true)
                {
                    marked.Add(t);
                }
            }

            foreach (Text m in marked)
            {
                env.Remove(m);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            env.DrawTiles(spriteBatch); //draw all Tiles in the Environment...
            env.DrawCorpses(spriteBatch); //draw all Corpses in the Environment...
            env.DrawPlayer(spriteBatch); //draw the Player...
            env.DrawNPCs(spriteBatch); //draw all NPCs in the Environment...
            env.DrawDecayingText(spriteBatch); //draw all Decaying Text in the Environment...

            if (characterSheetPressed) //PLACEHOLDER: this can be cleaned up once UIElement is implemented
            {
                spriteBatch.Draw(env.UIElementList[0], new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(env.FontList[0], "Hit Points: " + player.CurrentHitpoints + "/" + player.MaxHitpoints, new Vector2(0, 0), Color.Black);
                spriteBatch.DrawString(env.FontList[0], "Armor: " + player.Armor, new Vector2(0, 24), Color.Black);
                spriteBatch.DrawString(env.FontList[0], "Damage: " + player.Damage, new Vector2(0, 48), Color.Black);
                spriteBatch.DrawString(env.FontList[0], "Weapon: Shortsword", new Vector2(0, 96), Color.Black);
                spriteBatch.DrawString(env.FontList[0], "Armor: None", new Vector2(0, 120), Color.Black);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //called each time the player moves
        public void tick()
        {
            foreach (Character e in env.NPCList)
            {
                e.AIRoutine();
            }
        }

    }
}
