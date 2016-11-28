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

        public GamePlay()
        {
            world = new World();
            player = new Player();
            player.roomChangeEvent += onPlayerRoomChange;
            questNPC = new QuestNPC();
            enemyNPC = new EnemyNPC();
        }

        private void onPlayerRoomChange(Direction direction)
        {
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

        public override GameStates Update()
        {
            player.Update(world.GetActiveRoom().Tilemap);
            return GameStates.GamePlayState;
            //throw new NotImplementedException();
        }

        public override bool HandleInput(Keyboard.Key key, bool isPressed)
        {
            if(isPressed && key == Keyboard.Key.Escape)
            {
                ProjectRenderWindow.GetRenderWindowInstance().Close();
            }
            return true;
        }
    }
}
