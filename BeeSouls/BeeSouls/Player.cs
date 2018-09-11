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
        private Texture2D playerHitLeftTexture;  
        private Texture2D playerHitTexture;
        private Texture2D playerAttackTexture;
        private Texture2D playerAttackLeftTexture;

        private int direction = 1;
        private int wingFlapMult = 35;
        private int playerHealth = 100;
        private Rectangle playerHitBox, tempHitBox;
        bool IsPlayerHit = false;
        float timeSinceLastSprite = 0f;
        private float attackCounter = 0f; 

        private KeyboardState currKeyboardState;
        private KeyboardState prevKeyboardState;

        public Player(Game game) : base(game)
        {
            Position = new Vector2(50, 50);
            currKeyboardState = Keyboard.GetState();
            prevKeyboardState = currKeyboardState;
        }

        


        public override void Update(GameTime gameTime)
        {
            timeSinceLastSprite += (float)gameTime.ElapsedGameTime.TotalSeconds;

            prevKeyboardState = currKeyboardState;
            currKeyboardState = Keyboard.GetState();

            var attacking = PlayerAttack.IsAttacking;

            //Position += Velocity;

            playerHitBox = new Rectangle((int)Position.X, (int)Position.Y, currentTexture.Width, currentTexture.Height);
            

            if (playerHitBox.Intersects(tempHitBox))
            {
                playerHealth -= 10;
                IsPlayerHit = true;
            }


            if (currKeyboardState.IsKeyDown(Keys.W) || currKeyboardState.IsKeyDown(Keys.Up))
            {
                Velocity = new Vector2(0, -2.0f);

            }
            else if (currKeyboardState.IsKeyDown(Keys.S) || currKeyboardState.IsKeyDown(Keys.Down))
            {

                Velocity = new Vector2(0, 2.0f);


            }
            else if (currKeyboardState.IsKeyDown(Keys.A) || currKeyboardState.IsKeyDown(Keys.Left))
            {
                Velocity = new Vector2(-2.0f, 0);
                direction = -1;

            }
            else if (currKeyboardState.IsKeyDown(Keys.D) || currKeyboardState.IsKeyDown(Keys.Right))
            {
                Velocity = new Vector2(2.0f, 0);
                direction = 1;

            }
            else if (currKeyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space) && !PlayerAttack.IsAttacking)
            {

               

                PlayerAttack.IsAttacking = true;

                
            }
            else
            {
                Velocity = new Vector2(0, 0);
            }

            

            //Console.WriteLine(Math.Sin(gameTime.TotalGameTime.TotalSeconds));
            if (Math.Sin(wingFlapMult * gameTime.TotalGameTime.TotalSeconds) < 0) //Flying
            {
                if (direction > 0)
                {
                    currentTexture = playerFlyTexture;

                }
                else
                {
                    currentTexture = playerLeftFlyTexture;
                }
            }
           
            else //Normal
            {
                if (direction > 0)
                {
                    currentTexture = playerTexture;
                    if(IsPlayerHit == true)
                    {
                        currentTexture = playerHitTexture;
                        IsPlayerHit = false;
                    }
                }
                else
                {
                    currentTexture = playerLeftTexture;
                    if(IsPlayerHit == true)
                    {
                        currentTexture = playerHitLeftTexture;
                        IsPlayerHit = false;
                    }
                }
            }
           
            //Console.WriteLine(PlayerAttack.IsAttacking);
           

            if (PlayerAttack.IsAttacking  == true)
            {
                attackCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Console.WriteLine(attackCounter);
                Velocity = new Vector2(0, 0);


                if (attackCounter > 300)
                {
                  
                    PlayerAttack.IsAttacking = false;
                    attackCounter = 0;
                }
                else
                {
                    if (direction > 0)
                    {
                        currentTexture = playerAttackTexture;
                        PlayerAttack.IsAttacking = false;
                    }
                    else
                    {
                        currentTexture = playerAttackLeftTexture;
                        PlayerAttack.IsAttacking = false;
                    }

                }



            }

            base.Update(gameTime);
        }

        

        protected override void LoadContent()
        {

            playerTexture = Game.Content.Load<Texture2D>("player/beeRight");
            playerFlyTexture = Game.Content.Load<Texture2D>("player/bee_flyRight");
            playerLeftTexture = Game.Content.Load<Texture2D>("player/bee");
            playerLeftFlyTexture = Game.Content.Load<Texture2D>("player/bee_fly");
            playerHitTexture = Game.Content.Load<Texture2D>("player/bee_HitRight");
            playerHitLeftTexture = Game.Content.Load<Texture2D>("player/bee_Hit");
            playerAttackTexture = Game.Content.Load<Texture2D>("player/bee_attack");
            playerAttackLeftTexture = Game.Content.Load<Texture2D>("player/bee_attackLeft");


            currentTexture = playerFlyTexture;

            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
       

            spriteBatch.Draw(currentTexture, new Rectangle(300, 250, currentTexture.Width, currentTexture.Height), Color.White);
            
          
        }
    }
}
