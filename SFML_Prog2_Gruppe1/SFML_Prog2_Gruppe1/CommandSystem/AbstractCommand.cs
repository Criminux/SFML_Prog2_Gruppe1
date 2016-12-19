using SFML_Prog2_Gruppe1.CharacterSystem;

namespace SFML_Prog2_Gruppe1.CommandSystem
{
    /// <summary>
    /// Blueprint for player commands.
    /// </summary>
    public abstract class AbstractCommand
    {
        /// <summary>
        /// An instance of this method is called to execute commands.
        /// </summary>
        public abstract void Execute(Player player);
    }
}
