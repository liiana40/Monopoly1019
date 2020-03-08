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
    public class Card
    {
        public static Texture2D CardTex;
        public static Texture2D SquareTex;
        public static SpriteFont BigSf;
        public static SpriteFont SmallSf;

        Vector2 CenteredTitle;
        int LeftTab, CenterTab, RightTab;
        int ySpace = 18;

        public Rectangle Position = new Rectangle(0, 0, 200, 300);


        public Rectangle Background = new Rectangle(0, 0, 188, 50);
        private int current = 0;
        public int Current { get { return current; }
            set { current = value;
                CenteredTitle.X = Position.X + 100 - BigSf.MeasureString(Game1.Board[value].Name).X / 2;
            }
        }

        public Card(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
            Background.X = x + 6;
            Background.Y = y + 6;
            CenteredTitle = new Vector2(20 + x, y + 16);
            LeftTab = x + 9;
            RightTab = x + 120;
            CenterTab = x + 60;



        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(CardTex, Position, Color.White);
            sb.Draw(SquareTex, Background, Game1.Board[Current].Group);

            sb.DrawString(BigSf, Game1.Board[Current].Name, CenteredTitle, Color.Black);
            if (Game1.Board[Current].Rent == Game1.RegularRent)
            {
                sb.DrawString((Game1.Board[Current].Improvements == 0) ? BigSf : SmallSf, "Rent $" + Game1.Board[Current].RentStructure[0], new Vector2(CenterTab, Position.Y + 70), Color.Black);
                if (Game1.Board[Current].RentStructure.Length < 2) return;
                for (int i = 1; i < 5; i++)
                {
                    sb.DrawString((Game1.Board[Current].Improvements == i) ? BigSf : SmallSf, "With " + i.ToString() + " House" + ((i > 1) ? "s" : ""), new Vector2(LeftTab, Position.Y + 78 + ySpace * i), Color.Black);
                    sb.DrawString((Game1.Board[Current].Improvements == i) ? BigSf : SmallSf, "$" + Game1.Board[Current].RentStructure[i], new Vector2(RightTab, Position.Y + 78 + ySpace * i), Color.Black);
                }
                if (Game1.Board[Current].RentStructure.Length < 6) return;
                sb.DrawString((Game1.Board[Current].Improvements == 5) ? BigSf : SmallSf, "With HOTEL $" + Game1.Board[Current].RentStructure[5], new Vector2(LeftTab, Position.Y + 72 + ySpace * 7), Color.Black);

                sb.DrawString(SmallSf, "Mortgage Value $" + Game1.Board[Current].MortgageValue, new Vector2(LeftTab, Position.Y + 72 + ySpace * 9), Color.Black);
                sb.DrawString(SmallSf, "Houses cost $" + Game1.Board[Current].ImprovementCost + ". each", new Vector2(LeftTab, Position.Y + 72 + ySpace * 10), Color.Black);
                sb.DrawString(SmallSf, "Hotels, $" + Game1.Board[Current].ImprovementCost + ". Plus 4 Houses", new Vector2(LeftTab, Position.Y + 72 + ySpace * 11), Color.Black);
            }
            else
                if (Game1.Board[Current].Rent == Game1.RailRoadRent)
            {
                for (int i = 1; i < 5; i++)
                {
                    if (Game1.Board[Current].Owner != null)
                    {
                        sb.DrawString((Game1.Board[Current].Owner.NumberofRailroads == i) ? BigSf : SmallSf, i.ToString() + " Railroad" + ((i > 1) ? "s Owned" : " Owned"), new Vector2(LeftTab, Position.Y + 78 + ySpace * i), Color.Black);
                        sb.DrawString((Game1.Board[Current].Owner.NumberofRailroads == i) ? BigSf : SmallSf, "$" + Game1.Board[Current].RentStructure[i], new Vector2(RightTab + 40, Position.Y + 78 + ySpace * i), (Game1.Board[Current].Owner.NumberofRailroads == i) ? Color.Red : Color.Black);
                    }

                    else
                    {
                        sb.DrawString(SmallSf, i.ToString() + " Railroad" + ((i > 1) ? "s Owned" : " Owned"), new Vector2(LeftTab, Position.Y + 78 + ySpace * i), Color.Black);
                        sb.DrawString(SmallSf, "$" + Game1.Board[Current].RentStructure[i], new Vector2(RightTab + 40, Position.Y + 78 + ySpace * i), Color.Black);
                    }
                }


                sb.DrawString(SmallSf, "Mortgage Value $" + Game1.Board[Current].MortgageValue, new Vector2(LeftTab, Position.Y + 72 + ySpace * 9), Color.Black);
                
            }
            else
                if (Game1.Board[Current].Name == "Chance")
            {
                string[] split = Actions.ChanceText[Actions.picked].Split(' ');
                string line1 = "", line2 = "";
                for (int i = 0; i < split.Length / 2; i++)
                {
                    line1 += split[i] + ' ';
                    line2 += split[split.Length / 2 + i] + ' ';
                }
                if ((split.Length & 1) == 1)
                    line2 += split[split.Length - 1];


                
                     sb.DrawString(SmallSf,line1, new Vector2(LeftTab, Position.Y + 78 + ySpace), Color.Black);
                    sb.DrawString(SmallSf,line2, new Vector2(LeftTab, Position.Y + 78 + ySpace * 2), Color.Black);
            }


        }
    }
}
