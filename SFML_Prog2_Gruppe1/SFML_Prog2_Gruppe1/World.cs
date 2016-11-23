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
using System.Xml;

namespace SFML_Prog2_Gruppe1
{
    public class World
    {
        private List<Tile[,]> world;
        private int current;

        public Tile[,] CurrentRoom
        {
            get { return world[current]; }
        }

        public World()
        {
            world = new List<Tile[,]>();
            current = 0;
            InitializeWorld();
        }

        private void InitializeWorld()
        {
            XmlDocument document = new XmlDocument();
            document.Load("World.xml");


            XmlNode worldNode = document.SelectSingleNode("/World");

            foreach (XmlNode node in worldNode.ChildNodes)
            {
                Tile[,] tempWorld = new Tile[40, 20];
                String tempRoom = node.InnerText;
                String[] tempLines = tempRoom.Split("\r\n".ToCharArray());
                List<String> finalLines = new List<string>();

                for(int i = 0; i < tempLines.Length; i++)
                {
                    if (tempLines[i] == "" || tempLines[i] == " " || tempLines[i] == "  ") { }
                    else
                    {
                        while(true)
                        {
                            if (tempLines[i].StartsWith(" "))
                            {
                                tempLines[i] = tempLines[i].Remove(0, 1);
                            }
                            else { break; }
                        }
                        finalLines.Add(tempLines[i]);
                    }
                }
                

                int y = 0;
                
                for (int i = 0; i < finalLines.Count; i++)
                {
                    String[] IDs = finalLines[i].Split(' ');

                    for(int x = 0; x < IDs.Length; x++)
                    {
                        switch (IDs[x])
                        {
                            case "00":
                                tempWorld[x, y] = new NormalTile(new Vector2f(x * Tile.Width, y * Tile.Height));
                                break;
                            case "01":
                                tempWorld[x, y] = new CollisionTile(new Vector2f(x * Tile.Width, y * Tile.Height));
                                break;
                        }
                    }
                    y++;
                }
                
                world.Add(tempWorld);
            }
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
