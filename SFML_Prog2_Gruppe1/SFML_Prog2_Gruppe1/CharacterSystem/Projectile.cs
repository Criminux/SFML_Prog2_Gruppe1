using SFML.System;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    /// <summary>
    /// Blueprint for all projectiles.
    /// </summary>
    public class Projectile
    {
        private Texture projectileTexture;
        private Sprite projectileSprite;

        private Vector2f position;
        private Vector2f velocity;

        private Clock destructionTimer;

        /// <summary>
        /// Property to get the timer which destroys the projectiles.
        /// </summary>
        public Clock DestructionTimer
        {
            get { return destructionTimer; }
        }

        /// <summary>
        /// Property to get the bounds of the projectile.
        /// </summary>
        public FloatRect Bounds
        {
            get { return projectileSprite.GetGlobalBounds(); }
        }


        /// <summary>
        /// Assigns correct texture, position and velocity.
        /// </summary>
        /// <param name="position">Position of the projectile.</param>
        /// <param name="velocity">Velocity of the projectile.</param>
        public Projectile(Vector2f position, Vector2f velocity)
        {
            projectileTexture = new Texture("CharacterSystem/Projectile.png");
            projectileSprite = new Sprite(projectileTexture);

            destructionTimer = new Clock();

            this.position = position;
            this.velocity = velocity;

            projectileSprite.Position = position;
        }

        /// <summary>
        /// Updates the velocity and position.
        /// </summary>
        public void Update()
        {
            position += velocity;
            projectileSprite.Position = position;
        }

        /// <summary>
        /// Draws the projectile.
        /// </summary>
        public void Draw()
        {
                ProjectRenderWindow.GetRenderWindowInstance().Draw(projectileSprite);
        }
    }
}