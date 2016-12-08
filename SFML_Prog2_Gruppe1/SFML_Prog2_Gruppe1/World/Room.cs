using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFML_Prog2_Gruppe1
{
    public class Room
    {
        private Tile[,] tilemap;
        private int id;
        private Dictionary<string, int> connectedRooms;

        private List<EnemyNPC> enemies;
        private List<QuestNPC> npcs;

        /// <summary>
        /// Getter and Setter for the tilemap.
        /// </summary>
        public Tile[,] Tilemap
        {
            get { return tilemap; }
            set { tilemap = value; }
        }

        /// <summary>
        /// Getter and setter for the room ID.
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Getter and setter for the connected rooms dictionary.
        /// </summary>
        public Dictionary<string, int> ConnectedRooms
        {
            get { return connectedRooms; }
            set { connectedRooms = value; }
        }

        /// <summary>
        /// Getter and setter for the list of enemies.
        /// </summary>
        public List<EnemyNPC> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        /// <summary>
        /// Getter and setter for the list of npcs.
        /// </summary>
        public List<QuestNPC> Npcs
        {
            get { return npcs; }
            set { npcs = value; }
        }

        /// <summary>
        /// Empty constructor for initializing the fields.
        /// </summary>
        public Room()
        {
            connectedRooms = new Dictionary<string, int>();
            enemies = new List<EnemyNPC>();
            npcs = new List<QuestNPC>();
        }

        public Room(Tile[,] tilemap, int ID, Dictionary<string, int> connectedRooms, List<EnemyNPC> enemies, List<QuestNPC> npcs)
        {
            this.tilemap = tilemap;
            this.id = ID;
            this.connectedRooms = connectedRooms;
            this.enemies = enemies;
            this.npcs = npcs;
        }

        public void Update()
        {
            foreach (EnemyNPC tempEnemy in enemies)
            {
                tempEnemy.Update(this);
            }
            foreach (QuestNPC tempNPC in npcs)
            {
                tempNPC.Update(this);
            }
        }

        /// <summary>
        /// Draw Method for tilemap, enemies and npcs.
        /// </summary>
        public void Draw()
        {
            foreach (Tile tempTile in tilemap)
            {
                tempTile.Draw();
            }
            foreach (EnemyNPC tempEnemy in enemies)
            {
                tempEnemy.Draw();
            }
            foreach (QuestNPC tempNPC in npcs)
            {
                tempNPC.Draw();
            }
        }
    }
}
