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
            Time timeForFrame = Time.FromSeconds(1 / 60f);
            var timeSinceLastFrame = Time.Zero;
            var clock = new Clock();
            ProjectRenderWindow.GetRenderWindowInstance().Closed += new EventHandler(OnClosed);
            ProjectRenderWindow.GetRenderWindowInstance().KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

            //Programloop
            while (ProjectRenderWindow.GetRenderWindowInstance().IsOpen)
            {
                //Update
                stateMachine.Update();
                ProjectRenderWindow.GetRenderWindowInstance().DispatchEvents();

                //Draw
                ProjectRenderWindow.GetRenderWindowInstance().Clear(Color.White);
                stateMachine.Draw();
                ProjectRenderWindow.GetRenderWindowInstance().Display();

                //Frames
                var elapsedTime = clock.ElapsedTime;
                clock.Restart();
                timeSinceLastFrame += elapsedTime;

                while (timeSinceLastFrame > timeForFrame)
                {
                    timeSinceLastFrame -= timeForFrame;
                }
                
                UpdateStatistics(elapsedTime);

            }

        }

        static void UpdateStatistics(Time elapsedTime)
        {
            Time statisticsTime = new Time();
            int statisticsFrames = 0;

            statisticsTime += elapsedTime;
            statisticsFrames += 1;

            //if (statisticsTime >= Time.FromSeconds(1))
            //{
                Console.WriteLine("Frames / Second = {0}\nMicroseconds / Frame = {1}", statisticsFrames, statisticsTime.AsMicroseconds());
                statisticsTime -= Time.FromSeconds(1);
                statisticsFrames = 0;
            //}
        }


        static void OnClosed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            ProjectRenderWindow.GetRenderWindowInstance().Close();
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            if (e.Code == Keyboard.Key.Escape)
                ProjectRenderWindow.GetRenderWindowInstance().Close();
        }
    }
}
