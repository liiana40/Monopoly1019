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
    public class PlayerStats
    {
        

        Vector2 CenteredTitle;
        int LeftTab, CenterTab, RightTab;
        int ySpace=18;

        public Rectangle Position = new Rectangle(0, 0, 140, 200);

        Player p;
        //public Rectangle Background = new Rectangle(0, 0, 188, 50);
        private int current = 0;
        public int Current { get { return current; }
            set { current = value;
                CenteredTitle.X = Position.X + 100 - Card.BigSf.MeasureString(Game1.Board[value].Name).X /2; } }      
        
        public PlayerStats(int x, int y, Player p)
        {
            this.p = p;
            Position.X = x;
            Position.Y = y;
            //Background.X = x + 6;
            //Background.Y = y + 6;
            //CenteredTitle = new Vector2(20+x, y + 16);
            LeftTab = x + 9;
            RightTab = x + 120;
            CenterTab = x + 60;



        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Card.CardTex, Position, (Game1.Turn == p.PlayerID) ? ((p.inJail)?Color.Red:Color.Yellow) : Color.White);
            //sb.Draw(SquareTex, Background, Game1.Board[Current].Group);

            //sb.DrawString(Card.BigSf, "", CenteredTitle, Color.Black);
            sb.DrawString(Card.SmallSf, "Previous Money $" + p.PrevMoney.ToString(), new Vector2(LeftTab, Position.Y + 70 - ySpace), Color.Black);
            sb.DrawString(Card.SmallSf, "Money $" + p.Money.ToString(), new Vector2(LeftTab, Position.Y + 70), Color.Black);
            sb.DrawString(Card.SmallSf,
                Game1.Board[p.CurrentSquare].Name,
                new Vector2(LeftTab-3, Position.Y + 70 + 1 * ySpace),
                (Game1.Board[p.CurrentSquare].Owner == null) ? Color.Black :
                (Game1.Board[p.CurrentSquare].Owner == p) ? Color.Green : Color.Red
                );

            sb.DrawString(Card.SmallSf, "Property Value $" + p.PropertiesValue.ToString(), new Vector2(LeftTab, Position.Y + 70 + ySpace * 3), Color.Black);
            sb.DrawString(Card.SmallSf, "Property Count:" + p.NumberofProperties.ToString(), new Vector2(LeftTab, Position.Y + 70 + ySpace * 4), Color.Black);
            sb.DrawString(Card.SmallSf, "RailRoad Count:" + p.NumberofRailroads.ToString(), new Vector2(LeftTab, Position.Y + 70 + ySpace * 5), Color.Black);
            int i = 0;
            foreach (var item in p.OwnsAll)
            {
                sb.Draw(Card.CardTex, new Rectangle(LeftTab + (i+1) * ySpace, Position.Y + 70 + 6 * ySpace, ySpace * 2, ySpace), item);
                i++;
            }
            //if (Game1.Board[Current].RentStructure.Length < 2) return;
            //for (int i = 1; i < 5; i++)
            //{
            //    sb.DrawString(Card.SmallSf, "With " + i.ToString() + " House" + ((i > 1) ? "s" : ""), new Vector2(LeftTab, Position.Y + 78 + ySpace * i), Color.Black);
            //    sb.DrawString(Card.SmallSf, "$" + Game1.Board[Current].RentStructure[i], new Vector2(RightTab, Position.Y + 78 + ySpace * i), Color.Black);
            //}
            //if (Game1.Board[Current].RentStructure.Length < 6) return;
            //sb.DrawString(Card.SmallSf, "With HOTEL $" + Game1.Board[Current].RentStructure[5], new Vector2(LeftTab, Position.Y + 72 + ySpace * 7), Color.Black);

            //sb.DrawString(Card.SmallSf, "Mortgage Value $" + Game1.Board[Current].MortgageValue, new Vector2(LeftTab, Position.Y + 72 + ySpace * 9), Color.Black);
            //sb.DrawString(Card.SmallSf, "Houses cost $" + Game1.Board[Current].ImprovementCost +". each", new Vector2(LeftTab, Position.Y + 72 + ySpace * 10), Color.Black);
            //sb.DrawString(Card.SmallSf, "Hotels, $" + Game1.Board[Current].ImprovementCost + ". Plus 4 Houses", new Vector2(LeftTab, Position.Y + 72 + ySpace * 11), Color.Black);
        }
    }
}
