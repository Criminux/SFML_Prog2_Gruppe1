using SFML_Prog2_Gruppe1.CharacterSystem;

namespace SFML_Prog2_Gruppe1.CommandSystem
{
    /// <summary>
    /// Concrete command for receiving quests near NPCs.
    /// </summary>
    class Interaction : AbstractCommand
    {
        /// <summary>
        /// This command allows interaction with npc characters.
        /// </summary>
        /// <param name="player">
        /// Command is applied to player.
        /// </param>
        public override void Execute(Player player)
        {
            player.GetNewQuest();
        }
    }
}
