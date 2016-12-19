using System;
using SFML.System;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    /// <summary>
    /// Class for adding buttons to every menu.
    /// </summary>
    class Button
    {
        private Texture texture;
        private Texture hoverTexture;
        private Sprite buttonSprite;
        private Vector2f position;

        /// <summary>
        /// Getter and setter for the button position.
        /// </summary>
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

        public event EventHandler<EventArgs> Click;

        /// <summary>
        /// Constructor for new button.
        /// </summary>
        /// <param name="position">Position for the button.</param>
        /// <param name="text">Text inside the button.</param>
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
        }

        /// <summary>
        /// Updates the button.
        /// </summary>
        /// <param name="isSelected">Is the button selected.</param>
        /// <param name="clicked">Is the button clicked.</param>
        public void Update(bool isSelected, bool clicked)
        {
            this.isSelected = isSelected;

            if (isSelected) buttonSprite.Texture = hoverTexture;
            else buttonSprite.Texture = texture;

            if (this.isSelected && clicked)
            {
                OnClick();
            }


        }

        /// <summary>
        /// Draws the button.
        /// </summary>
        public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(buttonSprite);
            ProjectRenderWindow.GetRenderWindowInstance().Draw(buttonText);
        }

        /// <summary>
        /// Clickevent for the button.
        /// </summary>
        protected void OnClick()
        {
            Click(this, new EventArgs());
        }

    }
}
