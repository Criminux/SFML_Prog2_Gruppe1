using SFML.System;
using SFML_Prog2_Gruppe1.CharacterSystem;

namespace SFML_Prog2_Gruppe1.CommandSystem
{
    /// <summary>
    /// Concrete command to move inside a room.
    /// </summary>
    class PlayerMover : AbstractCommand
    {
        private Vector2f velocity;

        public PlayerMover(float x, float y)
        {
            velocity = new Vector2f(x, y);
        }

        /// <summary>
        /// An command of this class will move the player in the desired direction.
        /// </summary>
        /// <param name="player">
        /// Command is applied to player.
        /// </param>
        public override void Execute(Player player)
        {
            player.Velocity = velocity;
        }
    }
}
