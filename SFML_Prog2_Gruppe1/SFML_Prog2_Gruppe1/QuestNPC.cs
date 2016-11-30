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
namespace SFML_Prog2_Gruppe1
{
    public class QuestNPC : Character
    {

        /// <summary>
        /// Loads correct texture and applies starting position.
        /// </summary>
        public QuestNPC() : base()
        {
            characterType = CharacterID.QuestNPC;

            characterTexture = new Texture("Character/QuestNPC.png");
            characterSprite.Texture = characterTexture;
            characterSprite.Position = new Vector2f(250,250);


        }

        /// <summary>
        /// Updates the base.
        /// </summary>
        /// <param name="room">
        /// Current room.
        /// </param>
        public override void Update(Tile[,] room)
        {
            base.Update(room);
        }

    }
}
