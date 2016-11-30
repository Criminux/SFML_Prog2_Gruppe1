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
        /// Applies a new dictionary to the connectedRooms variable.
        /// </summary>
        public Room()
        {
            connectedRooms = new Dictionary<string, int>();
        }

        public Room(Tile[,] tilemap, int ID, Dictionary<string, int> connectedRooms)
        {
            this.tilemap = tilemap;
            this.id = ID;
            this.connectedRooms = connectedRooms;
        }
    }
}
