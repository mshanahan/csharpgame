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

        bool arrowKeyPressed = false;
        int arrowKeyPressedConsecutive = 0;
        bool numberPressed = false;
        bool AllDark = false;

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

            Texture2D TileWaterStagnantGraphic = Content.Load<Texture2D>("Graphics/TileWaterStagnant");
            TileWaterStagnant.Texture = TileWaterStagnantGraphic;

            //LOADING: Character Images
            Texture2D playerImage = Content.Load<Texture2D>("Graphics/PlayerToken");
            Texture2D PlayerDeathImage = Content.Load<Texture2D>("Graphics/PlayerDead");
            CharPlayer.PlayerImage = playerImage;
            CharPlayer.PlayerDeathImage = PlayerDeathImage;

            Texture2D goblinImage = Content.Load<Texture2D>("Graphics/GoblinToken");
            CharGoblin.GoblinImage = goblinImage;
            Texture2D goblinCorpseImage = Content.Load<Texture2D>("Graphics/GoblinDead");
            CharGoblin.GoblinDeathImage = goblinCorpseImage;

            Texture2D KoboldImage = Content.Load<Texture2D>("Graphics/KoboldToken");
            CharKobold.Image = KoboldImage;
            Texture2D KoboldDeathImage = Content.Load<Texture2D>("Graphics/KoboldDead");
            CharKobold.DeathImage = KoboldDeathImage;

            Texture2D TraderImage = Content.Load<Texture2D>("Graphics/TraderGraphic");
            Texture2D TraderScreen = Content.Load<Texture2D>("Graphics/TraderScreen");
            CharTrader.TraderImage = TraderImage;
            CharTrader.TraderDeathImage = TraderImage;
            CharTrader.TradingBackground = TraderScreen;

            //LOADING: Sound Effects
            SoundEffect thunk = Content.Load<SoundEffect>("SoundFX/thunk");
            env.Add(thunk);

            //LOADING: misc
            Texture2D HealthBack = Content.Load<Texture2D>("Graphics/HealthBack");
            Texture2D HealthBar = Content.Load<Texture2D>("Graphics/HealthBar");
            Texture2D GoldGraphic = Content.Load<Texture2D>("Graphics/Gold");
            Texture2D TorchGraphicFront = Content.Load<Texture2D>("Graphics/TorchGraphicFront");
            Texture2D TorchGraphicBack = Content.Load<Texture2D>("Graphics/TorchGraphicBack");
            UIPlayerState.HealthBarBackground = HealthBack;
            UIPlayerState.HealthBar = HealthBar;
            UIPlayerState.GoldGraphic = GoldGraphic;
            UIPlayerState.TorchGraphicFront = TorchGraphicFront;
            UIPlayerState.TorchGraphicBack = TorchGraphicBack;


            //LOADING: Fonts
            SpriteFont arial = Content.Load<SpriteFont>("Arial");
            env.Add(arial);
            
            List<Tuple<int, Action < Tile >>> WeightList = new List<Tuple<int, Action<Tile>>>();
            Tuple<int, Action<Tile>> GoblinWeight = new Tuple<int, Action<Tile>>(1, new Action<Tile>(CharGoblin.Spawn));
            WeightList.Add(GoblinWeight);
            Tuple<int, Action<Tile>> KoboldWeight = new Tuple<int, Action<Tile>>(2, new Action<Tile>(CharKobold.Spawn));
            WeightList.Add(KoboldWeight);
            //env.ReadMap("Content/Maps/prototype2.txt", WeightList);
            env.GenerateDungeon(3, 3, WeightList);
            
            CharPlayer player = CharPlayer.GetPlayer();
            env.Setup(this, player);

            //CharTrader trader = new CharTrader(env.TileList[2]);
            //env.Add(trader);

            CharTrader.ArmorProgression.Add(new ItemArmor("Padded Armor",4, 11));
            CharTrader.ArmorProgression.Add(new ItemArmor("Leather Armor", 6, 12));
            CharTrader.ArmorProgression.Add(new ItemArmor("Studded Leather Armor", 30, 13));
            CharTrader.ArmorProgression.Add(new ItemArmor("Scale Mail", 50, 14));
            CharTrader.ArmorProgression.Add(new ItemArmor("Chain Mail", 70, 15));
            CharTrader.ArmorProgression.Add(new ItemArmor("Splint Mail", 75, 16));
            CharTrader.ArmorProgression.Add(new ItemArmor("Banded Mail", 85, 17));
            CharTrader.ArmorProgression.Add(new ItemArmor("Plate Mail", 450, 18));

            CharTrader.WeaponProgression.Add(new ItemWeapon("Dagger", 5, 1, 4));
            CharTrader.WeaponProgression.Add(new ItemWeapon("Shortsword", 19, 1, 6));
            CharTrader.WeaponProgression.Add(new ItemWeapon("Broadsword", 32, 1, 8));
            CharTrader.WeaponProgression.Add(new ItemWeapon("Longsword", 59, 1, 10));
            CharTrader.WeaponProgression.Add(new ItemWeapon("Greatsword", 90, 2, 6));
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

            if (CharPlayer.GetPlayer().CurrentHitpoints <= 0 || AllDark)
            {
                CharPlayer.GetPlayer().Locked = true;
                CharPlayer.GetPlayer().texture = CharPlayer.PlayerDeathImage;
                List<Tuple<string, int, int>> GameOverList = new List<Tuple<string, int, int>>();
                GameOverList.Add(new Tuple<string, int, int>("GAME OVER", 0, 0));
                GameOverList.Add(new Tuple<string, int, int>("YOU HAVE DIED", 0, 20));
                if (AllDark) GameOverList.Add(new Tuple<string, int, int>("YOU RAN OUT OF TORCHES", 0, 40));
                UIElement GameOver = new UIElement(env.Game.GraphicsDevice.Viewport.Width / 2, env.Game.GraphicsDevice.Viewport.Height / 2 - 50,GameOverList);
                env.Add(GameOver);
            }

 


                env.DrawTradingScreen = false;
                foreach(CharTrader t in CharTrader.TraderList)
                {
                    if(Tile.distanceBetween(t.currentPosition,env.Player.currentPosition) <= 1)
                    {
                        env.DrawTradingScreen = true;
                    }
                }


                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D1) && !numberPressed && env.DrawTradingScreen) // heal 1
                {
                    numberPressed = true;
                    if (env.Player.CurrentHitpoints < env.Player.MaxHitpoints && env.Player.Gold > 0)
                    {
                        env.Player.Gold--;
                        env.Player.CurrentHitpoints++;
                    }
                    else
                    {
                        env.SoundFXList[0].Play(); //thunk
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D2) && !numberPressed && env.DrawTradingScreen) // heal all
                {
                    numberPressed = true;
                    if (env.Player.CurrentHitpoints < env.Player.MaxHitpoints && env.Player.Gold >= env.Player.MaxHitpoints - env.Player.CurrentHitpoints)
                    {
                        env.Player.Gold = env.Player.Gold - (env.Player.MaxHitpoints - env.Player.CurrentHitpoints);
                        env.Player.CurrentHitpoints = env.Player.MaxHitpoints;
                    }
                    else
                    {
                        env.SoundFXList[0].Play(); //thunk
                    }
                }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D3) && !numberPressed && env.DrawTradingScreen) // upgrade attack
            {
                numberPressed = true;
                if (env.Player.Gold >= (int) Math.Pow(CharTrader.AttackCount, 2))
                {
                    env.Player.Gold = env.Player.Gold - (int) Math.Pow(CharTrader.AttackCount, 2);
                    env.Player.Attack++;
                    CharTrader.AttackCount++;
                }
                else
                {
                    env.SoundFXList[0].Play(); //thunk
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D4) && !numberPressed && env.DrawTradingScreen) // upgrade damage
            {
                numberPressed = true;
                if (env.Player.Gold >= (int) Math.Pow(CharTrader.DamageCount,3))
                {
                    env.Player.Gold = env.Player.Gold - (int) Math.Pow(CharTrader.DamageCount, 3);
                    env.Player.Damage++;
                    CharTrader.DamageCount++;
                }
                else
                {
                    env.SoundFXList[0].Play(); //thunk
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D5) && !numberPressed && env.DrawTradingScreen) // upgrade defense
            {
                numberPressed = true;
                if (env.Player.Gold >= (int) Math.Pow(CharTrader.DefenseCount,3))
                {
                    env.Player.Gold = env.Player.Gold - (int)Math.Pow(CharTrader.DefenseCount, 3);
                    env.Player.Armor++;
                    CharTrader.DefenseCount++;
                }
                else
                {
                    env.SoundFXList[0].Play(); //thunk
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D6) && !numberPressed && env.DrawTradingScreen) // upgrade hitpoints
            {
                numberPressed = true;
                if (env.Player.Gold >= 2 * CharTrader.HitpointCount)
                {
                    env.Player.Gold = env.Player.Gold - 2 * CharTrader.HitpointCount;
                    env.Player.CurrentHitpoints++;
                    env.Player.MaxHitpoints++;
                    CharTrader.HitpointCount++;
                }
                else
                {
                    env.SoundFXList[0].Play(); //thunk
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D7) && !numberPressed && env.DrawTradingScreen) // buy torch
            {
                numberPressed = true;
                if (env.Player.Gold >= 20)
                {
                    env.Player.Gold = env.Player.Gold - 20;
                    env.Player.TorchCount++;
                }
                else
                {
                    env.SoundFXList[0].Play(); //thunk
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D8) && !numberPressed && env.DrawTradingScreen) // buy armor
            {
                numberPressed = true;
                if (env.Player.Gold >= CharTrader.ArmorProgression[CharTrader.ArmorCount].Price)
                {
                    env.Player.Gold = env.Player.Gold - CharTrader.ArmorProgression[CharTrader.ArmorCount].Price;
                    env.Player.ItemArmor = CharTrader.ArmorProgression[CharTrader.ArmorCount];
                    CharTrader.ArmorCount++;
                }
                else
                {
                    env.SoundFXList[0].Play(); //thunk
                }
            }
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D9) && !numberPressed && env.DrawTradingScreen) // buy weapon
            {
                numberPressed = true;
                if (env.Player.Gold >= CharTrader.WeaponProgression[CharTrader.WeaponCount].Price)
                {
                    env.Player.Gold = env.Player.Gold - CharTrader.WeaponProgression[CharTrader.WeaponCount].Price;
                    env.Player.ItemWeapon = CharTrader.WeaponProgression[CharTrader.WeaponCount];
                    CharTrader.WeaponCount++;
                }
                else
                {
                    env.SoundFXList[0].Play(); //thunk
                }
            }


            if (!CharPlayer.GetPlayer().Locked)
            {
                if (arrowKeyPressed) arrowKeyPressedConsecutive++;
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                {
                    if (!arrowKeyPressed || arrowKeyPressedConsecutive % 10 == 0)
                    {
                        env.Player.Move(0, -1);
                        arrowKeyPressed = true;
                        arrowKeyPressedConsecutive++;
                        this.tick();
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down))
                {
                    if (!arrowKeyPressed || arrowKeyPressedConsecutive % 10 == 0)
                    {
                        env.Player.Move(0, 1);
                        arrowKeyPressed = true;
                        arrowKeyPressedConsecutive++;
                        this.tick();
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right))
                {
                    if (!arrowKeyPressed || arrowKeyPressedConsecutive % 10 == 0)
                    {
                        env.Player.Move(1, 0);
                        arrowKeyPressed = true;
                        arrowKeyPressedConsecutive++;
                        this.tick();
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left))
                {
                    if (!arrowKeyPressed || arrowKeyPressedConsecutive % 10 == 0)
                    {
                        env.Player.Move(-1, 0);
                        arrowKeyPressed = true;
                        arrowKeyPressedConsecutive++;
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
                    arrowKeyPressedConsecutive = 0;
                }

                numberPressed =
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D1) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D2) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D3) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D4) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D5) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D6) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D7) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D8) ||
                    Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D9);

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

            if(!AllDark) {
                env.RayCast(); //mark tiles in line of site for drawing
                env.DrawTiles(spriteBatch); //draw all Tiles in the Environment...
                env.DrawCorpses(spriteBatch); //draw all Corpses in the Environment...
                env.DrawPlayer(spriteBatch); //draw the Player...
                env.DrawNPCs(spriteBatch); //draw all NPCs in the Environment...
                env.DrawDecayingText(spriteBatch); //draw all Decaying Text in the Environment...
                env.ResetDrawState(); //remove the tile draw mark
            }
            env.DrawUIElements(spriteBatch); //draw all UI Elements in the Environment...

            spriteBatch.End();
            base.Draw(gameTime);
        }

        //called each time the player moves
        public void tick()
        {
            env = Environment.Current();
            env.Player.TorchTicks++;
            if(env.Player.TorchTicks == CharPlayer.MaxTicks)
            {
                env.Player.TorchTicks = 0;
                env.Player.TorchCount--;
                if(env.Player.TorchCount < 0)
                {
                    this.AllDark = true;
                }
            }
            foreach (Character e in env.NPCList)
            {
                e.AIRoutine();
            }
        }

    }
}
