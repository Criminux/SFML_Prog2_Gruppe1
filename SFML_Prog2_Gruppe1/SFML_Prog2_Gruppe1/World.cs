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
        private int currentID;

        //public Tile[,] CurrentRoom
        //{
        //    get { return world[current].Tilemap; }
        //}

        public Room GetRoomByID(int id)
        {
            foreach(Room room in world)
            {
                if (room.ID == id) return room;
            }
            return new Room();
        }
        public Room GetActiveRoom()
        {
            return GetRoomByID(currentID);
        }

        public int CurrentID
        {
            get { return currentID; }
            set { currentID = value; }
        }

        public World()
        {
            world = new List<Room>();
            currentID = 22;
            InitializeWorld();
        }

        private void InitializeWorld()
        {
            XmlDocument document = new XmlDocument();
            document.Load("World.xml");
            
            XmlNode worldNode = document.SelectSingleNode("/World");

            foreach (XmlNode roomNode in worldNode.ChildNodes)
            {

                Room room = new Room();

                foreach(XmlNode roomChild in roomNode.ChildNodes)
                {
                    if (roomChild.Name == "Tilemap")
                        room.Tilemap = parseTilemapFrom(roomChild.InnerText);
                    if (roomChild.Name == "ID")
                        room.ID = Convert.ToInt32(roomChild.InnerText);
                    if (roomChild.Name == "topRoom")
                        room.ConnectedRooms.Add("topRoom", Convert.ToInt32(roomChild.InnerText));
                    if (roomChild.Name == "botRoom")
                        room.ConnectedRooms.Add("botRoom", Convert.ToInt32(roomChild.InnerText));
                    if (roomChild.Name == "leftRoom")
                        room.ConnectedRooms.Add("leftRoom", Convert.ToInt32(roomChild.InnerText));
                    if (roomChild.Name == "rightRoom")
                        room.ConnectedRooms.Add("rightRoom", Convert.ToInt32(roomChild.InnerText));
                }

                world.Add(room);
                
            }
        }

        private Tile[,] parseTilemapFrom(string tileMapAsString)
        {


            Tile[,] tileMap = new Tile[40, 20];
            String tempRoom = tileMapAsString;
            String[] tempLines = tempRoom.Split("\r\n".ToCharArray());
            List<String> finalLines = new List<string>();

            for (int i = 0; i < tempLines.Length; i++)
            {
                if (!(tempLines[i] == "" || tempLines[i] == " " || tempLines[i] == "  ")) 
                {
                    while (tempLines[i].StartsWith(" ")) tempLines[i] = tempLines[i].Remove(0, 1);
                    finalLines.Add(tempLines[i]);
                }
            }

            for (int y = 0; y < finalLines.Count; y++)
            {
                String[] IDs = finalLines[y].Split(' ');

                for (int x = 0; x < IDs.Length; x++)
                {
                    switch (IDs[x])
                    {
                        case "00":
                            tileMap[x, y] = new NormalTile(new Vector2f(x * Tile.Width, y * Tile.Height));
                            break;
                        case "01":
                            tileMap[x, y] = new CollisionTile(new Vector2f(x * Tile.Width, y * Tile.Height));
                            break;
                    }
                }
            }

            return tileMap;
        }

        public void Draw()
        {
            foreach(Tile tempTile in GetActiveRoom().Tilemap)
            {
                tempTile.Draw();
            }
        }
    }
}
