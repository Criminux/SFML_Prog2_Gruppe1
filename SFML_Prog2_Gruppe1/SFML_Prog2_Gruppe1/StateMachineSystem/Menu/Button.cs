using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
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

            texture = new Texture("StateMachineSystem/Menu/Button.png");
            hoverTexture = new Texture("StateMachineSystem/Menu/ButtonPressed.png");
            font = new Font("StateMachineSystem/Menu/POORICH.TTF");

            buttonSprite = new Sprite(texture);
            buttonSprite.Position = position;

            this.text = text;
            buttonText = new Text(text, font, 30);
            bounds = buttonSprite.GetGlobalBounds();
            buttonText.Position = new Vector2f(position.X + buttonSprite.GetGlobalBounds().Width / 2f, position.Y + buttonSprite.GetGlobalBounds().Height / 2f) - new Vector2f(buttonText.GetGlobalBounds().Width / 2f, buttonText.GetGlobalBounds().Height / 2f);
            //buttonText.Position = new Vector2f(bounds.Left + bounds.Width / 2, bounds.Top + bounds.Height / 2);
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
            ProjectRenderWindow.GetRenderWindowInstance().Draw(buttonText);
        }

        protected void OnClick()
        {
            Click(this, new EventArgs());
        }

    }
}
