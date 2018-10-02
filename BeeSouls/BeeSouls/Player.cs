using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BeeSouls
{
    class Player : DrawableGameComponent, IMovingObjects
    {
        public static bool IsDead { get; set; }

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
        private int playerHealth = 10;
        private Rectangle playerHitBox;
        public Rectangle bHitbox;
        public Rectangle bbHitbox;

        public Rectangle PlayerHitBox
        {
            get { return playerHitBox; }
        }

        public static float xPos;
        public static float yPos;
        bool IsPlayerHit = false;

        float timeSinceLastSprite = 0f;
        private float attackCounter = 0f;

        public KeyboardState currKeyboardState;
        public KeyboardState prevKeyboardState;
        private Vector2 _position;
        List<Bullet> bullets = new List<Bullet>();
        List<BossBullet> bBullets = new List<BossBullet>();

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

            var bulletHitBox = bHitbox;
            var bullet = new Bullet(this.Game);
            bulletHitBox = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, Bullet.Texture.Width, Bullet.Texture.Height);


            xPos = Position.X;
            yPos = Position.Y;

            for (int i = 0; i < EnemyManager.enemylist.Count; i++)
            {
                Rectangle enemyBox = EnemyManager.enemylist[i].Hitbox;
                if (playerHitBox.Intersects(enemyBox) && attacking == true)
                {
                    //EnemyManager.enemylist.RemoveAt(i);
                    Console.WriteLine("där fick han");
                }
                else if (playerHitBox.Intersects(enemyBox))
                {
                    IsPlayerHit = true;
                    EnemyManager.enemylist.RemoveAt(i);
                }

                for (int j = 0; j < bullets.Count; j++)
                {
                    Bullet b = bullets[j];

                    if (b.Hitbox.Intersects(enemyBox))
                    {
                        bullets.RemoveAt(j);
                        EnemyManager.enemylist.RemoveAt(i);
                    }
                    if (b.Hitbox.Intersects(Boss.bossHitBox))
                    {
                        Boss.bossHealth -= 2;
                        bullets.RemoveAt(j);

                    }
                }
            }

            bBullets = Boss.bossBullets;
            for (int l = 0; l < bBullets.Count; l++)
            {
                BossBullet b = bBullets[l];

                if (b.Hitbox.Intersects(playerHitBox))
                {
                    IsPlayerHit = true;
                }
            }

            var bossBox = Boss.bossHitBox;


            if (playerHitBox.Intersects(bossBox))
            {
                IsPlayerHit = true;
            }


            if (IsPlayerHit)
            {
                playerHealth -= 10;
            }

            foreach (var b in bullets)
                b.Update(gameTime);

            if (currKeyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space) && !attacking && !IsDead)
            {
                attacking = true;
            }



            if (currKeyboardState.IsKeyDown(Keys.E) && prevKeyboardState.IsKeyUp(Keys.E) && !IsDead)
            {

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
                        playerHealth -= 10;
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


            if (attacking)
            {
                attackCounter += (float)gameTime.ElapsedGameTime.TotalMilliseconds;



                Console.WriteLine(attackCounter);
                //if (attackCounter > 500)
                //{
                //    attacking = false;
                //    attackCounter = 0;

                //}
                //else
                //{

                if (direction > 0)
                {
                    currentTexture = playerAttackTexture;

                    if (attackCounter > 200)
                    {
                        attacking = false;

                    }
                }
                else
                {
                    currentTexture = playerAttackLeftTexture;

                    if (attackCounter > 200)
                    {
                        attacking = false;

                    }
                }

                //}
                if (attacking == false)
                    attackCounter = 0;

            }



            if (playerHealth <= 0)
            {
                IsDead = true;
            }
            else
            {
                IsDead = false;
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
