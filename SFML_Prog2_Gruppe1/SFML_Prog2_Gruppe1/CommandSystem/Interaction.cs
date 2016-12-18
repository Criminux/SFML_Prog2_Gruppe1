using SFML_Prog2_Gruppe1.CharacterSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1.CommandSystem
{
    class Interaction : AbstractCommand
    {
        public Interaction()
        {}

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
