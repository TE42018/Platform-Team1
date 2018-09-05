using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace BeeSouls
{
    class Player : DrawableGameComponent, IMovingObjects
    {
        public bool IsDead { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        //Variables for the graphic
        private Texture2D currentTexture;
        private Texture2D playerTexture;
        private Texture2D playerFlyTexture;
        private Texture2D playerLeftTexture;
        private Texture2D playerLeftFlyTexture;

        private int wingFlapMult = 35;


        float timeSinceLastSprite = 0f;
    
   

        public Player(Game game) : base(game)
        {
            Position = new Vector2(50, 50);
        }




        public override void Update(GameTime gameTime)
        {
            timeSinceLastSprite += (float)gameTime.ElapsedGameTime.TotalSeconds;
                       

            Position += Velocity;
          

            

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                Velocity = new Vector2(0, -2.0f);
               
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                
                   Velocity = new Vector2(0, 2.0f);
                
                
            }
            else if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                Velocity = new Vector2(-2.0f, 0);


            }
            else if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                Velocity = new Vector2(2.0f, 0);
                
            }
            else
            {
                Velocity = new Vector2(0, 0);
            }

            Console.WriteLine(Math.Sin(gameTime.TotalGameTime.TotalSeconds));
            if (Math.Sin(wingFlapMult * gameTime.TotalGameTime.TotalSeconds) < 0) //Fly
            {
                if (Velocity.X >= 0)
                    currentTexture = playerFlyTexture;
                else
                    currentTexture = playerLeftFlyTexture;
            }
            else //Normal
            {
                if (Velocity.X >= 0)
                    currentTexture = playerTexture;
                else
                    currentTexture = playerLeftTexture;
            }


            base.Update(gameTime);
        }

        

        protected override void LoadContent()
        {

            

            playerTexture = Game.Content.Load<Texture2D>("player/beeRight");
            playerFlyTexture = Game.Content.Load<Texture2D>("player/bee_flyRight");
            playerLeftTexture = Game.Content.Load<Texture2D>("player/bee");
            playerLeftFlyTexture = Game.Content.Load<Texture2D>("player/bee_fly");

            currentTexture = playerFlyTexture;

            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();


            spriteBatch.Draw(currentTexture, new Rectangle((int)Position.X, (int)Position.Y, currentTexture.Width, currentTexture.Height), Color.White);
            //if (Velocity.X >= 0)
            //{
            //    //spriteBatch.Draw(playerTexture, Position, Color.White);
                
            //    if (timeSinceLastSprite > 0.5f)
            //    {
            //        spriteBatch.Draw(playerFlyTexture, Position, Color.White);
            //        timeSinceLastSprite = 0f;
            //    }
            //    else
            //    {
            //        spriteBatch.Draw(playerTexture, Position, Color.White);
            //    }
            //}
            //else if(Velocity.X < 0)
            //{
            //    spriteBatch.Draw(playerLeftTexture, Position, Color.White);

            //    if (timeSinceLastSprite > .5f)
            //    {
            //        spriteBatch.Draw(playerLeftFlyTexture, Position, Color.White);
            //        timeSinceLastSprite = 0f;
            //    }
            //    else
            //    {
            //        spriteBatch.Draw(playerTexture, Position, Color.White);
            //    }
            //}

          
            
            spriteBatch.End();
        }
    }
}
