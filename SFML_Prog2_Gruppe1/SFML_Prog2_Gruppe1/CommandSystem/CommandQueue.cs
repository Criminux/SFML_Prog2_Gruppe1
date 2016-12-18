using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1.CommandSystem
{
    public class CommandQueue
    {
        private Queue<AbstractCommand> movementQueue = new Queue<AbstractCommand>();

        /// <summary>
        /// Adds a Command to the queue.
        /// </summary>
        /// <param name="command">
        /// Command that is queued.
        /// </param>
        public void Push(AbstractCommand command)
        {
            movementQueue.Enqueue(command);
        }

        /// <summary>
        /// Command that returns the next movement command from movementQueue.
        /// </summary>
        /// <returns>
        /// The movement command.
        /// </returns>
        public AbstractCommand Pop()
        {
            return movementQueue.Dequeue();
        }

        /// <summary>
        /// Checks if the queue is empty of commands.
        /// </summary>
        /// <returns>
        /// If true, queue is empty.
        /// </returns>
        public bool IsEmpty()
        {
            if (movementQueue.Count == 0)
            {
                return true; 
            }
            else return false;
        }
    }
}
