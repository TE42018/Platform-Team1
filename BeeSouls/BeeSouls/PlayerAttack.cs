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
    class PlayerAttack : DrawableGameComponent 
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
       
        public static bool IsAttacking = false;

        Texture2D bulletTexture;
        Rectangle bulletHitBox;
        public float bulletSpeed = 3f;

        public PlayerAttack(Game game) : base(game) 
        {
             
        }

     

        public void Shoot() 
        {
            //bulletList.Add(new Bullet(Game) { Position = BulletOrigin, Velocity });
          
            this.Velocity = new Vector2(bulletSpeed, 0);
            //his.Position = BulletOrigin;
            this.bulletHitBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, bulletTexture.Width, bulletTexture.Height);
            
        }

        public override void Update(GameTime gameTime)
        {
            
            
            
            base.Update(gameTime);
        }
         
        protected override void LoadContent()
        {

            bulletTexture = Game.Content.Load<Texture2D>("player/playerShot");
            
            base.LoadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
