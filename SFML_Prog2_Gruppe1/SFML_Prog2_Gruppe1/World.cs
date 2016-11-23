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
        private List<Room> world;
        private int current;

        public Tile[,] CurrentRoom
        {
            get { return world[current].Tilemap; }
        }

        public World()
        {
            world = new List<Room>();
            current = 0;
            InitializeWorld();
        }

        private void InitializeWorld()
        {
            XmlDocument document = new XmlDocument();
            document.Load("World.xml");


            XmlNode worldNode = document.SelectSingleNode("/World");

            foreach (XmlNode roomNode in worldNode.ChildNodes)
            {
                Tile[,] tempWorld = new Tile[40, 20];
                String tempRoom = roomNode.InnerText;
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
                //TODO SAVE ID
                Room room = new Room();
                room.Tilemap = tempWorld;
                
                foreach(XmlAttribute attr in roomNode.Attributes)
                {
                    if (attr.Name == "id")
                        room.ID = Convert.ToInt32(attr.InnerText);
                }

                world.Add(room);
                
            }
        }

        public void Draw()
        {
            foreach(Tile tempTile in world[current].Tilemap)
            {
                tempTile.Draw();
            }
        }
    }
}
