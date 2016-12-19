using SFML.System;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    /// <summary>
    /// Spawnclass for holding a position to spawn items and enemies. Also contains a isUsed field so multiple items and enemies are not spawned at the same position.
    /// </summary>
    public class Spawn
    {
        private Vector2f position;
        private bool isUsed;

        /// <summary>
        /// Getter and setter for the isUsed field.
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }

        /// <summary>
        /// Getter and setter for the position.
        /// </summary>
        public Vector2f Position
        {
            get { return position; }
        }

        /// <summary>
        /// Constructor for new spawn with position.
        /// </summary>
        /// <param name="position">Position of the spawn.</param>
        public Spawn(Vector2f position)
        {
            this.position = position;
            isUsed = false;
        }
    }
}
