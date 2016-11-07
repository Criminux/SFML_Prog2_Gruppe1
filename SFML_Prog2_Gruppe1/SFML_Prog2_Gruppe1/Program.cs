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
    class Program
    {
        static void Main()
        {
            ProjectRenderWindow.GetRenderWindowInstance().SetActive();

            StateMachine stateMachine = new StateMachine();

            Game game = new Game();


            while(ProjectRenderWindow.GetRenderWindowInstance().IsOpen)
            {
                //Update
                //game.Update();
                stateMachine.Update();


                //Draw
                ProjectRenderWindow.GetRenderWindowInstance().Clear(Color.White);
                //game.Draw();
                stateMachine.Draw();
                ProjectRenderWindow.GetRenderWindowInstance().Display();
            }

        }
    }
}
