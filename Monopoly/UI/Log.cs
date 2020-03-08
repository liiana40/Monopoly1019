using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Monopoly
{
    public class Log
    {
        int LeftTab, CenterTab, RightTab;
        int ySpace=18;

        public Rectangle Position = new Rectangle(0, 0, 290, 200);
        public string[] Values = new string[10];
        public int current = 0;
       
        public Log(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
            LeftTab = x + 9;
            RightTab = x + 120;
            CenterTab = x + 60;
            for (int i = 0; i < 10; i++)
                Values[i] = "";
        }
        public void AddLine(string Message)
        {
            Values[current] = Message;
            current++;
            current %= 10;
        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Card.CardTex, Position, Color.White);
            for (int i = 0; i < 10; i++)
            {
                if ((current + 9) % 10 == (i + current) % 10)
                    sb.DrawString(Card.BigSf, Values[(i + current) % 10], new Vector2(LeftTab, Position.Y + 170 - ySpace * i), Color.Black);
                else
                    sb.DrawString(Card.SmallSf, Values[(i + current) % 10], new Vector2(LeftTab, Position.Y + 170- ySpace * i), Color.Black);
            }
        }
    }
}
