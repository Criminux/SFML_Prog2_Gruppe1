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
    public class UIManager
    {
        private Texture uiBar;
        private Sprite sprite;

        public UIManager()
        {
            uiBar = new Texture("UI/UIBar.png");
            sprite = new Sprite(uiBar);
            sprite.Position = new Vector2f(0, 640);
        }

        public void Update()
        {

        }

        public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(sprite);
        }
    }
}
