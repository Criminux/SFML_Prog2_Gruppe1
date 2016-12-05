﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

namespace SFML_Prog2_Gruppe1
{
    class CollisionTile : Tile
    {
        /// <summary>
        /// Constructor for collidable tile.
        /// </summary>
        /// <param name="position">Position.</param>
        public CollisionTile(Vector2f position, CollisionTileType type) : base()
        {
            Initialize();

            switch(type)
            {
                case CollisionTileType.Wall:
                    texture = new Texture("Tile/CollisionTile.png");
                    break;

            }
            this.position = position;

            sprite.Texture = texture;
            sprite.Position = position;
        }
    }
}
