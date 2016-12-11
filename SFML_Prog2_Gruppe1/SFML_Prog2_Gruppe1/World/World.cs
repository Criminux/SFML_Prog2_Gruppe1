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

        //TODO: Spawn random Enemies and Items
        public World()
        {
            world = new List<Room>();
            currentID = 22;
            InitializeWorld();
        }

        /// <summary>
        /// Initializes the world based on the XML file.
        /// </summary>
        private void InitializeWorld()
        {
            XmlDocument document = new XmlDocument();
            document.Load("World/World.xml");
            
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
                    if (roomChild.Name == "Enemy")
                    {
                        EnemyNPC enemy = new EnemyNPC();
                        foreach(XmlNode enemyChild in roomChild.ChildNodes)
                        {
                            if (enemyChild.Name == "PositionX") enemy.Position = new Vector2f(Convert.ToInt32(enemyChild.InnerText), enemy.Position.Y);
                            if (enemyChild.Name == "PositionY") enemy.Position = new Vector2f(enemy.Position.X, Convert.ToInt32(enemyChild.InnerText));
                        }
                        room.Enemies.Add(enemy);
                    }
                    if (roomChild.Name == "NPC")
                    {
                        QuestNPC npc = new QuestNPC();
                        foreach (XmlNode enemyChild in roomChild.ChildNodes)
                        {
                            if (enemyChild.Name == "PositionX") npc.Position = new Vector2f(Convert.ToInt32(enemyChild.InnerText), npc.Position.Y);
                            if (enemyChild.Name == "PositionY") npc.Position = new Vector2f(npc.Position.X, Convert.ToInt32(enemyChild.InnerText));
                        }
                        room.Npcs.Add(npc);
                    }
                    if (roomChild.Name == "Item")
                    {
                        int tempX = 0;
                        int tempY = 0;
                        foreach (XmlNode enemyChild in roomChild.ChildNodes)
                        {
                            if (enemyChild.Name == "PositionX") tempX = Convert.ToInt32(enemyChild.InnerText);
                            if (enemyChild.Name == "PositionY") tempY = Convert.ToInt32(enemyChild.InnerText);
                        }
                        Item item = new Item(new Vector2f((float)tempX, (float)tempY));
                        room.Items.Add(item);
                    }
                }

                world.Add(room);
                
            }
        }

        /// <summary>
        /// Creates the Tilemap based on a string of Tile-IDs.
        /// </summary>
        /// <param name="tileMapAsString">
        /// Tilemap IDs as string.
        /// </param>
        /// <returns>
        /// Tilemap as Twodimensional array of Tiles.
        /// </returns>
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
                            tileMap[x, y] = new NormalTile(new Vector2f(x * Tile.Width, y * Tile.Height), NormalTileType.Grass);
                            break;
                        case "01":
                            tileMap[x, y] = new NormalTile(new Vector2f(x * Tile.Width, y * Tile.Height), NormalTileType.GrassTop);
                            break;
                        case "02":
                            tileMap[x, y] = new NormalTile(new Vector2f(x * Tile.Width, y * Tile.Height), NormalTileType.GrassBot);
                            break;
                        case "03":
                            tileMap[x, y] = new NormalTile(new Vector2f(x * Tile.Width, y * Tile.Height), NormalTileType.GrassL);
                            break;
                        case "04":
                            tileMap[x, y] = new NormalTile(new Vector2f(x * Tile.Width, y * Tile.Height), NormalTileType.GrassR);
                            break;

                        case "10":
                            tileMap[x, y] = new CollisionTile(new Vector2f(x * Tile.Width, y * Tile.Height), CollisionTileType.Wall);
                            break;
                        case "11":
                            tileMap[x, y] = new CollisionTile(new Vector2f(x * Tile.Width, y * Tile.Height), CollisionTileType.Ruin);
                            break;
                        case "12":
                            tileMap[x, y] = new CollisionTile(new Vector2f(x * Tile.Width, y * Tile.Height), CollisionTileType.Tree1);
                            break;
                        case "13":
                            tileMap[x, y] = new CollisionTile(new Vector2f(x * Tile.Width, y * Tile.Height), CollisionTileType.Tree2);
                            break;

                    }
                }
            }

            return tileMap;
        }

        public static bool isAtStart(int currentID)
        {
            if (currentID == 22)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update()
        {
            GetActiveRoom().Update();
        }

        public void Draw()
        {
            GetActiveRoom().Draw();
        }
    }
}
