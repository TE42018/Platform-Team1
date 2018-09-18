using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeSouls
{
    enum EnemyType
    {
        snake,
        fly,
        snail,
        spider,
        worm
    }

    class Enemy : GameObject
    {
        public EnemyType Type { get; private set; }
        public int EnemyDamage { get; set; }
        public Texture2D EnemyGraphics{ get; set; }
        public int Points { get; set; }
        
        public Enemy(EnemyType type, Texture2D enemyGraphics, int EnemyDamage, Vector2 _Position, int Points, Vector2 _Velocity)
        {
            Type = type;
            Position = _Position;
            Velocity = _Velocity;
            
            EnemyGraphics = enemyGraphics;
            switch (Type)
            {
                //case enemytype.worm:
                //    position = new vector2(0, 0);
                //    enemygraphics = enemy/worm;
                //    enemydamage = 1;
                //    break;
                //case enemytype.fly:
                //    position = new vector2(0, 0);
                //    enemygraphics = "enemy/fly";
                //    enemydamage = 1;
                //    break;
                //case enemytype.snail:
                //    position = new vector2(0, 0);
                //    enemygraphics = "enemy/snail";
                //    enemydamage = 1;
                //    break;
                //case enemytype.spider:
                //    position = new vector2(0, 0);
                //    enemygraphics = "enemy/spider";
                //    enemydamage = 1;
                //    break;
                //case enemytype.bat:
                //    position = new vector2(0, 0);
                //    enemygraphics = "enemy/bat";
                //    enemydamage = 1;
                //    break;
            }
        }
        public void Update(GameTime gameTime)
        {
           
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(EnemyGraphics, Position, Color.White);
        }
    }
}