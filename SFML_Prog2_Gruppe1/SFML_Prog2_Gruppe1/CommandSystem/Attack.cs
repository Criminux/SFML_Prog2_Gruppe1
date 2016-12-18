using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML_Prog2_Gruppe1.CharacterSystem;

namespace SFML_Prog2_Gruppe1.CommandSystem
{
    class Attack : AbstractCommand
    {
        public override void Execute(Player player)
        {
            player.Attack();
        }
    }
}
