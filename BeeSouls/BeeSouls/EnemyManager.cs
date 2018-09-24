using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;


namespace BeeSouls
{
    class EnemyManager: GameObject
    {
        Player player;
        public int EnemyCount { get; set; }
        public int MaxEnemies { get; set; }
        public int SpawnCounter = 0;
        public float SpawnInterval { get; set; }
        private float difference = 0;
        private Rectangle SnakeHitBox, SnailHitBox, SpiderHitBox, WormHitBox, FlyHitBox, enemyhitbox;

        public Rectangle flyHitBox 
        {
            get { return FlyHitBox; }
        }

        private List<Texture2D> _texturesGround;
        private List<Texture2D> _texturesAir;
        
        public static List<Enemy> enemylist = new List<Enemy>();
        public EnemyManager(ContentManager content)
        {
            EnemyCount = 5;
            MaxEnemies = 15;
            
            
            SpawnInterval = 2500;
        }
        public void Loadcontent(ContentManager Content)
        {
            _texturesGround = new List<Texture2D>()
            {
            Content.Load<Texture2D>("enemy/snake"),
            Content.Load<Texture2D>("enemy/snail"),
            Content.Load<Texture2D>("enemy/spider"),
            Content.Load<Texture2D>("enemy/worm"),
            };
            
            _texturesAir = new List<Texture2D>()
            { 
            Content.Load<Texture2D>("enemy/fly"),
            };
            enemylist = CreateEnemies(EnemyCount);
        }
        public void Update(GameTime gameTime)
        {
            Texture2D CurrentSnakeTexture = _texturesGround[1];
          //  SnakeHitBox = new Rectangle((int)Position.X, (int)Position.Y, CurrentSnakeTexture.Width, CurrentSnakeTexture.Height);

            Texture2D CurrentSnailTexture = _texturesGround[1];
          //  SnailHitBox = new Rectangle((int)Position.X, (int)Position.Y, CurrentSnakeTexture.Width, CurrentSnakeTexture.Height);

            Texture2D CurrentSpiderTexture = _texturesGround[1];
          //  SpiderHitBox = new Rectangle((int)Position.X, (int)Position.Y, CurrentSnakeTexture.Width, CurrentSnakeTexture.Height);

            Texture2D CurrentWormTexture = _texturesGround[1];
          //  WormHitBox = new Rectangle((int)Position.X, (int)Position.Y, CurrentSnakeTexture.Width, CurrentSnakeTexture.Height);

            Texture2D CurrentFlyTexture = _texturesAir[0];
          //  FlyHitBox = new Rectangle((int)Position.X, (int)Position.Y, CurrentSnakeTexture.Width, CurrentSnakeTexture.Height);
            // lägg till tid på spawncounter
            SpawnCounter += gameTime.ElapsedGameTime.Milliseconds;
            
            //Kolla om spawncounter >= spawninterval
            //Om det är det, spawna en fiende och sätt spawncounter till noll
            if (SpawnCounter >= SpawnInterval && EnemyCount < MaxEnemies)     
            {

                
                    
                  
            }

            foreach(Enemy ee in enemylist)
            {
                //difference = player.Position.X - ee.Position.X;
                //if (difference > 850)
                //{ 
                    ee.Position += ee.Velocity;
                //}
            }
          //  Debug.WriteLine(EnemyCount);
           
        }
        
        Random enemyrandom = new Random();
        private List<Enemy> CreateEnemies(int enemyCount)
        {
            List<Enemy> _enemyList = new List<Enemy>();

            //_enemylist.Add(new Enemy(EnemyType.bat, _textures[enemyrandom.Next(0, 5)], 20, new Vector2(enemyrandom.Next(0, 200), enemyrandom.Next(0, 200))));
            
            for (int i = 0; i < 3; i++)
            {
                ////ground enemies
                //if (enemyhitbox.Intersects(enemyhitbox))//snake
                //{
                   _enemyList.Add(new Enemy(EnemyType.snake, _texturesGround[0], 20, new Vector2(enemyrandom.Next(200, 2800), 865), 20, new Vector2(0, 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[0].Width, _texturesGround[0].Height);
                //}

                //if (enemyhitbox.Intersects(enemyhitbox))//spider
                //{
                   _enemyList.Add(new Enemy(EnemyType.spider, _texturesGround[1], 20, new Vector2(enemyrandom.Next(200, 2800), 865), 25, new Vector2(0, 0)));
                   enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[1].Width, _texturesGround[1].Height);
                //    //Debug.WriteLine("ny enemy spawnades");
                //}

                //if (enemyhitbox.Intersects(enemyhitbox))//snail
                //{
                    _enemyList.Add(new Enemy(EnemyType.snail, _texturesGround[2], 20, new Vector2(enemyrandom.Next(200, 2800), 865), 10, new Vector2(0, 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[3].Width, _texturesGround[2].Height);
                //}

                //if (enemyhitbox.Intersects(enemyhitbox))//worm
                //{
                   _enemyList.Add(new Enemy(EnemyType.worm, _texturesGround[3], 20, new Vector2(enemyrandom.Next(200, 2800), 865), 10, new Vector2(0, 0)));
                   enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[3].Width, _texturesGround[3].Height);
                //}

                ////air enemies
                //if (enemyhitbox.Intersects(enemyhitbox))
                //{
                    _enemyList.Add(new Enemy(EnemyType.fly, _texturesAir[enemyrandom.Next(0, 1)], 20, new Vector2(enemyrandom.Next(200, 2800), enemyrandom.Next(0, 865)), 20, new Vector2(0, 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesAir[0].Width, _texturesAir[0].Height);
                //}
            }

            return _enemyList;
        }
        
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Enemy e in enemylist)
            {
                e.Draw(spritebatch);

            }
        }
    }
}
