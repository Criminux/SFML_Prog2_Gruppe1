using SFML_Prog2_Gruppe1.CharacterSystem;

namespace SFML_Prog2_Gruppe1.CommandSystem
{
    class Attack : AbstractCommand
    {
        /// <summary>
        /// This command triggers an attack from the player.
        /// </summary>
        /// <param name="player">
        /// Command is applied to player.
        /// </param>
        public override void Execute(Player player)
        {
            player.Attack();
        }
    }
}
