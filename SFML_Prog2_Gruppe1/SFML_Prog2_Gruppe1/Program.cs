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
            //Get the Window instance
            RenderWindow window = ProjectRenderWindow.GetRenderWindowInstance();
            window.SetActive();

            Game game = new Game();


            while(window.IsOpen)
            {
                //Update
                game.Update();


                //Draw
                window.Clear(Color.White);
                game.Draw();
                window.Display();
            }

        }
    }
}
