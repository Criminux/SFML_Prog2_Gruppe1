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
    public enum QuestType
    {
        Kill = 1,
        Collect = 2
    }

    public class Quest
    {
        private QuestType type;
        private Font font;
        private Text text;
        private string message;
        private int enemiesToKill;
        private int itemsToCollect;

        public int EnemiesToKill
        {
            get { return enemiesToKill; }
            set { enemiesToKill = value; }
        }
        public int ItemsToCollect
        {
            get { return itemsToCollect; }
            set { itemsToCollect = value; }
        }
        public QuestType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Instantiates a new random quest.
        /// </summary>
        public Quest()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            font = new Font("States/Menu/POORICH.TTF");


            int tempType = rand.Next(1, 3);
            if (tempType == 1)
            {
                type = QuestType.Kill;
                message = "Kill Enemies: ";
                text = new Text(message, font, 20);
                enemiesToKill = rand.Next(1, 6);
                itemsToCollect = 0;
            }
            else
            {
                type = QuestType.Collect;
                message = "Collect Items: ";
                text = new Text(message, font, 20);
                itemsToCollect = rand.Next(1, 6);
                enemiesToKill = 0;
            }

            text.Position = new Vector2f(435,685);
        }

        /// <summary>
        /// Updates the quest information.
        /// </summary>
        public void Update()
        {
            if(type == QuestType.Collect)
            {
                message = "Collect Items: " + itemsToCollect.ToString();
            }
            else
            {
                message = "Kill Enemies: " + enemiesToKill.ToString();
            }

            text.DisplayedString = message;
        }

        /// <summary>
        /// Draws the quest.
        /// </summary>
        public void Draw()
        {
            ProjectRenderWindow.GetRenderWindowInstance().Draw(text);
        }


        
    }
}
