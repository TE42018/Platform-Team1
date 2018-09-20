using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BeeSouls
{
    class TileEngine : GameComponent
    {
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int[,] MapData { get; set; }
        public List<Rectangle> Hitboxes { get; set; }
        public Texture2D TileMap { get; set; }
        public Vector2 CameraPosition { get; set; }
        public Vector2 min;
        public Vector2 max;

        public static Vector2 CameraOffset { get; set; }

        private int viewportWidth, viewportHeight;

        public override void Initialize()
        {
            viewportWidth = Game.GraphicsDevice.Viewport.Width;
            viewportHeight = Game.GraphicsDevice.Viewport.Height;


            base.Initialize();
        }

        public TileEngine(Game game) : base(game)
        {
            game.Components.Add(this);
            CameraPosition = Vector2.Zero;
            Hitboxes = new List<Rectangle>();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (MapData == null || TileMap == null)
                return;

            int screenCenterX = viewportWidth / 2;
            int screenCenterY = viewportHeight / 2;
            min = new Vector2(screenCenterX, screenCenterY);
            max = new Vector2(MapData.GetLength(1)*TileWidth - screenCenterX, MapData.GetLength(0)*TileHeight - screenCenterY);


            CameraPosition = Vector2.Clamp(CameraPosition, min, max);
            CameraOffset = new Vector2(-CameraPosition.X + screenCenterX, -CameraPosition.Y + screenCenterY);

            int startX = (int) ((CameraPosition.X - screenCenterX) / TileWidth);
            int startY = (int) ((CameraPosition.Y - screenCenterY) / TileHeight);

            int endX = (int) (startX + viewportWidth / TileWidth) + 1;
            int endY = (int) (startY + viewportHeight / TileHeight) + 1;

            if (startX < 0)
                startX = 0;
            if (startY < 0)
                startY = 0;

            Vector2 position = Vector2.Zero;
            int tilesPerLine = TileMap.Width / TileWidth;

            for (int y = startY; y < MapData.GetLength(0) && y <= endY; y++)
            {
                for (int x = startX; x < MapData.GetLength(1) && x <= endX; x++)
                {
                    position.X = (x * TileWidth - CameraPosition.X + screenCenterX);
                    position.Y = (y * TileHeight - CameraPosition.Y + screenCenterY);

                    int index = MapData[y, x];
                    Rectangle tileGfx = new Rectangle((index % tilesPerLine) * TileWidth,
                        (index / tilesPerLine) * TileHeight, TileWidth, TileHeight);

                    spriteBatch.Draw(TileMap,
                        position, tileGfx, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);

                }
            }
        }

        public CollisionData CheckCollision(Rectangle hitBox)
        {
            int startX = (int) (hitBox.Left / TileWidth);
            int startY = (int) ((hitBox.Top) / TileHeight);

            int endX = (int) (hitBox.Right / TileWidth);
            int endY = (int) (hitBox.Bottom / TileHeight); 

            var rect = new CollisionData();

            for (int y = startY; y < MapData.GetLength(0) && y <= endY; y++)
            {
                for (int x = startX; x < MapData.GetLength(1) && x <= endX; x++)
                {
                    if (MapData[y, x] == 2 || MapData[y, x] == 1 || MapData[y, x] == 3 || MapData[y, x] == 4 || MapData[y, x] == 9 || MapData[y, x] == 10)
                    {
                        var tmp = new Rectangle(x*TileWidth, y*TileHeight, TileWidth, TileHeight);
                        var intersect = Intersection(hitBox, tmp);
                        return new CollisionData()
                        {
                            Area = intersect,
                            Tile = MapData[y, x]
                        };
                    }
                }
            }

            return rect;
        }

        public static Rectangle Intersection(Rectangle r1, Rectangle r2)
        {
            int x1 = Math.Max(r1.Left, r2.Left);
            int y1 = Math.Max(r1.Top, r2.Top);
            int x2 = Math.Min(r1.Right, r2.Right);
            int y2 = Math.Min(r1.Bottom, r2.Bottom);

            if ((x2 >= x1) && (y2 >= y1))
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return Rectangle.Empty;
        }
    }
}