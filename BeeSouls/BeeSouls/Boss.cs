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
        public static Rectangle bossHitBox;
        List<Bullet> bossBullets = new List<Bullet>();
        float bulletTimer = 1000f; 

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
            bossHitBox = new Rectangle((int)Position.X, (int)Position.Y, currentTexture.Width, currentTexture.Height);
            Position += Velocity;
            var origin = new Vector2(currentTexture.Width / 2f, currentTexture.Height / 2f);
            var bullet = new Bullet(this.Game);
            bulletTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Position.X > Player.xPos)
            {
                Velocity = new Vector2(-2f, 0);
            }
            else if (Position.X < Player.xPos)
            {
                Velocity = new Vector2(2f, 0);
            }

            if (bulletTimer <= 0f)
            {
                bullet.Velocity = new Vector2(bossDirection, 0) * 8;
                bullet.Position = Position;
                bossBullets.Add(bullet);
                bulletTimer = 500;
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
