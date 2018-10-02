using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BeeSouls
{
    class Boss : DrawableGameComponent, IMovingObjects
    {
        Texture2D currentTexture;
        Texture2D bossLeftTexture;
        Texture2D bossRightTexture;
        private int Speed = 2;
        public static Rectangle bossHitBox;
        List<BossBullet> bossBullets = new List<BossBullet>();
        float bulletTimer = 1000f;
        public Rectangle bbHitbox;
        

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public int bossDirection = 1;
        public Boss(Game game) : base(game)
        {
            Position =  new Vector2(300, 200);
        }


        protected override void LoadContent()
        {
            bossLeftTexture = Game.Content.Load<Texture2D>("boss/bossLeft");
            bossRightTexture = Game.Content.Load<Texture2D>("boss/boss");
            currentTexture = bossLeftTexture;

            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            Vector2 playerPos = new Vector2(Player.xPos, Player.yPos);
            Vector2 direction = Vector2.Normalize(playerPos - Position);
            Velocity = direction * Speed;
            bossHitBox = new Rectangle((int)Position.X, (int)Position.Y, currentTexture.Width, currentTexture.Height);
            Position += Velocity;
            var origin = new Vector2(currentTexture.Width / 2f, currentTexture.Height / 2f);
            var bullet = new BossBullet(Vector2.Zero, Vector2.Zero);
            bulletTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            var bulletHitBox = bbHitbox;
            bulletHitBox = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, BossBullet.BossBulletTexture.Width, BossBullet.BossBulletTexture.Height);

            var target = new Vector2(Player.xPos, Player.yPos);
            var bulletOrigin = new Vector2(Position.X, Position.Y);
           
            

            if (bulletTimer <= 0f)
            {
                bulletTimer = 1200 - (int)gameTime.TotalGameTime.TotalSeconds * 20;
                bulletTimer = Math.Max(bulletTimer, 333);
                Console.WriteLine(bulletTimer);
                bossBullets.Add(new BossBullet(Position, playerPos));
            }
              
            currentTexture = bossLeftTexture;
            foreach (var b in bossBullets)
                b.Update(gameTime);
            base.Update(gameTime);
        }

       

        public void Draw(SpriteBatch spriteBatch)
        {    
            spriteBatch.Draw(currentTexture, new Rectangle((int)(Position.X + TileEngine.CameraOffset.X), (int)(Position.Y + TileEngine.CameraOffset.Y), currentTexture.Width, currentTexture.Height), Color.White);
            foreach (var b in bossBullets)
                b.Draw(spriteBatch);
        }
    }
}
