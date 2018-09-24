using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSouls
{
    class EnemyShot : GameObject
    {
        public Vector2 EnemyShotPosition;
        public Vector2 EnemyShootVelocity;
        public Texture2D ShotTexture { get; set; }


        public EnemyShot(Texture2D shotTexture)
        {
            ShotTexture = shotTexture;
            isVisible = false;

        }
    }
}
