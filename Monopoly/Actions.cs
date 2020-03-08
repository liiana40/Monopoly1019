using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Monopoly
{
    public class Actions
    {
        //public delegate void Action1(Player Current, Property p);
        public static int picked = 0;
        public static string[] ChanceText ={
                "Advance to Go.",
                "Advance to Illinois Ave.",
                "Advance to St. Charles Place.",
                "Advance to nearest Utility. Pay owner ten times dice roll.",
                "Advance to nearest Railroad. Pay Twice the rent.",
                "Advance to nearest Railroad. Pay Twice the rent.",
                "Bank pays you a dividend of $50.",
                "Get out of Jail Free.",
                "Go back three spaces.",
                "Go to Jail.",
                "Make general repairs: Pay $25 per house and $100 per hotel.",
                "Pay poor tax of $15.",
                "Take a trip on Reading Railroad.",
                "Take a walk on Boardwalk.",
                "You have been elected Chairman of the Board. Pay each player $50.",
                "Your builing and loan matures. Collect $150."
            };
        public static string[] CommunityChestText ={
               "Advance to Go.",
                "Bank Error in your favor. Collect $200.",
                "Doctor's Fees. Pay $50.",
                "From sale of stock you get $50.",
                "Get out of Jail Free.",
                "Go to Jail.",
                "Grand Opera Night. Collect $50 from each player.",
                "Xmas fund matures, Collect 100",
                "Income tax refund. Collect $20.",
                "It's your birthday. Collect $10 from each player.",
                "Life insurance matures, Collect 100",
                "Hospital Fees. Pay $50.",
                "School Fees. Pay $50.",
                "Receive $25 consultancy fees.",
                "Street repairs: Pay $40 per house and $115 per hotel.",
                "You have won a second prize in a beauty contest. Collect $10.",
                "You inherit $100."
            };
        public void CollectRent(Player Current, Property p)
        {
            p.Rent.Collect(Current, p);
        }

        public void None(Player Current, Property p)
        {

        }

        public void ForSale(Player Current, Property p)
        {
            //implement puchase gui
        }

        public void onPurchase(Player Current, Property p)
        {
            p.Owner = Current;

            if (p.Group == Groups.Utilities)
                Current.NumberofUtilities++;
            else
            if (p.Group == Groups.Railroads)
                Current.NumberofRailroads++;
            else
            {
                Current.PropertiesValue += p.Cost;
                Current.NumberofProperties++;
                bool OwnsAll = true;
                foreach (var item in Game1.Board)
                {
                    if (item.Owner != Current && item.Group == p.Group) OwnsAll = false;
                }
                if (OwnsAll)
                {
                    Game1.Logs[Current.PlayerID].AddLine("Bought All of this Color");
                    Current.OwnsAll.Add(p.Group);
                }
            }
        }
        public void CommunityChest(Player Current, Property p)
        {
            picked = Die.rnd.Next(17);
            
            Game1.Logs[Current.PlayerID].AddLine(CommunityChestText[picked]);
            switch (picked)
            {
                case 0:
                    Current.CurrentSquare = 0;
                    Current.Collect(200);
                    break;

                case 1:
                    Current.Collect(200);
                    break;

                case 2:
                    Current.Pay(50);
                    break;
                case 3:
                    Current.Collect(50);
                    break;
                case 4:
                    Current.GetOutofJailCards++;
                    break;
                case 5:
                    Current.GotoJail();
                    break;
                case 6:
                    foreach (var player in Game1.g.Players)
                    {
                        if (player == Current) continue;
                        Current.Collect(50);
                        player.Pay(50);
                    }
                    break;
                case 7:
                    Current.Collect(100);
                    break;
                case 8:
                    Current.Collect(20);
                    break;
                case 9:
                    foreach (var player in Game1.g.Players)
                    {
                        if (player == Current) continue;
                        Current.Collect(10);
                        player.Pay(10);
                    }
                    break;
                case 10:
                    Current.Collect(100);
                    break;
                case 11:
                    Current.Pay(50);
                    break;
                case 12:
                    Current.Pay(50);
                    break;
                case 13:
                    Current.Collect(25);
                    break;
                case 14:
                    int sum = Current.HouseCount * 40 + Current.HotelCount * 115;
                    Current.Pay(sum);
                    Game1.Logs[Current.PlayerID].AddLine("Paid $" + sum.ToString() + " for repairs.");
                    break;
                case 15:
                    Current.Collect(10);
                    break;
                case 16:
                    Current.Collect(100);
                    break;

                default:
                    break;
            }
        }
        public void Chance(Player Current, Property p)
        {
            picked = Die.rnd.Next(16);
            
            Game1.Logs[Current.PlayerID].AddLine(ChanceText[picked]);
            switch (picked)
            {
                case 0:
                    Current.CurrentSquare = 0;
                    Current.Collect(200);
                    break;

                case 1:
                    Current.MoveTo(24);
                    break;

                case 2:
                    Current.MoveTo(11);
                    break;
                case 3:

                    if (Current.CurrentSquare < 12 || Current.CurrentSquare > 28)
                    {
                        Current.MoveTo(12, true, Game1.Board[12].ForSale);
                        if (Game1.Board[12].Owner == null) return;
                    }
                    else
                    {
                        Current.MoveTo(28, true, Game1.Board[28].ForSale);
                        if (Game1.Board[28].Owner == null) return;
                    }
                    Game1.Board[Current.CurrentSquare].Owner.Collect((Game1.die[0].Value + Game1.die[1].Value) * 10);
                    Current.Pay((Game1.die[0].Roll() + Game1.die[1].Roll()) * 10);

                    break;
                case 4:
                    Current.MoveTo((Current.CurrentSquare / 10 * 10 + ((Current.CurrentSquare % 10 < 5) ? 5:15))% 40);
                    Game1.Board[Current.CurrentSquare].LandAction(Current, Game1.Board[Current.CurrentSquare]);
                    break;
                case 5:
                    Current.MoveTo((Current.CurrentSquare / 10 * 10 + ((Current.CurrentSquare % 10 < 5) ? 5 : 15)) % 40);
                    Game1.Board[Current.CurrentSquare].LandAction(Current, Game1.Board[Current.CurrentSquare]);
                    break;
                case 6:
                    Current.Collect(50);
                    break;
                case 7:
                    Current.GetOutofJailCards++;
                    break;
                case 8:
                    Current.MoveTo((Current.CurrentSquare + 37) % 40,false);
                    break;
                case 9:
                    Current.GotoJail();
                    break;
                case 10:
                    int sum = Current.HouseCount * 25 + Current.HotelCount * 100;
                    Current.Pay(sum);
                    Game1.Logs[Current.PlayerID].AddLine("Paid $" + sum.ToString() + " for repairs.");
                    break;
                case 11:
                    Current.Pay(15);
                    break;
                case 12:
                    Current.MoveTo(5);
                    break;
                case 13:
                    Current.MoveTo(39);
                    break;
                case 14:
                    foreach (var player in Game1.g.Players)
                    {
                        if (player == Current) continue;
                        player.Collect(50);
                        Current.Pay(50);
                    }
                    break;
                case 15:
                    Current.Collect(150);
                    break;
            
               default:
                    break;
            }

        }

    }



    
}

