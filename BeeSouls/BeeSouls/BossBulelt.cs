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
    class BossBullet : DrawableGameComponent, IMovingObjects
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle Hitbox { get; set; }
        public Point Size { get; set; }
        public static Texture2D bbTexture { get; set; }


        public BossBullet(Game game) : base(game)
        {
            Size = new Point(bbTexture.Width, bbTexture.Height);
        }

        public new void LoadContent()
        {
            bbTexture = Game.Content.Load<Texture2D>("boss/bulletboi");
        }


        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bbTexture, new Vector2(Position.X + TileEngine.CameraOffset.X, Position.Y + TileEngine.CameraOffset.Y), Color.White);
        }
    }
}
