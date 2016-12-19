using SFML.Window;
using SFML.Graphics;

namespace SFML_Prog2_Gruppe1.Util
{
    /// <summary>
    /// This class holds the render window. Prevents multiple windows opening (Singleton pattern).
    /// </summary>
    public class ProjectRenderWindow
    {
        static RenderWindow window = null;

        /// <summary>
        /// Opens new window, if no window is already open.
        /// </summary>
        /// <returns>
        /// Window.
        /// </returns>
        public static RenderWindow GetRenderWindowInstance()
        {
            if (window == null)
            {
                window = new RenderWindow(new VideoMode(1280, 720), "Prog2_Gruppe1", Styles.Close, new ContextSettings(24, 8, 2));
                return window;
            }
            return window;
        }
    }
}
