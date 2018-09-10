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
    class PlayerAttack : DrawableGameComponent, IMovingObjects
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

       
        public static bool IsAttacking = false;

        Texture2D bulletTexture;
        Rectangle bulletHitBox;

        public PlayerAttack(Game game) : base(game) 
        {
             
        }

        public override void Update(GameTime gameTime)
        {
            
            
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {

            
            base.LoadContent();
        }

    }
}
