using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    /// <summary>
    /// Handles all the animations necessary.
    /// </summary>
    public class Animation
    {
        private Texture spriteSheet;
        private Sprite sprite;
        private int currentFrame;
        private int animationSpriteCountX;
        private int animationSpriteCountY;
        private int animationTime;
        private float currentAnimationTime;
        private int animationSpriteHeight;
        private int animationSpriteWidth;
        private List<IntRect> animationSprites;
        private Clock animationClock;

        /// <summary>
        /// Property to get the character sprites for the animations.
        /// </summary>
        public Sprite Sprite
        {
            get { return sprite; }
        }

        /// <summary>
        /// Assigns the variables and has a animation clock to time the animations properly. Calls load method at the end.
        /// </summary>
        /// <param name="spriteSheet">Spritesheet for animations</param>
        /// <param name="countX">The amount of sprites on the x-axis</param>
        /// <param name="countY">The amount of sprites on the y-axis</param>
        /// <param name="width">The width of the spritesheet in pixel</param>
        /// <param name="height">The height of the spritesheet in pixel</param>
        /// <param name="time">Time for the animation</param>
        public Animation(Texture spriteSheet, int countX, int countY, int width, int height, int time)
        {
            this.spriteSheet = spriteSheet;
            this.animationSpriteCountX = countX;
            this.animationSpriteCountY = countY;
            this.animationSpriteWidth = width;
            this.animationSpriteHeight = height;
            this.animationTime = time;
            this.currentAnimationTime = 0.0f;
            this.currentFrame = 0;
            this.animationSprites = new List<IntRect>();

            animationClock = new Clock();
            animationClock.Restart();

            sprite = new Sprite();

            Load();
        }

        /// <summary>
        /// Loads the sprites from the spritesheet and assignes correct rectangles.
        /// </summary>
        protected void Load()
        {
            //Loads the sprites from the spritesheets
            IntRect rect = new IntRect();
            for (int y = 0; y < animationSpriteCountY; y++)
            {
                for (int x = 0; x < animationSpriteCountX; x++)
                {
                    rect.Left = animationSpriteWidth * x;
                    rect.Top = animationSpriteHeight * y;
                    rect.Width = animationSpriteWidth;
                    rect.Height = animationSpriteHeight;

                    animationSprites.Add(rect);
                }
            }
        }

        /// <summary>
        /// Updates the animation timer and increases the frame if time is right. Restarts the animation clock at the end.
        /// </summary>
        public void Update()
        {
            //Updates the characters in relation to the game time and the frames
            int deltaTime = animationClock.ElapsedTime.AsMilliseconds();

            currentAnimationTime -= deltaTime;

            if (currentAnimationTime <= 0.0f)
            {
                currentFrame++;

                if (currentFrame >= animationSprites.Count)
                {
                    currentFrame = 0;
                }
                currentAnimationTime = animationTime;
            }
            animationClock.Restart();
        }

        /// <summary>
        /// Draws the character with the correct sprites and on the correct position in the render window.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="InvertHorizontal"></param>
        public void Draw(Vector2f position)
        {
            sprite = new Sprite(spriteSheet, animationSprites[currentFrame]);
            sprite.Position = position;

            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
        }
    }
}
