﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace SFML_Prog2_Gruppe1.States.Menu
{
    class Button
    {
        private Texture texture;
        private Texture hoverTexture;

        private Sprite buttonSprite;

        private Vector2f position;

        public Vector2f Position
        {
            get { return position; }
            set
            {
                position = value;
                bounds.Left = (int)position.X;
                bounds.Top = (int)position.Y;
            }
        }

        private bool isSelected;
        private Text buttonText;
        private Font font;
        private string text;
        FloatRect bounds;

        //Click Event
        public event EventHandler<EventArgs> Click;

        public Button(Vector2f position, string text)
        {
            this.position = position;

            texture = new Texture("States/Menu/Button.png");
            hoverTexture = new Texture("States/Menu/ButtonPressed.png");
            font = new Font("States/Menu/POORICH.TTF");

            buttonSprite = new Sprite(texture);
            buttonSprite.Position = position;

            this.text = text;
            buttonText = new Text(text, font);
            bounds = buttonSprite.GetGlobalBounds();
            buttonText.Position = new Vector2f(bounds.Left + bounds.Width / 2, bounds.Top + bounds.Height / 2);
        }

        public void Update(bool isSecected, bool clicked)
        {
            //Set isHoveredOver bool
            this.isSelected = isSecected;

            if (isSecected) buttonSprite.Texture = hoverTexture;
            else buttonSprite.Texture = texture;

            if (isSelected && clicked)
            {
                OnClick();
            }


        }

        public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(buttonSprite);

            //TODO: Add StringDrawing
            //spriteBatch.DrawString(spriteFont, text, position + new Vector2(texture.Width / 2f, texture.Height / 2f) - spriteFont.MeasureString(text) / 2f, Color.Black);
            ProjectRenderWindow.GetRenderWindowInstance().Draw(buttonText);
        }

        protected void OnClick()
        {
            Click(this, new EventArgs());
        }

    }
}
