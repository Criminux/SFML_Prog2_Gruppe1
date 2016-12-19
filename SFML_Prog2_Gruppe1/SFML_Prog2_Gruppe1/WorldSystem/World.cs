using System;
using System.Collections.Generic;
using SFML.System;
using System.Xml;
using SFML_Prog2_Gruppe1.CharacterSystem;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    /// <summary>
    /// Manages the world and its rooms. Also initializes the word from XML.
    /// </summary>
    public class World
    {
        private const int NumberOfEnemies = 5;
        private const int NumberOfItems = 5;

        private List<Room> world;
        private int currentID;
        
        /// <summary>
        /// Property to get a room by a specific id.
        /// </summary>
        /// <param name="id">ID of the room</param>
        /// <returns>Room with matching ID</returns>
        public Room GetRoomByID(int id)
        {
            foreach(Room room in world)
            {
                if (room.ID == id) return room;
            }
            return new Room();
        }
        /// <summary>
        /// Property to return active room.
        /// </summary>
        /// <returns>Room which is active</returns>
        public Room GetActiveRoom()
        {
            return GetRoomByID(currentID);
        }

        /// <summary>
        /// Getter and setter for active ID.
        /// </summary>
        public int CurrentID
        {
            get { return currentID; }
            set { currentID = value; }
        }

        /// <summary>
        /// Constructor of world to call initialize and reset variables.
        /// </summary>
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
            document.Load("WorldSystem/World.xml");
            
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
                    if (roomChild.Name == "Spawn")
                    {
                        Vector2f spawn = new Vector2f();
                        foreach(XmlNode enemyChild in roomChild.ChildNodes)
                        {
                            if (enemyChild.Name == "PositionX") spawn.X = Convert.ToInt32(enemyChild.InnerText);
                            if (enemyChild.Name == "PositionY") spawn.Y = Convert.ToInt32(enemyChild.InnerText);
                        }
                        room.Spawns.Add(new Spawn(spawn));
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
                }

                world.Add(room);
                
            }

            for(int i = 0; i <= NumberOfEnemies; i++)
            {
                SpawnEnemy();
            }
            for (int i = 0; i <= NumberOfItems; i++)
            {
                SpawnItem();
            }
        }

        /// <summary>
        /// Spawns a random enemy.
        /// </summary>
        public void SpawnEnemy()
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            int randomRoom;
            int randomSpawn;

            while (true)
            {
                randomRoom = rand.Next(0, world.Count);
                if (world[randomRoom] != GetActiveRoom())
                {
                    randomSpawn = rand.Next(0, world[randomRoom].Spawns.Count);
                    if (world[randomRoom].Spawns[randomSpawn].IsUsed == false) break;
                }
            }
            
            EnemyNPC enemy = new EnemyNPC();
            enemy.Position = world[randomRoom].Spawns[randomSpawn].Position;
            enemy.Spawn = world[randomRoom].Spawns[randomSpawn];
            world[randomRoom].Spawns[randomSpawn].IsUsed = true;

            world[randomRoom].Enemies.Add(enemy);
        }

        /// <summary>
        /// Spawns a random item.
        /// </summary>
        public void SpawnItem()
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            int randomRoom;
            int randomSpawn;

            while (true)
            {
                randomRoom = rand.Next(0, world.Count);
                if (world[randomRoom] != GetActiveRoom())
                {
                    randomSpawn = rand.Next(0, world[randomRoom].Spawns.Count);
                    if (world[randomRoom].Spawns[randomSpawn].IsUsed == false) break;
                }
            }

            Item item = new Item(world[randomRoom].Spawns[randomSpawn].Position);
            item.Spawn = world[randomRoom].Spawns[randomSpawn];
            world[randomRoom].Spawns[randomSpawn].IsUsed = true;

            world[randomRoom].Items.Add(item);
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

        /// <summary>
        /// Check if current room is start room.
        /// </summary>
        /// <param name="currentID">current ID</param>
        /// <returns>true if current room is start room.</returns>
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

        /// <summary>
        /// Updates the world.
        /// </summary>
        /// <param name="playerPosition">Position of player.</param>
        public void Update(Vector2f playerPosition)
        {
            GetActiveRoom().Update(playerPosition);

            for (int i = GetActiveRoom().Enemies.Count - 1; i >= 0; i--)
            {
                if(GetActiveRoom().Enemies[i].Position.X < 0 || GetActiveRoom().Enemies[i].Position.X > 1280 || GetActiveRoom().Enemies[i].Position.Y < 0 || GetActiveRoom().Enemies[i].Position.Y > 672)
                {
                    GetActiveRoom().Enemies.RemoveAt(i);
                    SpawnEnemy();
                }
            }
        }

        /// <summary>
        /// Draws the world.
        /// </summary>
        public void Draw()
        {
            GetActiveRoom().Draw();
        }
    }
}
