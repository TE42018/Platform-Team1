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
        public float MaxZoom { get; set; }
        public float MinZoom { get; set; }


        private float zoom;
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom > MaxZoom)
                    zoom = MaxZoom;
                if (zoom < MinZoom)
                    zoom = MinZoom;
            }
        }

        private int viewportWidth, viewportHeight;

        public override void Initialize()
        {
            viewportWidth = Game.GraphicsDevice.Viewport.Width;
            viewportHeight = Game.GraphicsDevice.Viewport.Height;

            MaxZoom = 4.0f;
            MinZoom = 0.5f;
            MaxZoom = 1.0f;

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

            int screenCenterX = viewportWidth / 5;
            int screenCenterY = viewportHeight / 5;

            float zoomTileWidth = (TileWidth * Zoom);
            float zoomTileHeight = (TileHeight * Zoom);
            Vector2 zoomCameraPosition = CameraPosition * Zoom;

            int startX = (int) ((CameraPosition.X - screenCenterX) / zoomTileWidth);
            int startY = (int) ((CameraPosition.Y - screenCenterY) / zoomTileHeight);

            int endX = (int) (startX + viewportWidth / zoomTileWidth) + 1;
            int endY = (int) (startY + viewportHeight / zoomTileHeight) + 1;

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
                    position.X = (x * zoomTileWidth - zoomCameraPosition.X + screenCenterX);
                    position.Y = (y * zoomTileHeight - zoomCameraPosition.Y + screenCenterY);

                    int index = Data[y, x];
                    Rectangle tileGfx = new Rectangle((index % tilesPerLine) * TileWidth,
                        (index / tilesPerLine) * TileHeight, TileWidth, TileHeight);

                    spriteBatch.Draw(TileMap,
                        position, tileGfx, Color.White, 0f, Vector2.Zero,zoom, SpriteEffects.None, 0f);

                }
            }
        }
    }
}