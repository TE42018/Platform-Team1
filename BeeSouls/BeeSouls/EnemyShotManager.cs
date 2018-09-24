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
    class EnemyShotManager : GameObject
    {
        private List<Texture2D> _texturesShot;

        List<EnemyShot> enemyShots = new List<EnemyShot>();


        public void Loadcontent(ContentManager Content)
        {

            _texturesShot = new List<Texture2D>()
             {
                 Content.Load<Texture2D>("shots/laserblueburst"),
                 Content.Load<Texture2D>("shots/laserrgreenburst"),
                 Content.Load<Texture2D>("shots/laserredburst"),
                 Content.Load<Texture2D>("shots/laseryellowburst"),
             };
        }


        float shoot = 0;

        public void Update(GameTime gameTime)
        {
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
                {
                    enemyShot.isVisible = false;
                }
                for (int i = 0; i < enemyShots.Count; i++)
                {
                    if (!enemyShots[i].isVisible)
                    {
                        enemyShots.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public void EnemyShootBullets()
        {
            EnemyShot newEnemyShot = new EnemyShot(_texturesShot[0]);
            newEnemyShot.EnemyShootVelocity.X = Velocity.X - 3f;
            newEnemyShot.EnemyShotPosition = new Vector2(Position.X + newEnemyShot.EnemyShootVelocity.X, Position.Y + (_texturesShot[0].Height / 2) - (_texturesShot[0].Height / 2));

            newEnemyShot.isVisible = true;
            if (enemyShots.Count < 3)
            {
                enemyShots.Add(newEnemyShot);
            }

        }

        public void Draw(SpriteBatch spritebatch)
        {

            foreach (EnemyShot enemyShot in enemyShots)
                spritebatch.Draw(enemyShot.ShotTexture, new Vector2(Position.X + TileEngine.CameraOffset.X, Position.Y + TileEngine.CameraOffset.Y), Color.White);
        }
    }
}