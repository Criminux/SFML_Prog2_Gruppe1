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
using System.IO;

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
            
            StreamReader reader = new StreamReader("World.txt");
            String templine;
            int yCount = 0;
            while((templine = reader.ReadLine())!= null)
            {
                String[] IDs = templine.Split(' ');

                for(int i = 0; i < IDs.Length; i++)
                {
                    switch(IDs[i])
                    {
                        case "00":
                            world[i, yCount] = new NormalTile(new Vector2f(i * 32, yCount * 32));
                            break;
                        case "01":
                            world[i, yCount] = new CollisionTile(new Vector2f(i * 32, yCount * 32));
                            break;
                    }
                }

                yCount++;
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
