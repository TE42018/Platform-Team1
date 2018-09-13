using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSouls
{
    class EnemyShot :GameObject
    {
        private List<Texture2D> _texturesShot;
        
        List<EnemyShot> enemyShots = new List<EnemyShot>();
        private Texture2D ShotTexture;
        Vector2 shotVelocity = new Vector2();
        float shoot = 0;
        public EnemyShot(Texture2D shotTexture)
        {
           
            ShotTexture = shotTexture;
        }

        public void Loadcontent(ContentManager Content)
        {
            _texturesShot = new List<Texture2D>()
            {
                Content.Load<Texture2D>("shots/laserBlueBurst"),
                Content.Load<Texture2D>("shots/laserGreenBurst"),
                Content.Load<Texture2D>("shots/laserRedBurst"),
                Content.Load<Texture2D>("shots/laserYellowBurst"),
            };
        }
        public void Update(GameTime gameTime)
        {
            Position += Velocity;
            shoot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shoot > 1)
            {
                shoot = 0;
                EnemyShootBullets();
            }
            UpdateBullet();
        }
        public void UpdateBullet()
        {
            foreach (EnemyShot enemyShot in enemyShots)
            {
                enemyShot.Position += enemyShot.Velocity;
                if (enemyShot.Position.X < 0)
                    enemyShot.isVisible = false;
            }
            for (int i = 0; i < enemyShots.Count; i++)
                if (!enemyShots[i].isVisible)
                {
                    enemyShots.RemoveAt(i);
                    i--;
                }
        }
        
        public void EnemyShootBullets()
        { 
            EnemyShot newEnemyShot = new EnemyShot(ShotTexture);
            newEnemyShot.shotVelocity.X = Velocity.X - 3f;
            newEnemyShot.Position = new Vector2(Position.X + newEnemyShot.Velocity.X, Position.Y + (ShotTexture.Height / 2) - (ShotTexture.Height / 2));

            newEnemyShot.isVisible = true;
            if(enemyShots.Count() < 3)
            {
                enemyShots.Add(newEnemyShot);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
                foreach (EnemyShot enemyshot in enemyShots)
                    enemyshot.Draw(spriteBatch);
                spriteBatch.Draw(ShotTexture, Position, Color.White);
        }
    }
}   

