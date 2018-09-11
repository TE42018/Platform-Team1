<<<<<<< HEAD:BeeSouls/BeeSouls/BeeSoulsGame.cs
﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace BeeSouls
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BeeSoulsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EnemyManager enemyManager;

        Player player;
        PlayerAttack bullet;

        public BeeSoulsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            enemyManager = new EnemyManager(Content);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new Player(this);
            Components.Add(player);

            bullet = new PlayerAttack(this);
            Components.Add(bullet);
          
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
            enemyManager.Loadcontent(Content);
            // TODO: use this.Content to load your game content here

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            enemyManager.Update(gameTime);
            player.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            enemyManager.Draw(spriteBatch);

            player.Draw(spriteBatch);         
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
=======
﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeSouls
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileEngine tileEngine;

        MouseState mouseState, previousMouseState;
        KeyboardState ks;
        Color col;

        const byte MENU = 0, PLAYGAME = 1, GAMEOVER = 2, HIGHSCORE = 3, OPTIONS = 4, EXIT = 5;
        int CurrentScreen = MENU;

        //Variables for the MENU Screen
        Texture2D highscoreText, optionText, playgameText,exitText;
        Button playGameButton, optionsButton, highscoreButton,exitButton;
        float screenwidth, screenheight;
        Texture2D bgimage;


        public Game1()
        {
            tileEngine = new TileEngine(this);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
        }
        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.BackBufferFormat = SurfaceFormat.Color;
            e.GraphicsDeviceInformation.PresentationParameters.DepthStencilFormat = DepthFormat.Depth24;
        }

        protected override void Initialize()
        {

            col = Color.White;
            screenheight = graphics.GraphicsDevice.Viewport.Height;
            screenwidth = graphics.GraphicsDevice.Viewport.Width;
            tileEngine.TileHeight = 70;
            tileEngine.TileWidth = 70;
            tileEngine.Data = new int[,]
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

            

            base.Initialize();
        }

        internal void ChangeState(MenuState menuState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileEngine.TileMap = Content.Load<Texture2D>("tilemap");
            //Things we want to load in the MENU screen.
            highscoreText = Content.Load<Texture2D>("highscores");
            optionText = Content.Load<Texture2D>("options");
            playgameText = Content.Load<Texture2D>("btnPlay");
            exitText = Content.Load<Texture2D>("exit");
            bgimage = Content.Load<Texture2D>("main menu");

            exitButton = new Button(new Rectangle(300, 400, exitText.Width, exitText.Height), true);
            exitButton.load(Content, "exit");
            highscoreButton = new Button(new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), true);
            highscoreButton.load(Content, "highscores");

            optionsButton = new Button(new Rectangle(300, 200, optionText.Width, optionText.Height), true);
            optionsButton.load(Content, "options");

            playGameButton = new Button(new Rectangle(300, 100, playgameText.Width, playgameText.Height), true);
            playGameButton.load(Content, "btnPlay");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P))
                Exit();
            //Checking the state of our mouse.
            mouseState = Mouse.GetState();
            ks = Keyboard.GetState();

            KeyboardState state = Keyboard.GetState();


            switch (CurrentScreen)
            {
                case MENU:
                    //What we want to happen in the MENU screen goes in here.
                    //GO TO PLAYGAME SCREEN
                    if (playGameButton.update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = PLAYGAME;
                        tileEngine.CameraPosition = new Vector2(950,1000);

                    }
                    else
                    {
                        tileEngine.CameraPosition = new Vector2(-5500, -5500);

                    }
                    //GO TO OPTIONS SCREEN
                    if (optionsButton.update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = OPTIONS;
                    }

                    //GO TO HIGHSCORE SCREEN
                    if (highscoreButton.update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = HIGHSCORE;
                    }


                    if (exitButton.update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
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

                    if (state.IsKeyDown(Keys.Down))
                        tileEngine.CameraPosition += new Vector2(0, 5.0f);
                    if (state.IsKeyDown(Keys.Up))
                        tileEngine.CameraPosition += new Vector2(0, -5.0f);
                    if (state.IsKeyDown(Keys.Left))
                        tileEngine.CameraPosition += new Vector2(-5.0f, 0);
                    if (state.IsKeyDown(Keys.Right))
                        tileEngine.CameraPosition += new Vector2(5.0f, 0);


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



            }
            base.Update(gameTime);
         

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            tileEngine.Draw(gameTime, spriteBatch);
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


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
>>>>>>> background:BeeSouls/BeeSouls/Game1.cs
