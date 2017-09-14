using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace csharpgame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Tile> tileList = new List<Tile>();
        Character player;
        List<Character> enemyList = new List<Character>();
        List<SoundEffect> fxList = new List<SoundEffect>();
        List<Texture2D> miscTexList = new List<Texture2D>();
        List<SpriteFont> fonts = new List<SpriteFont>();

        bool arrowKeyPressed = false;
        bool characterSheetPressed = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            Texture2D dirtImage = Content.Load<Texture2D>("Graphics/TileDirt");
            Texture2D stoneImage = Content.Load<Texture2D>("Graphics/TileRock");

            //LOADING: Character Images
            Texture2D playerImage = Content.Load<Texture2D>("Graphics/PlayerToken");
            Texture2D enemyImage = Content.Load<Texture2D>("Graphics/enemyToken");
            Texture2D goblinImage = Content.Load<Texture2D>("Graphics/GoblinToken");

            //LOADING: Sound Effects
            SoundEffect thunk = Content.Load<SoundEffect>("SoundFX/thunk");
            fxList.Add(thunk);

            //LOADING: misc
            Texture2D beigeCard = Content.Load<Texture2D>("Graphics/BeigeCard");
            miscTexList.Add(beigeCard);

            //LOADING: Fonts
            SpriteFont arial = Content.Load<SpriteFont>("Arial");
            fonts.Add(arial);

            Random randGen = new Random();

            for (int i=0;i<16;i++)
            {
                for(int j=0;j<9;j++)
                {
                    int percentile = randGen.Next(1, 101);
                    Tile t;
                    if(percentile <= 80)
                    {
                        t = new Tile(Tile.Type.Dirt, dirtImage, i, j);
                    }
                    else
                    {
                        t = new Tile(Tile.Type.Rock, stoneImage, i, j);
                    }
                    tileList.Add(t);
                }
            }

            Random rnd = new Random();

            Tile randomTile = tileList[rnd.Next(0, tileList.Count)];
            player = new Character(15, 10, 6, playerImage, randomTile,fxList,randGen);
            player.setPlayer();

            for(int i=0;i<4;i++)
            {
                randomTile = tileList[rnd.Next(0, tileList.Count)];
                Character enemy = new Character(12, 10, 6, goblinImage, randomTile,fxList, randGen);
                enemyList.Add(enemy);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                characterSheetPressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.C))
            {
                characterSheetPressed = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (!arrowKeyPressed)
                {
                    player.Move(tileList, 0, -1);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if(!arrowKeyPressed)
                {
                    player.Move(tileList, 0, 1);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (!arrowKeyPressed)
                {
                    player.Move(tileList, 1, 0);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (!arrowKeyPressed)
                {
                    player.Move(tileList, -1, 0);
                    arrowKeyPressed = true;
                    this.tick();
                }
            }

            if(Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                arrowKeyPressed = false;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            foreach(Tile t in tileList)
            {
                int tileX = t.gridX;
                int tileY = t.gridY;
                Vector2 positionVector = new Vector2(tileX * 50, tileY * 50);
                spriteBatch.Draw(t.texture,positionVector,Color.White);
            }

            int playerTileX = (player.currentPosition.gridX * 50) + 25;
            int playerTileY = (player.currentPosition.gridY * 50) + 25;
            Vector2 playerVector = new Vector2(playerTileX, playerTileY);
            //spriteBatch.Draw(player.texture, playerVector, Color.White);
            Vector2 origin = new Vector2(player.texture.Width/2, player.texture.Height/2);
            spriteBatch.Draw(player.texture, playerVector, null, Color.White, player.rotation, origin, 1F, SpriteEffects.None, 0f);

            foreach(Character e in enemyList)
            {
                int enemyTileX = (e.currentPosition.gridX * 50) + 25;
                int enemyTileY = (e.currentPosition.gridY * 50) + 25;
                Vector2 enemyVector = new Vector2(enemyTileX, enemyTileY);
                //spriteBatch.Draw(e.texture, enemyVector, Color.White);
                Vector2 eOrigin = new Vector2(e.texture.Width / 2, e.texture.Height / 2);
                spriteBatch.Draw(e.texture, enemyVector, null, Color.White, e.rotation, eOrigin, 1F, SpriteEffects.None, 0f);
            }


            if(characterSheetPressed)
            {
                spriteBatch.Draw(miscTexList[0], new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(fonts[0], "Hit Points: " + player.hitpoints, new Vector2(0, 0), Color.Black);
                spriteBatch.DrawString(fonts[0], "Armor: " + player.armorClass, new Vector2(0, 24), Color.Black);
                spriteBatch.DrawString(fonts[0], "Damage: 1-6", new Vector2(0, 48), Color.Black);
                spriteBatch.DrawString(fonts[0], "Weapon: Shortsword", new Vector2(0, 96), Color.Black);
                spriteBatch.DrawString(fonts[0], "Armor: None", new Vector2(0, 120), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //called each time the player moves
        public void tick()
        {
            foreach(Character e in enemyList)
            {
                e.AIRoutine(tileList);
            }
        }
    }
}
