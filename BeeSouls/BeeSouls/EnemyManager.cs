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
        public static int EnemyCount;
        public int MaxEnemies { get; set; }
        public int SpawnCounter = 0;
        public float SpawnInterval { get; set; }
        private float difference;
        private int SnakeCount, SnailCount, SpiderCount, WormCount, FlyCount;
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
            EnemyCount = 0;
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

                CreateEnemies(EnemyCount);
                    
                  
            }
            for (int j = 0; j < enemylist.Count; j++)
                {
                    
                    //difference = ppos - enemylist[j].Position.X;
                    //Console.WriteLine("P: "  + ppos);
                    //Console.WriteLine("E: " + enemylist[j].Position.X);
                    //if (difference < 200)
                    //{
                    if(enemylist[j].Position.X < Player.xpos)
                    { 
                        enemylist[j].Position -= enemylist[j].Velocity;
                    }
                    else
                    {
                    enemylist[j].Position += enemylist[j].Velocity;
                    }
                //    Console.WriteLine("difference is " + difference);
                //}
            }
            UpdateEnemy();

            //Debug.WriteLine(EnemyCount);

        }
        public void Collide(CollisionData data, string direction)
        {
            Rectangle overlap = data.Area;
            foreach(Enemy e in enemylist)
            { 
                if (overlap == Rectangle.Empty)
                {
                    return;
                }


                //if (overlap.Width > overlap.Height)
                if (direction == "height")
                {
                    //justera höjdled
                    if (enemyhitbox.Center.Y > overlap.Center.Y)
                    {
                        Position = new Vector2(Position.X, overlap.Bottom + 1);
                    }
                    else
                    {
                        Position = new Vector2(Position.X, overlap.Top - enemyhitbox.Height - 1);
                    }
                }
                if (direction == "width")
                {
                    //justera sidled
                    if (enemyhitbox.Center.X > overlap.Center.X)
                    {
                        Position = Position + new Vector2(overlap.Width, 0);
                    }
                    else
                    {
                        Position = Position + new Vector2(-overlap.Width - 1, 0);
                    }
                }
            }
        }
        public void UpdateEnemy()
        {
            foreach (Enemy enemy in enemylist)
            {
                //if (enemy.Position.X < 0)
                //{
                //    enemy.isVisible = false;
                //}
                //for (int i = 0; i < enemylist.Count; i++)
                //{
                //    if (!enemylist[i].isVisible)
                //    {
                //        enemylist.RemoveAt(i);
                //        i--;
                //        EnemyCount--;
                //    }
                //}
            }
        }
        Random enemyrandom = new Random();
        private List<Enemy> CreateEnemies(int enemyCount)
        {
            List<Enemy> _enemyList = new List<Enemy>();

            //_enemylist.Add(new Enemy(EnemyType.bat, _textures[enemyrandom.Next(0, 5)], 20, new Vector2(enemyrandom.Next(0, 200), enemyrandom.Next(0, 200))));
            
            for (int i = 0; i < 3; i++)
            {
                if(SnakeCount < 5)
                { 
                   _enemyList.Add(new Enemy(EnemyType.snake, _texturesGround[0], 20, new Vector2(enemyrandom.Next(200, 2800), 888), 20, new Vector2(enemyrandom.Next(-4,-1), 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[0].Width, _texturesGround[0].Height);
                    EnemyCount++;
                    SnakeCount++;
                }

                if(SpiderCount < 5)
                { 
                    _enemyList.Add(new Enemy(EnemyType.spider, _texturesGround[2], 20, new Vector2(enemyrandom.Next(200, 2800), 865), 25, new Vector2(enemyrandom.Next(-4, -1), 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[2].Width, _texturesGround[2].Height);
                    EnemyCount++;
                    SpiderCount++;
                }
                if(SnailCount < 5)
                { 
                    _enemyList.Add(new Enemy(EnemyType.snail, _texturesGround[1], 20, new Vector2(enemyrandom.Next(200, 2800), 872), 10, new Vector2(enemyrandom.Next(-4, -1), 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[1].Width, _texturesGround[1].Height);
                    EnemyCount++;
                    SnailCount++;
                }

                if (WormCount < 5)
                { 
                    _enemyList.Add(new Enemy(EnemyType.worm, _texturesGround[3], 20, new Vector2(enemyrandom.Next(200, 2800), 889), 10, new Vector2(enemyrandom.Next(-4, -1), 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesGround[3].Width, _texturesGround[3].Height);
                    EnemyCount++;
                    WormCount++;
                }

                if(FlyCount <= 5)
                {
                    _enemyList.Add(new Enemy(EnemyType.fly, _texturesAir[enemyrandom.Next(0, 1)], 20, new Vector2(enemyrandom.Next(200, 2800), enemyrandom.Next(0, 865)), 20, new Vector2(enemyrandom.Next(-5, -1), 0)));
                    enemyhitbox = new Rectangle((int)Position.X, (int)Position.Y, _texturesAir[0].Width, _texturesAir[0].Height);
                    EnemyCount++;
                    FlyCount++;
                }
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