﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeSouls
{
    //normal
    class Button
    {
        Rectangle posSize;
        bool clicked;
        bool available;
        Texture2D image;
        private SpriteFont _font;
        private Texture2D _texture;
        public Color PenColour { get; set; }

        public string Text;
        //Constructor
        public Button()
        {
            posSize = new Rectangle(100, 100, 100, 50);
            clicked = false;
            available = true;
        }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            PenColour = Color.Black;
        }
        //OverLoaded Constructor
        public Button(Rectangle rec, bool avail)
        {
            posSize = rec;
            available = avail;
            clicked = false;
        }

        //Load Content
        public void load(ContentManager content, string name)
        {
            image = content.Load<Texture2D>(name);
        }

        //Update
        public bool update(Vector2 mouse)
        {
            if (mouse.X >= posSize.X && mouse.X <= posSize.X + posSize.Width && mouse.Y >= posSize.Y && mouse.Y <= posSize.Y + posSize.Height)
            {
                clicked = true;
            }

            else
            {
                clicked = false;
            }

            if (!available)
            {
                clicked = false;
            }

            return clicked;


        }


        //Draw
        public void draw(SpriteBatch sp)
        {

            Color col = Color.White;

            if (!available)
            {
                col = new Color(50, 50, 50);
            }

            if (clicked)
            {
                col = Color.Green;
            }

            sp.Draw(image, posSize, col);

        }


        //Getters and Setters
        public bool Clicked
        {

            get { return clicked; }

            set { clicked = value; }

        }

        public bool Available
        {

            get { return available; }

            set { available = value; }

        }

        public Rectangle PosSize
        {

            get { return posSize; }

            set { posSize = value; }

        }

    }

}
