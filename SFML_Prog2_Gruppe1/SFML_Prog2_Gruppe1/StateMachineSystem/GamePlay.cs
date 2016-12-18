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
using SFML_Prog2_Gruppe1.WorldSystem;
using SFML_Prog2_Gruppe1.CharacterSystem;
using SFML_Prog2_Gruppe1.CommandSystem;

namespace SFML_Prog2_Gruppe1.StateMachineSystem
{
    public class GamePlay : State
    {
        World world;
        Player player;
        UIManager uimanager;
        CommandQueue commandQueue;
        GameStates targetState;

        public GameStates TargetState
        {
            get { return targetState; }
        }

        /// <summary>
        /// The different object instances are created, which are needed for the gameplay state.
        /// </summary>
        public GamePlay()
        {
            world = new World();
            player = new Player();
            player.roomChangeEvent += onPlayerRoomChange;
            player.QuestEvent += onPlayerQuest;
            player.EnemyEvent += onEnemyEvent;
            player.ItemEvent += onItemEvent;
            uimanager = new UIManager();
            commandQueue = new CommandQueue();
            targetState = GameStates.GamePlayState;
        }

        /// <summary>
        /// Updates the "collect items" quest for each collected item.
        /// </summary>
        private void onItemEvent()
        {
            world.SpawnItem();

            if ((player.QuestCompleted == false) && (player.Quest.Type == QuestType.Collect))
            {
                player.Quest.ItemsToCollect--;

                if (player.Quest.ItemsToCollect <= 0)
                {
                    player.Quest.ItemsToCollect = 0;
                    QuestSuccess();
                }
            }
        }

        /// <summary>
        /// Updates the "kill enemies" quest for each eliminated enemy.
        /// </summary>
        private void onEnemyEvent()
        {
            world.SpawnEnemy();

            if ((player.QuestCompleted == false) && (player.Quest.Type == QuestType.Kill))
            {
                player.Quest.EnemiesToKill--;

                if (player.Quest.EnemiesToKill <= 0)
                {
                    player.Quest.EnemiesToKill = 0;
                    QuestSuccess();
                }
            }
        }

        /// <summary>
        /// This method causes the improvement of the player's capabilities on quest completion.
        /// </summary>
        private void QuestSuccess()
        {
            player.QuestCompleted = true;
            player.ShotCoolDown -= 50;
            player.ProjectileSpeed += 2;
            player.MovementSpeed += 1;
        }

        /// <summary>
        /// Instantiates a new quest if all prerequisites are met.
        /// </summary>
        private void onPlayerQuest()
        {
            if(isQuestAvailable())
            {
                player.QuestCompleted = false;
                player.Quest = new Quest();
            }
        }

        /// <summary>
        /// Is triggered by player room change event and changes the active room according to the direction in which the player was going.
        /// </summary>
        /// <param name="direction">
        /// Determines in which direction the player was leaving the room.
        /// </param>
        private void onPlayerRoomChange(Direction direction)
        {
            switch(direction)
            {
                case Direction.DOWN:
                    if (world.GetActiveRoom().ConnectedRooms.ContainsKey("botRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["botRoom"]; }
                    break;
                case Direction.LEFT:
                    if (world.GetActiveRoom().ConnectedRooms.ContainsKey("leftRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["leftRoom"]; }
                    break;
                case Direction.RIGHT:
                    if (world.GetActiveRoom().ConnectedRooms.ContainsKey("rightRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["rightRoom"]; }
                    break;
                case Direction.UP:
                    if (world.GetActiveRoom().ConnectedRooms.ContainsKey("topRoom")) { world.CurrentID = world.GetActiveRoom().ConnectedRooms["topRoom"]; }
                    break;
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
            uimanager.Draw(player.Quest);
        }

        public override void Initialize()
        {
            targetState = GameStates.GamePlayState;
        }

        /// <summary>
        /// Updates the active rooms for players and NPCs, adds commands to the movementQueue and returns the active GameState.
        /// </summary>
        /// <returns>
        /// Returns the active GameState, in this case the gameplay state.
        /// </returns>
        public override GameStates Update()
        {
            player.CheckInputs(commandQueue);

            if (!commandQueue.IsEmpty())
            {
                OnCommand(commandQueue.Pop());
            }

            player.Update(world.GetActiveRoom());
            world.Update(player.Position);

            uimanager.Update(player.Health, player.MovementSpeed);

            if (player.Health <= 0) targetState = GameStates.CreditScreenState;
            
            return targetState;
        }

        /// <summary>
        /// This code executes the movement command.
        /// </summary>
        /// <param name="command">
        /// Movement command from commandQueue.
        /// </param>
        private void OnCommand(AbstractCommand command)
        {
            command.Execute(player);
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
                targetState = GameStates.PauseMenuState;
            }
        }

        /// <summary>
        /// Checks if the player is is within the defined distance of an npc.
        /// </summary>
        /// <returns>
        /// True if the condition is met.
        /// </returns>
        public bool isQuestAvailable()
        {
            float distance = 20;
            float distancesqrd = 0;
            Vector2f PlayerPos = player.Position;

            if (world.GetActiveRoom().ID == 22)
            {
                foreach (QuestNPC npc in world.GetActiveRoom().Npcs)
                {
                    Vector2f NpcPos = npc.Position;

                    distancesqrd = ((PlayerPos.X - NpcPos.X) * (PlayerPos.X - NpcPos.X) +
                        (PlayerPos.Y - NpcPos.Y) * (PlayerPos.Y - NpcPos.Y));
                    if (distancesqrd <= (distance * distance))
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
            {
                return false;
            }
        }

    }
}
