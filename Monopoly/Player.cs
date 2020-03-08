using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Monopoly
{
    public class Player
    {
        public static Texture2D Tex;

        public const int WIDTH = 63;
        public const int HEIGHT = 116;



        public int PlayerID;

        public Vector2 Position;
        public Rectangle SourceRect;

        public int NumberofUtilities = 0;
        public int NumberofRailroads = 0;
        public int NumberofProperties = 0;
        public int PropertiesValue = 0;

        public int Money = 1500;
        public int PrevMoney = 1500;
        public int CurrentSquare = 0;
        public bool inJail = false;
        public bool isDead = false;
        public int GetOutofJailCards = 0;

        public int HouseCount = 0;
        public int HotelCount = 0;

        public int DoublesCount = 0;

        public HashSet<Color> OwnsAll = new HashSet<Color>();

        public Player(int ID)
        {
            SourceRect = new Rectangle(32 * ID, 0, 32, 32);
            PlayerID = ID;            
            SetLocation();
        }

        public void Roll()
        {
            if (isDead) return;
            PrevMoney = Money;
            // Buy House if possible
            AddImprovement(CurrentSquare);

            int d1 = Game1.die[0].Roll();
            int d2 = Game1.die[1].Roll();
            if (d1 == d2)
                DoublesCount++;
            else
                DoublesCount = 0;
            
            if (inJail && DoublesCount > 0)
            {
                Game1.Logs[PlayerID].AddLine("Rolled Doubles to get out of Jail");
                DoublesCount = 0;
                inJail = false;
            }
            if (inJail)
            {
                if (GetOutofJailCards > 0)
                {
                    GetOutofJailCards--;
                    DoublesCount = 0;
                    inJail = false;
                    Game1.Logs[PlayerID].AddLine("Used Get out of Jail Free Card");
                }
                return;
            }
            if (DoublesCount > 3)
            {
                Game1.Logs[PlayerID].AddLine("In Jail from Doubles");
                GotoJail();
                return;
            }

          
            MoveTo((CurrentSquare + d1 + d2) % 40);
        }
        public void MoveTo(int Square, bool CollectGo = true,bool PerformAction = true)
        {
            Game1.Logs[PlayerID].AddLine("Moving to " + Game1.Board[Square].Name + " " + Square.ToString());
            if (Square < CurrentSquare && CollectGo)
            {
                Money += 200;
                Game1.Logs[PlayerID].AddLine("Pass Go Collect $200"); 
            }
            CurrentSquare = Square % 40;
            //Game1.g.Window.Title = CurrentSquare.ToString();
            Game1.Card.Current = CurrentSquare;
            SetLocation();
            if (!PerformAction) return;
            //auto buy
            if (Game1.Board[CurrentSquare].ForSale)
            {
                if(Buy(CurrentSquare))
                    Game1.Logs[PlayerID].AddLine("Bought " + Game1.Board[Square].Name + " " + Square.ToString() + " for $"  + Game1.Board[Square].Cost);

                return;
            }
            Game1.Board[CurrentSquare].LandAction(this, Game1.Board[CurrentSquare]);
            


        }

        public bool AddImprovement(int p)
        {
            if (Game1.Board[p].Owner != this) return false;
            if (Game1.Board[p].ImprovementCost == 0) return false;
            if (Game1.Board[p].ImprovementCost > Money) return false;
            if (Game1.Board[p].Improvements >= Game1.Board[p].RentStructure.Length - 1) return false;
            if (!OwnsAll.Contains(Game1.Board[p].Group)) return false;

            Game1.Board[p].Improvements++;
            if (Game1.Board[p].Improvements == 5) // hotel
            {
                HouseCount -= 4;
                HotelCount++;
                Game1.Logs[PlayerID].AddLine("Bought Hotel for" 
                    + Game1.Board[p].Name 
                    + " " + p.ToString() + " for $" 
                    + Game1.Board[p].ImprovementCost);
            }
            else
            {
                HouseCount++;
                Game1.Logs[PlayerID].AddLine("Bought House for"
                   + Game1.Board[p].Name
                   + " " + p.ToString() + " for $"
                   + Game1.Board[p].ImprovementCost);
            }

            Money -= Game1.Board[p].ImprovementCost;

            return true;
        }



        public bool Buy(int id)
        {
            if (!Game1.Board[id].ForSale) return false;
            if (Money < Game1.Board[id].Cost) return false;
            Money -= Game1.Board[id].Cost;
            Game1.Board[id].Owner = this;
            Game1.Board[id].PurchaseAction(this, Game1.Board[id]);
            Game1.Board[id].ForSale = false;

            return true;

        }
        public void Die()
        {
            for (int i = 0; i < 40; i++)
            {
               
                if (Game1.Board[i].Owner== this)
                {
                    Game1.Board[i].Owner = null;
                    Game1.Board[i].Mortgaged = false;
                    Game1.Board[i].ForSale = true;
                    Game1.Board[i].Improvements = 0;
                    OwnsAll.Clear();
                    isDead = true;
                }
            }
        }

        public void SetLocation()
        { 

            int side = (CurrentSquare / 10);
            int x = CurrentSquare % 10;

            switch (side)
            {
                case 2:
                    Position = new Vector2(
                       HEIGHT + WIDTH * (x -1),
                        PlayerID * 32
                        );
                    break;
                case 3:
                    Position = new Vector2(
                        736 - PlayerID * 32,
                         HEIGHT + WIDTH * (x -1)
                        );
                    break;
                case 0:
                    Position = new Vector2(
                        HEIGHT + WIDTH * (9 - x),
                        736 - PlayerID * 32
                        );
                    break;
                case 1:
                    Position = new Vector2(
                        PlayerID * 32,
                        HEIGHT + WIDTH * (9 - x)                        
                        );
                    break;
                default:
                    break;
            }

            if (inJail) Position.X += 32;

        }
        public void GotoJail()
        {
            inJail = true;
            DoublesCount = 0;            
            CurrentSquare = 10;
            SetLocation();
        } 
        public void Pay(int amount)
        {
            Money -= amount;
            if (Money < 0) Die();
        }
        public void Collect(int amount)
        {
            Money += amount;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Tex, Position, SourceRect, Color.White);
        }

    }
}