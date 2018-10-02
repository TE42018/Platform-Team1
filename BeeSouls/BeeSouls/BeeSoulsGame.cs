using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace BeeSouls
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BeeSoulsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Player player;
        PlayerAttack bullet;
        

        EnemyManager enemyManager;
        static Boss boss;

        public static TileEngine tileEngine;

        MouseState mouseState, previousMouseState;
        KeyboardState ks;
        Color col;


        const byte MENU = 0, PLAYGAME = 1, GAMEOVER = 2, HIGHSCORE = 3, OPTIONS = 4, EXIT = 5;
        int CurrentScreen = MENU;

        //Variables for the MENU Screen
        Texture2D highscoreText, optionText, playgameText, exitText;
        Button playGameButton, optionsButton, highscoreButton, exitButton;
        float screenwidth, screenheight;
        Texture2D bgimage;
        // music 
        public Song song;
            SoundEffect effect;
            SoundEffectInstance effectInstance;



        public BeeSoulsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            enemyManager = new EnemyManager(Content);
            tileEngine = new TileEngine(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            //graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            col = Color.White;
            screenheight = graphics.GraphicsDevice.Viewport.Height;
            screenwidth = graphics.GraphicsDevice.Viewport.Width;
            tileEngine.TileHeight = 70;
            tileEngine.TileWidth = 70;
            tileEngine.MapData = new int[,]
            {   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,8,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,7,8,0,0,0,0,0,0,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,7,8,0,0,0,0,0,0},
                {0,0,0,7,8,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,8,0,0,0,0,0,0,0,0,0,0,0,4},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {0,0,0,0,0,0,5,0,0,1,0,0,5,0,0,6,0,0,0,0,0,1,2,1,0,0,0,5,0,0,0,0,0,1,0,0,0,6,0,0,0,3},
                {1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,1},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2}};

            player = new Player(this);
            Components.Add(player);

            boss = new Boss(this);
            Components.Add(boss);

            bullet = new PlayerAttack(this);
            Components.Add(bullet);

            base.Initialize();
        }
        internal void ChangeState(MenuState menuState)
        {
            throw new NotImplementedException();
        }

        public static void LoadNextMAp()
        {
            boss.HasSpawned = true;
           
            tileEngine.MapData = new int[,]
            {   {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,9,11,11,11,11,11,11,11,11,11},
                {11,11,11,11,11,11,14,11,11,11,11,11,12,11,11,13,11,11,11,11,11,9,10,9,11,11,11,13,11,11,11,11},
                {9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,10,10,10,9,9,9,9,9,9,9,9},
                {10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10}};
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            enemyManager.Loadcontent(Content);
            tileEngine.TileMap = Content.Load<Texture2D>("tilemap");
            //Things we want to load in the MENU screen.
            highscoreText = Content.Load<Texture2D>("highscores");
            optionText = Content.Load<Texture2D>("options");
            playgameText = Content.Load<Texture2D>("btnPlay");
            exitText = Content.Load<Texture2D>("exit");
            bgimage = Content.Load<Texture2D>("main menu");

            //music
            effect = Content.Load<SoundEffect>("BeeSound");
            effectInstance = effect.CreateInstance();
            song = Content.Load<Song>("song");
            effectInstance.Volume = 0.5f;



            exitButton = new Button(new Rectangle(300, 400, exitText.Width, exitText.Height), true);
            exitButton.load(Content, "exit");
            highscoreButton = new Button(new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), true);
            highscoreButton.load(Content, "highscores");

            optionsButton = new Button(new Rectangle(300, 200, optionText.Width, optionText.Height), true);
            optionsButton.load(Content, "options");

            playGameButton = new Button(new Rectangle(300, 100, playgameText.Width, playgameText.Height), true);
            playGameButton.load(Content, "btnPlay");

            Bullet.Texture = Content.Load<Texture2D>("player/playerShot");
            BossBullet.BossBulletTexture = Content.Load<Texture2D>("boss/bulletboi");

            //bossBullet = new BossBullet(this);
            //Components.Add(bossBullet);

            base.LoadContent();
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
            //Checking the state of our mouse.
            mouseState = Mouse.GetState();
            ks = Keyboard.GetState();

            KeyboardState state = Keyboard.GetState();


            Console.WriteLine(player.Velocity.Length());



                switch (CurrentScreen)
            {
                case MENU:
                    //What we want to happen in the MENU screen goes in here.
                    //GO TO PLAYGAME SCREEN

                    if (playGameButton.update(new Vector2(mouseState.X, mouseState.Y)) == true &&
                        mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = PLAYGAME;
                        tileEngine.CameraPosition = new Vector2(player.Position.X, player.Position.Y);
                        MediaPlayer.Play(song);
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Volume = 0.5f;

                    }
                    else
                    {
                        tileEngine.CameraPosition = new Vector2(-5500, -5500);

                    }

                    //GO TO OPTIONS SCREEN
                    if (optionsButton.update(new Vector2(mouseState.X, mouseState.Y)) == true &&
                        mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = OPTIONS;
                    }

                    //GO TO HIGHSCORE SCREEN
                    if (highscoreButton.update(new Vector2(mouseState.X, mouseState.Y)) == true &&
                        mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = HIGHSCORE;
                    }


                    if (exitButton.update(new Vector2(mouseState.X, mouseState.Y)) == true &&
                        mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = EXIT;
                        Exit();
                    }

                    break;

                case OPTIONS:
                    //Whatever Options you want to DISPLAY
                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        CurrentScreen = MENU;
                        tileEngine.CameraPosition += new Vector2(-2000, -2000);

                    }

                    break;

                case HIGHSCORE:
                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        CurrentScreen = MENU;
                        tileEngine.CameraPosition += new Vector2(-2000, -2000);
                    }

                    break;

                    

                case PLAYGAME:
                    tileEngine.CameraPosition = player.Position;
                    enemyManager.Update(gameTime);
                    player.Update(gameTime);
                    bullet.Update(gameTime);
                    boss.Update(gameTime);
                    if (Player.IsDead == false )

                    if (state.IsKeyDown(Keys.Down))
                    {
                        if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
                        {
                            player.Position += new Vector2(0, 5.0f);
                            player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "height");
                        }
                        if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
                        {
                            player.Position += new Vector2(0, -5.0f);
                            player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "height");
                        }
                        if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
                        {
                            player.Position += new Vector2(-5.0f, 0);
                            player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "width");
                            player.direction = -1;
                        }
                        if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
                        {
                            player.Position += new Vector2(5.0f, 0);
                            player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "width");
                            player.direction = 1;
                        }
                        player.Position += new Vector2(0, 5.0f);
                        player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "height");


                    }
                    else

                        if (state.IsKeyDown(Keys.Up))
                    {
                        Console.WriteLine("Man kan inte g� n�r man e d�d");
                        player.Position += new Vector2(0, -5.0f);
                        player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "height");

                    }

                    if (state.IsKeyDown(Keys.Left))
                    {
                        player.Position += new Vector2(-5.0f, 0);
                        player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "width");
                        player.direction = -1;
                    }

                    if (state.IsKeyDown(Keys.Right))
                    {
                        player.Position += new Vector2(5.0f, 0);
                        player.Collide(tileEngine.CheckCollision(player.PlayerHitBox), "width");
                        player.direction = 1;
                       
                    }

                    if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Left) ||
                        state.IsKeyDown(Keys.Right))
                    {
                        if (effectInstance.State == SoundState.Stopped)
                            effectInstance.Play();
                    }
                    else
                    {
                        effectInstance.Stop();
                    }

                    if (player.currKeyboardState.IsKeyDown(Keys.K) && player.prevKeyboardState.IsKeyUp(Keys.K))
                    {
                        effectInstance.Volume += 0.1f;
                    }
                    else if (player.currKeyboardState.IsKeyDown(Keys.J) && player.prevKeyboardState.IsKeyUp(Keys.J))

                    {
                        effectInstance.Volume -= 0.1f;
                    }
                    else if (effectInstance.Volume >= 0.9f)
                    {
                        effectInstance.Volume = 0.9f;
                    }
                    else if (effectInstance.Volume <= 0.1f)
                    {
                        effectInstance.Volume = 0.1f;
                    }

                    if (player.currKeyboardState.IsKeyDown(Keys.I) && player.prevKeyboardState.IsKeyUp(Keys.I))
                    {
                        MediaPlayer.Volume += 0.1f;
                    }
                    else if (player.currKeyboardState.IsKeyDown(Keys.O) && player.prevKeyboardState.IsKeyUp(Keys.O))

                    {
                        MediaPlayer.Volume -= 0.1f;
                    }
                    else if (MediaPlayer.Volume >= 1.0f)
                    {
                        MediaPlayer.Volume = 1.0f;
                    }
                    else if (MediaPlayer.Volume <= 0.0f)
                    {
                        MediaPlayer.Volume = 0.0f;
                    }


                    int screenCenterX = tileEngine.viewportWidth / 2;
                    int screenCenterY = tileEngine.viewportHeight / 2;
                    var min = new Vector2(screenCenterX, screenCenterY);
                    var max = new Vector2(tileEngine.MapData.GetLength(1) * tileEngine.TileWidth, tileEngine.MapData.GetLength(0) * tileEngine.TileHeight);
                    player.Position = Vector2.Clamp(player.Position, Vector2.Zero, new Vector2(max.X - player.PlayerHitBox.Width, max.Y - player.PlayerHitBox.Height));
                   // Console.WriteLine(tileEngine.GetHitboxes(player.PlayerHitBox).Count);
                    //What we want to happen when we play our GAME goes in here.
                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        CurrentScreen = MENU;
                        tileEngine.CameraPosition += new Vector2(-2000, -2000);
                    }

                    break;
                case EXIT:
                    //What we want to happen when we play our GAME goes in here.
                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        CurrentScreen = MENU;
                        tileEngine.CameraPosition += new Vector2(-2000, -2000);
                    }

                    break;

                case GAMEOVER:
                    //What we want to happen when our GAME is OVER goes in here.
                    break;


                    base.Update(gameTime);

            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            tileEngine.Draw(gameTime, spriteBatch);
            enemyManager.Draw(spriteBatch);
            //boss.Draw(spriteBatch);
            switch (CurrentScreen)
            {
                case MENU:
                    //What we want to happen in the MENU screen goes in here.
                    spriteBatch.Draw(bgimage, new Rectangle(0, 0, bgimage.Width, bgimage.Height), Color.White);
                    spriteBatch.Draw(playgameText, new Rectangle(300, 100, playgameText.Width, playgameText.Height), Color.White);
                    spriteBatch.Draw(optionText, new Rectangle(300, 200, optionText.Width, optionText.Height), Color.White);
                    spriteBatch.Draw(highscoreText, new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), Color.White);
                    spriteBatch.Draw(exitText, new Rectangle(300, 400, exitText.Width, exitText.Height), Color.White);
                    // spriteBatch.Draw(bgimage, new Rectangle(800, 420, bgimage.Width, bgimage.Height), Color.White);
                    break;

                case OPTIONS:
                    //Whatever Options you want to DISPLAY
                    spriteBatch.Draw(optionText, new Rectangle(300, 100, optionText.Width, optionText.Height), Color.White);
                    spriteBatch.Draw(optionText, new Rectangle(300, 200, optionText.Width, optionText.Height), Color.White);
                    spriteBatch.Draw(optionText, new Rectangle(300, 300, optionText.Width, optionText.Height), Color.White);
                    break;

                case HIGHSCORE:
                    spriteBatch.Draw(highscoreText, new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), Color.White);
                    break;

                case PLAYGAME:
                    //What we want to happen when we play our GAME goes in here.

                    break;
                case EXIT:
                    //What we want to happen when we play our GAME goes in here.

                    break;

                case GAMEOVER:
                    //What we want to happen when our GAME is OVER goes in here.
                    break;

            }


            player.Draw(spriteBatch);
            boss.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}