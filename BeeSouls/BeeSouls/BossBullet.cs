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
    class BossBullet :  IMovingObjects
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle Hitbox { get; set; }
        public Point Size { get; set; }
        public static Texture2D BossBulletTexture { get; set; }
        public Vector2 bulletDirection = new Vector2(0, 0);
        public Rectangle bbHitBox;

        public BossBullet(Vector2 Pos, Vector2 BulletTarget)
        {
            Position = Pos;
            bulletDirection = BulletTarget - Pos;
            bulletDirection = Vector2.Normalize(bulletDirection);
            Size = new Point(BossBulletTexture.Width, BossBulletTexture.Height);
            bbHitBox = new Rectangle(Position.ToPoint(), Size);
            Velocity = bulletDirection * 10;
        }
        //public BossBullet(Vector2 Pos, Vector2 BulletTarget)
        //{
        //    Position = Pos;
        //    bulletDirection = BulletTarget - Pos;
        //    bulletDirection = Vector2.Normalize(bulletDirection);
        //    bbHitBox = new Rectangle((int)Position.X, (int)Position.Y, BossBulletTexture.Width, BossBulletTexture.Height);
        //}

        public void Update(GameTime gameTime)
        {
            Position += Velocity;
            Hitbox = new Rectangle(Position.ToPoint(), Size);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BossBulletTexture, new Vector2(Position.X + TileEngine.CameraOffset.X, Position.Y + TileEngine.CameraOffset.Y), Color.White);
        }
    }
}
