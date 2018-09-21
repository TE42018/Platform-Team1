using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BeeSouls
{
    class Player : DrawableGameComponent, IMovingObjects
    {
        public bool IsDead { get; set; }

        internal static int getInt(string totalCoinKey, int v)
        {
            throw new NotImplementedException();
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                playerHitBox = new Rectangle((int)Position.X, (int)Position.Y, 60, 40);
            }
        }

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
        private Texture2D playerDeadTexture;
        private Texture2D playerDeadRightTexture;

        public int direction = 1;
        private int wingFlapMult = 35;
        private int playerHealth = 100;
        private Rectangle playerHitBox;

        public Rectangle PlayerHitBox
        {
            get { return playerHitBox; }
        }


        bool IsPlayerHit = false;

        float timeSinceLastSprite = 0f;
        private float attackCounter = 0f;
        private float timeSinceLastHit = 0f;

        private KeyboardState currKeyboardState;
        private KeyboardState prevKeyboardState;
        private Vector2 _position;
        List<Bullet> bullets = new List<Bullet>();

        public Player(Game game) : base(game)
        {
            Position = new Vector2(50, 800);
        }

        public void Collide(CollisionData data, string direction)
        {
            Rectangle overlap = data.Area;
            if (overlap == Rectangle.Empty)
            {
                return;
            }

            if (data.Tile == 3)
            {
                BeeSoulsGame.LoadNextMAp();
                Position = new Vector2(50, 50);
            }

            //if (overlap.Width > overlap.Height)
            if (direction == "height")
            {
                //justera höjdled
                if (PlayerHitBox.Center.Y > overlap.Center.Y)
                {
                    Position = new Vector2(Position.X, overlap.Bottom + 1);
                }
                else
                {
                    Position = new Vector2(Position.X, overlap.Top - PlayerHitBox.Height - 1);
                }
            }
            if (direction == "width")
            {
                //justera sidled
                if (PlayerHitBox.Center.X > overlap.Center.X)
                {
                    Position = Position + new Vector2(overlap.Width, 0);
                }
                else
                {
                    Position = Position + new Vector2(-overlap.Width - 1, 0);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            timeSinceLastSprite += (float)gameTime.ElapsedGameTime.TotalSeconds;

            prevKeyboardState = currKeyboardState;
            currKeyboardState = Keyboard.GetState();

            var attacking = PlayerAttack.IsAttacking;

            playerHitBox = new Rectangle((int)Position.X, (int)Position.Y, currentTexture.Width, currentTexture.Height);

            Console.WriteLine(playerHealth);

            for (int i = 0; i < EnemyManager.enemylist.Count; i++)
            {
                Rectangle enemyBox = EnemyManager.enemylist[i].Hitbox;
                if (playerHitBox.Intersects(enemyBox))
                {
                    IsPlayerHit = true;
                    EnemyManager.enemylist.RemoveAt(i);
                }
            }

            foreach (var b in bullets)
                b.Update(gameTime);

            if (currKeyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space) && !PlayerAttack.IsAttacking)
            {
                attacking = true;
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

            if (currKeyboardState.IsKeyDown(Keys.E) && prevKeyboardState.IsKeyUp(Keys.E))
            {
                var bullet = new Bullet(this.Game);
                bullet.Velocity = new Vector2(direction, 0) * 8;
                bullet.Position = Position;
                bullets.Add(bullet);
            }

            //Console.WriteLine(Math.Sin(gameTime.TotalGameTime.TotalSeconds));
            if (Math.Sin(wingFlapMult * gameTime.TotalGameTime.TotalSeconds) < 0 && IsDead == false) //Flying
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
                    if (IsPlayerHit == true)
                    {
                        currentTexture = playerHitTexture;
                        IsPlayerHit = false;
                    }
                    else if (IsDead == true)
                    {
                        currentTexture = playerDeadRightTexture;
                        Velocity = new Vector2(0, 0);
                    }
                }
                else
                {
                    currentTexture = playerLeftTexture;
                    if (IsPlayerHit == true)
                    {
                        currentTexture = playerHitLeftTexture;
                        IsPlayerHit = false;

                    }
                    else if (IsDead == true)
                    {
                        currentTexture = playerDeadTexture;
                        Velocity = new Vector2(0, 0);
                    }
                }
            }



            if (playerHealth <= 0)
            {
                IsDead = true;
            }
            else
            {
                IsDead = false;
            }



            if (attacking == true)
            {
                attackCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                Velocity = new Vector2(0, 0);


                if (attackCounter > 500)
                {

                    attacking = false;
                    attackCounter = 0;
                }
                else
                {
                    if (direction > 0)
                    {
                        currentTexture = playerAttackTexture;

                    }
                    else
                    {
                        currentTexture = playerAttackLeftTexture;

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
            playerDeadTexture = Game.Content.Load<Texture2D>("player/bee_dead");
            playerDeadRightTexture = Game.Content.Load<Texture2D>("player/bee_deadRight");


            currentTexture = playerFlyTexture;

            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var i = (int)(Position.X + TileEngine.CameraOffset.X);
            var i1 = (int)(Position.Y + TileEngine.CameraOffset.Y);
            spriteBatch.Draw(currentTexture, new Rectangle(i, i1, currentTexture.Width, currentTexture.Height), Color.White);

            foreach (var b in bullets)
                b.Draw(spriteBatch);
        }
    } 
}
 