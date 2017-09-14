﻿using Microsoft.Xna.Framework;
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
        bool arrowKeyPressed = false;

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

            //LOADING: Sound Effects
            SoundEffect thunk = Content.Load<SoundEffect>("SoundFX/thunk");
            fxList.Add(thunk);

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
            player = new Character(15, 10, 6, playerImage, randomTile,fxList);

            for(int i=0;i<4;i++)
            {
                randomTile = tileList[rnd.Next(0, tileList.Count)];
                Character enemy = new Character(12, 10, 6, enemyImage, randomTile,fxList);
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
            

            if(Keyboard.GetState().IsKeyDown(Keys.Up))
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            foreach(Tile t in tileList)
            {
                int tileX = t.gridX;
                int tileY = t.gridY;
                Vector2 positionVector = new Vector2(tileX * 50, tileY * 50);
                spriteBatch.Draw(t.texture,positionVector,Color.White);
            }

            int playerTileX = player.currentPosition.gridX * 50;
            int playerTileY = player.currentPosition.gridY * 50;
            Vector2 playerVector = new Vector2(playerTileX, playerTileY);
            spriteBatch.Draw(player.texture, playerVector, Color.White);

            foreach(Character e in enemyList)
            {
                int enemyTileX = e.currentPosition.gridX * 50;
                int enemyTileY = e.currentPosition.gridY * 50;
                Vector2 enemyVector = new Vector2(enemyTileX, enemyTileY);
                spriteBatch.Draw(e.texture, enemyVector, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //called each time the player moves
        public void tick()
        {

        }
    }
}
