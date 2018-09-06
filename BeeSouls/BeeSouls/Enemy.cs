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
                //case EnemyType.worm:
                //    Position = new Vector2(0, 0);
                //    EnemyGraphics = enemy/worm;
                //    EnemyDamage = 1;
                //    break;
                //case EnemyType.fly:
                //    Position = new Vector2(0, 0);
                //    EnemyGraphics = "enemy/fly";
                //    EnemyDamage = 1;
                //    break;
                //case EnemyType.snail:
                //    Position = new Vector2(0, 0);
                //    EnemyGraphics = "enemy/snail";
                //    EnemyDamage = 1;
                //    break;
                //case EnemyType.spider:
                //    Position = new Vector2(0, 0);
                //    EnemyGraphics = "enemy/spider";
                //    EnemyDamage = 1;
                //    break;
                //case EnemyType.bat:
                //    Position = new Vector2(0, 0);
                //    EnemyGraphics = "enemy/bat";
                //    EnemyDamage = 1;
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