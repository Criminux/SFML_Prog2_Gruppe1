﻿using System.Collections.Generic;
using SFML.System;
using SFML_Prog2_Gruppe1.CharacterSystem;

namespace SFML_Prog2_Gruppe1.WorldSystem
{
    /// <summary>
    /// Room class for holding one room with its tilemap and containing enemies, npcs and items.
    /// </summary>
    public class Room
    {
        private Tile[,] tilemap;
        private int id;
        private Dictionary<string, int> connectedRooms;

        private List<EnemyNPC> enemies;
        private List<QuestNPC> npcs;
        private List<Item> items;
        private List<Spawn> spawns;

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
        /// Getter and setter for the list of items.
        /// </summary>
        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// GEtter and setter for the spawns in a room.
        /// </summary>
        public List<Spawn> Spawns
        {
            get { return spawns; }
            set { spawns = value; }
        }

        /// <summary>
        /// Empty constructor for initializing the fields.
        /// </summary>
        public Room()
        {
            connectedRooms = new Dictionary<string, int>();
            enemies = new List<EnemyNPC>();
            npcs = new List<QuestNPC>();
            items = new List<Item>();
            spawns = new List<Spawn>();
        }

        /// <summary>
        /// Constructor with all fields.
        /// </summary>
        /// <param name="tilemap">2-dimensional tilearray</param>
        /// <param name="ID">Room id</param>
        /// <param name="connectedRooms">Dictionary of all connected rooms</param>
        /// <param name="enemies">List of enemies</param>
        /// <param name="npcs">List of npcs</param>
        /// <param name="items">List of items</param>
        /// <param name="spawns">List of spawns</param>
        public Room(Tile[,] tilemap, int ID, Dictionary<string, int> connectedRooms, List<EnemyNPC> enemies, List<QuestNPC> npcs, List<Item> items, List<Spawn> spawns)
        {
            this.tilemap = tilemap;
            this.id = ID;
            this.connectedRooms = connectedRooms;
            this.enemies = enemies;
            this.npcs = npcs;
            this.items = items;
            this.spawns = spawns;
        }

        /// <summary>
        /// Updates the room and its enemies and npcs.
        /// </summary>
        /// <param name="playerPosition">Position of the player.</param>
        public void Update(Vector2f playerPosition)
        {
            foreach (EnemyNPC tempEnemy in enemies)
            {
                tempEnemy.Update(this, playerPosition);
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
            foreach (Item tempItem in items)
            {
                tempItem.Draw();
            }
        }
    }
}
