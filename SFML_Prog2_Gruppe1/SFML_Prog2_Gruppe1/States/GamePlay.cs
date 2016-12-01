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
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.States
{
    public class GamePlay : State
    {
        World world;
        Player player;
        QuestNPC questNPC;
        EnemyNPC enemyNPC;

        /// <summary>
        /// The different object instances are created, which are needed for the gameplay state.
        /// </summary>
        public GamePlay()
        {
            world = new World();
            player = new Player();
            player.roomChangeEvent += onPlayerRoomChange;
            questNPC = new QuestNPC();
            enemyNPC = new EnemyNPC();
        }

        /// <summary>
        /// Is triggered by player room change event and changes the active room according to the direction in which the player was going.
        /// </summary>
        /// <param name="direction">
        /// Determines in which direction the player was leaving the room.
        /// </param>
        private void onPlayerRoomChange(Direction direction)
        {
            //TODO: switch case
            if (direction == Direction.DOWN)
            {
                if (world.GetActiveRoom().ConnectedRooms.ContainsKey("botRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["botRoom"]; }
            }
            else if (direction == Direction.LEFT)
            {
                if (world.GetActiveRoom().ConnectedRooms.ContainsKey("leftRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["leftRoom"]; }
            }
            else if (direction == Direction.RIGHT)
            {
                if (world.GetActiveRoom().ConnectedRooms.ContainsKey("rightRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["rightRoom"]; }
            }
            else if (direction == Direction.UP)
            {
                if (world.GetActiveRoom().ConnectedRooms.ContainsKey("topRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["topRoom"]; }
            }
        }

        public override void Dispose()
        {
           // throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the different objects needed for the gameplay state.
        /// </summary>
        public override void Draw()
        {
            world.Draw();
            player.Draw();
            questNPC.Draw();
            enemyNPC.Draw();
            //throw new NotImplementedException();
        }

        public override void Initialize()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the active rooms for players and NPCs and returns the active GameState.
        /// </summary>
        /// <returns>
        /// Returns the active GameState, in this case the gameplay state.
        /// </returns>
        public override GameStates Update()
        {
            player.Update(world.GetActiveRoom().Tilemap);
            enemyNPC.Update(world.GetActiveRoom().Tilemap);
            return GameStates.GamePlayState;
        }

        /// <summary>
        /// Handles the input if the player wants to pause or quit the game out of the gameplay state.
        /// </summary>
        /// <param name="key">
        /// Represents which key was pressed on the keyboard.
        /// </param>
        /// <param name="isPressed">
        /// Checks if the key is pressed.
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public override void HandleInput(Keyboard.Key key, bool isPressed)
        {
            if(isPressed && key == Keyboard.Key.Escape)
            {
                ProjectRenderWindow.GetRenderWindowInstance().Close();
            }
        }
    }
}
