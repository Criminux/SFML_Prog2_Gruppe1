using System;
using SFML.System;
using SFML.Graphics;
using SFML_Prog2_Gruppe1.Util;

namespace SFML_Prog2_Gruppe1.CharacterSystem
{
    /// <summary>
    /// Enumeration for the different quest types.
    /// </summary>
    public enum QuestType
    {
        Kill = 1,
        Collect = 2
    }

    /// <summary>
    /// Blueprint for all possible quests.
    /// </summary>
    public class Quest
    {
        private QuestType type;
        private Font font;
        private Text text;
        private string message;
        private int enemiesToKill;
        private int itemsToCollect;

        /// <summary>
        /// Property to get and set the enemy to kill value.
        /// </summary>
        public int EnemiesToKill
        {
            get { return enemiesToKill; }
            set { enemiesToKill = value; }
        }

        /// <summary>
        /// Property to get and set the items to collect value.
        /// </summary>
        public int ItemsToCollect
        {
            get { return itemsToCollect; }
            set { itemsToCollect = value; }
        }

        /// <summary>
        /// Property to get the quest type.
        /// </summary>
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
            font = new Font("StateMachineSystem/Menu/POORICH.TTF");


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

            text.Position = new Vector2f(435, 685);
        }

        /// <summary>
        /// Updates the quest information.
        /// </summary>
        public void Update()
        {
            if (type == QuestType.Collect)
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