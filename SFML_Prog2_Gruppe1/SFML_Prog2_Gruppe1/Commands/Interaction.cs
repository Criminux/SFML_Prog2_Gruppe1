using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1
{
    class Interaction : AbstractCommand
    {
        public Interaction(int finishedQuests)
        {
            switch (finishedQuests)
            {
                case 0:
                    { 
                        // Spawn a quest item
                    }
                    break;

                case 1:
                    {
                        // Spawn a quest enemy
                    }
                    break;
            }
        }

        public override void Execute(Player player)
        {
            player.GetNewQuest();
        }
    }
}
