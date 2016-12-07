using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1
{
    public abstract class AbstractCommand
    {
        /// <summary>
        /// An instance of this method is called to execute commands.
        /// </summary>
        public abstract void Execute(Player player);
    }
}
