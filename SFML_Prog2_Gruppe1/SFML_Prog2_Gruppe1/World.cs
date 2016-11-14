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
        List<Tile[,]> world;
        private int current;

        public World()
        {
            world = new List<Tile[,]>();
            current = 0;
            InitializeWorld();
        }

        private void InitializeWorld()
        {
            StreamReader reader = new StreamReader("World.txt");

            while(true)
            {
                world.Add(ReadPart(reader));
                if(world.Last()[0,0] == null)
                {
                    break;
                }
            }
        }

        private Tile[,] ReadPart(StreamReader reader)
        {
            Tile[,] tempWorld = new Tile[40, 20];

            String templine;
            int yCount = 0;
            while ((templine = reader.ReadLine()) != null)
            {
                String[] IDs = templine.Split(' ');

                for (int i = 0; i < IDs.Length; i++)
                {
                    switch (IDs[i])
                    {
                        case "00":
                            tempWorld[i, yCount] = new NormalTile(new Vector2f(i * 32, yCount * 32));
                            break;
                        case "01":
                            tempWorld[i, yCount] = new CollisionTile(new Vector2f(i * 32, yCount * 32));
                            break;
                    }
                }

                yCount++;
            }

            return tempWorld;
        }

        public void Draw()
        {
            foreach(Tile tempTile in world[current])
            {
                tempTile.Draw();
            }
        }
    }
}
