using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> Player
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
<<<<<<< HEAD
        EnemyManager enemyManager;
        
=======
        Player player;
        PlayerAttack bullet;
       
>>>>>>> Player
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
<<<<<<< HEAD
            enemyManager.Loadcontent(Content);
            // TODO: use this.Content to load your game content here
=======

            base.LoadContent();
>>>>>>> Player
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

<<<<<<< HEAD
            // TODO: Add your update logic here
            enemyManager.Update(gameTime);
=======
          
            player.Update(gameTime);
           
>>>>>>> Player
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
<<<<<<< HEAD
            spriteBatch.Begin();
            enemyManager.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

=======

            player.Draw(spriteBatch);
          
            spriteBatch.End();
>>>>>>> Player
            base.Draw(gameTime);

        }
    }
}
