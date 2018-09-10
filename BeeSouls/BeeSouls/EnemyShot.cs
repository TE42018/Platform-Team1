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

        public EnemyShot(Texture2D shotTexture)
        {
            ShotTexture = shotTexture;
        }

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
        public void UpdateBullet(GameTime gameTime)
        { 
        
            foreach (EnemyShot enemyShot in enemyShots)
            {
                enemyShot.Position += enemyShot.Velocity;
                if(enemyShot.Position.X < 0)
                {
                    enemyShot.isVisible = false;
                }
                for(int i = 0; i< enemyShots.Count; i++)
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
            EnemyShot newEnemyShot = new EnemyShot(ShotTexture);

        }
    }
}


