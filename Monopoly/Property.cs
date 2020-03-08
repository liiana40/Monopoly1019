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
    public delegate void Action1(Player Current, Property p);
    public class Groups
    {
        public static Color None = Color.Transparent;
        public static Color Brown = new Color(148, 85, 52);
        public static Color LightBlue = new Color(172, 224, 247);
        public static Color Pink = new Color(214, 60, 150);
        public static Color Orange = new Color(247, 148, 31);
        public static Color Red = new Color(237, 24, 39);
        public static Color Yellow = Color.Yellow;
        public static Color Green = new Color(30, 178, 92);
        public static Color DarkBlue = new Color(1, 114, 184);
        public static Color Utilities = Color.White;
        public static Color Railroads = Color.WhiteSmoke;
    }

    
    public struct Property
    {
        public Player Owner;
        public string Name;
        public Color Group;
        public bool ForSale;
        public bool Mortgaged;
        public int MortgageValue;
        public int Cost;
        public RentType Rent;
        public Action1 LandAction; //default to Rent
        public Action1 PurchaseAction;
        public int Improvements;
        public int[] RentStructure;
        public int ImprovementCost;


    }
}
