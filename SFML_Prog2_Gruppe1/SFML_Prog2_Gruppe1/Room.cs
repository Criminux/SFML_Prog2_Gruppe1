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

        public Tile[,] Tilemap
        {
            get { return tilemap; }
            set { tilemap = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Dictionary<string, int> ConnectedRooms
        {
            get { return connectedRooms; }
            set { connectedRooms = value; }
        }


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
