using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.CharacterSystem;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    /// <summary>
    /// Manages UI for health, quest and stats.
    /// </summary>
    public class UIManager
    {
        private Texture uiBar;
        private Sprite sprite;

        private Texture fullHeart;
        private Texture emptyHeart;

        private int health;
        private List<Sprite> heartSprites;

        private float playerLevel;
        private Font font;
        private Text PlayerSpeed;
        private string message;

        /// <summary>
        /// UI Manager constructor
        /// </summary>
        public UIManager()
        {
            uiBar = new Texture("StateMachineSystem/UI/UIBar.png");
            sprite = new Sprite(uiBar);
            sprite.Position = new Vector2f(0, 640);

            font = new Font("StateMachineSystem/Menu/POORICH.TTF");
            PlayerSpeed = new Text(message, font, 20);
            PlayerSpeed.Position = new Vector2f(860, 685);

            fullHeart = new Texture("StateMachineSystem/UI/FullHeart.png");
            emptyHeart = new Texture("StateMachineSystem/UI/EmptyHeart.png");

            heartSprites = new List<Sprite>();

            heartSprites.Add(new Sprite());
            heartSprites[0].Position = new Vector2f(16, 680);
            heartSprites.Add(new Sprite());
            heartSprites[1].Position = new Vector2f(80, 680);
            heartSprites.Add(new Sprite());
            heartSprites[2].Position = new Vector2f(144, 680);
            heartSprites.Add(new Sprite());
            heartSprites[3].Position = new Vector2f(208, 680);
            heartSprites.Add(new Sprite());
            heartSprites[4].Position = new Vector2f(272, 680);
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="health">Health of the Player.</param>
        public void Update(int health, float speed)
        {
            this.health = health;
            playerLevel = speed - 4;

            if (health >= 1) heartSprites[0].Texture = fullHeart;
            else heartSprites[0].Texture = emptyHeart;
            if (health >= 2) heartSprites[1].Texture = fullHeart;
            else heartSprites[1].Texture = emptyHeart;
            if (health >= 3) heartSprites[2].Texture = fullHeart;
            else heartSprites[2].Texture = emptyHeart;
            if (health >= 4) heartSprites[3].Texture = fullHeart;
            else heartSprites[3].Texture = emptyHeart;
            if (health >= 5) heartSprites[4].Texture = fullHeart;
            else heartSprites[4].Texture = emptyHeart;

            message = "Player Level: " + playerLevel.ToString();
            PlayerSpeed.DisplayedString = message;
        }

        /// <summary>
        /// Draws the UI.
        /// </summary>
        public void Draw(Quest quest)
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);

            foreach (Sprite tempSprite in heartSprites)
            {
                ProjectRenderWindow.GetRenderWindowInstance().Draw(tempSprite);
            }

            ProjectRenderWindow.GetRenderWindowInstance().Draw(PlayerSpeed);

            if (quest != null) quest.Draw();

        }
    }
}
