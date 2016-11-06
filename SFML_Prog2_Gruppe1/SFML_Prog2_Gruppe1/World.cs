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

namespace SFML_Prog2_Gruppe1
{
    public class World
    {
        Tile[,] world;


        public World()
        {
            world = new Tile[40,20];
            InitializeWorld();
        }

        private void InitializeWorld()
        {
            for (int x = 0; x < 40; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    world[x, y] = new NormalTile(new Vector2f(x * 32, y * 32));
                }
            }
        }

        public void Draw()
        {
            foreach(Tile tile in world)
            {
                tile.Draw();
            }
        }
    }
}
