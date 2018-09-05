using System;
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

        const byte MENU = 0, PLAYGAME = 1, GAMEOVER = 2, HIGHSCORE = 3, OPTIONS = 4;
        int CurrentScreen = MENU;

        //Variables for the MENU Screen
        Texture2D highscoreText, optionText, playgameText;
        Button playGameButton, optionsButton, highscoreButton;
        float screenwidth, screenheight;
        Texture2D bgimage;

        public Game1()
        {
            tileEngine = new TileEngine(this);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
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
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
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
            playgameText = Content.Load<Texture2D>("PlayGame");
            bgimage = Content.Load<Texture2D>("main menu");

            highscoreButton = new Button(new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), true);
            highscoreButton.load(Content, "highscores");

            optionsButton = new Button(new Rectangle(300, 200, optionText.Width, optionText.Height), true);
            optionsButton.load(Content, "options");

            playGameButton = new Button(new Rectangle(300, 100, playgameText.Width, playgameText.Height), true);
            playGameButton.load(Content, "PlayGame");

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
            if (state.IsKeyDown(Keys.Down))
                tileEngine.CameraPosition += new Vector2(0, 5.0f);
            if (state.IsKeyDown(Keys.Up))
                tileEngine.CameraPosition += new Vector2(0, -5.0f);
            if (state.IsKeyDown(Keys.Left))
                tileEngine.CameraPosition += new Vector2(-5.0f, 0);
            if (state.IsKeyDown(Keys.Right))
                tileEngine.CameraPosition += new Vector2(5.0f, 0);
            if (state.IsKeyDown(Keys.PageDown))
                tileEngine.Zoom += 0.05f;
            if (state.IsKeyDown(Keys.PageUp))
                tileEngine.Zoom -= 0.05f;

            switch (CurrentScreen)
            {
                case MENU:
                    //What we want to happen in the MENU screen goes in here.
                    //GO TO PLAYGAME SCREEN
                    if (playGameButton.update(new Vector2(mouseState.X, mouseState.Y)) == true && mouseState != previousMouseState && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        CurrentScreen = PLAYGAME;
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

                    break;

                case OPTIONS:
                    //Whatever Options you want to DISPLAY
                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        CurrentScreen = MENU;
                    }
                    break;

                case HIGHSCORE:
                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        CurrentScreen = MENU;
                    }
                    break;



                case PLAYGAME:
                    //What we want to happen when we play our GAME goes in here.
                    if (ks.IsKeyDown(Keys.Escape))
                    {
                        CurrentScreen = MENU;
                    }
                    break;

                case GAMEOVER:
                    //What we want to happen when our GAME is OVER goes in here.
                    break;



            }
            previousMouseState = mouseState;
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
            switch (CurrentScreen)
            {
                case MENU:
                    //What we want to happen in the MENU screen goes in here.
                    spriteBatch.Draw(bgimage, new Rectangle(0, 0, bgimage.Width, bgimage.Height), Color.White);
                    spriteBatch.Draw(playgameText, new Rectangle(300, 100, playgameText.Width, playgameText.Height), Color.White);
                    spriteBatch.Draw(optionText, new Rectangle(300, 200, optionText.Width, optionText.Height), Color.White);
                    spriteBatch.Draw(highscoreText, new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), Color.White);
                    // spriteBatch.Draw(bgimage, new Rectangle(800, 420, bgimage.Width, bgimage.Height), Color.White);
                    break;

                case OPTIONS:
                    //Whatever Options you want to DISPLAY
                    spriteBatch.Draw(optionText, new Rectangle(300, 200, optionText.Width, optionText.Height), Color.White);
                    break;

                case HIGHSCORE:
                    spriteBatch.Draw(highscoreText, new Rectangle(300, 300, highscoreText.Width, highscoreText.Height), Color.White);
                    break;

                case PLAYGAME:
                    //What we want to happen when we play our GAME goes in here.
                    spriteBatch.Draw(playgameText, new Rectangle(300, 100, playgameText.Width, playgameText.Height), Color.White);
                    spriteBatch.Draw(playgameText, new Rectangle(300, 200, playgameText.Width, playgameText.Height), Color.White);
                    spriteBatch.Draw(playgameText, new Rectangle(300, 300, playgameText.Width, playgameText.Height), Color.White);
                    break;

                case GAMEOVER:
                    //What we want to happen when our GAME is OVER goes in here.
                    break;

            }
            tileEngine.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
