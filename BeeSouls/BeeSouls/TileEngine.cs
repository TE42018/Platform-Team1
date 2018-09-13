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
        public int[,] Data { get; set; }
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
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Data == null || TileMap == null)
                return;

            int screenCenterX = viewportWidth / 2;
            int screenCenterY = viewportHeight / 2;
            min = new Vector2(screenCenterX, screenCenterY);
            max = new Vector2(2940 - screenCenterX, 1050 - screenCenterY); 


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

            for (int y = startY; y < Data.GetLength(0) && y <= endY; y++)
            {
                for (int x = startX; x < Data.GetLength(1) && x <= endX; x++)
                {
                    position.X = (x * TileWidth - CameraPosition.X + screenCenterX);
                    position.Y = (y * TileHeight - CameraPosition.Y + screenCenterY);

                    int index = Data[y, x];
                    Rectangle tileGfx = new Rectangle((index % tilesPerLine) * TileWidth,
                        (index / tilesPerLine) * TileHeight, TileWidth, TileHeight);

                    //tiles.Add(new Tile(position, tileTex));

                    //hitboxes.Add(new Rectangle(position.ToPoint(), TileWidth, TileHeight);

                    spriteBatch.Draw(TileMap,
                        position, tileGfx, Color.White, 0f, Vector2.Zero,1.0f, SpriteEffects.None, 0f);

                }
            }
        }
    }
}