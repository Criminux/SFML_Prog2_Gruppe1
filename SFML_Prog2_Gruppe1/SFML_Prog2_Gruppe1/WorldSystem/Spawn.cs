using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    public class Spawn
    {
        private Vector2f position;
        private bool isUsed; 

        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }

        public Vector2f Position
        {
            get { return position; }
        }

        public Spawn(Vector2f position)
        {
            this.position = position;
            isUsed = false;
        }
    }
}
