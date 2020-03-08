using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monopoly
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState ms = new MouseState(), oms;
        const int PLAYERCOUNT = 2;
        public Player[] Players = new Player[PLAYERCOUNT];
        public static Log[] Logs = new Log[PLAYERCOUNT];
        PlayerStats[] Stats = new PlayerStats[PLAYERCOUNT];
        int count = 0;
        Texture2D BoardTex;
        Rectangle BoardRect = new Rectangle(0, 0, 768, 768);

        public static Property[] Board = new Property[40];

        public static Game1 g;

        public static Die[] die = new Die[2];

        public static int FreeParking = 500;
        public Actions a = new Actions();

        public static Card Card = new Card(101, 101);

        public static int Turn = -1;

        public static NoRent NoRent = new NoRent();
        public static RegularRent RegularRent = new RegularRent();
        public static UtilityRent UtilityRent = new UtilityRent();
        public static RailRoadRent RailRoadRent = new RailRoadRent();

        public Game1()
        {
            g = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;

            for (int i = 0; i < PLAYERCOUNT; i++)
            {
                Players[i] = new Player(i);
                Stats[i] = new PlayerStats(100 + i * 140, 400, Players[i]);
                Logs[i] = new Log(380, 100 + 200 * i);
            }
           

        }
        public void Setup()
        {
            Board[0] = new Property();
            Board[0].Owner = null;
            Board[0].Name = "Go";
            Board[0].Group = Groups.None;
            Board[0].ForSale = false;
            Board[0].Mortgaged = false;
            Board[0].MortgageValue = 0;
            Board[0].Cost=0;
            Board[0].Rent = NoRent;
            Board[0].LandAction = a.None;
            Board[0].PurchaseAction = a.None;
            Board[0].Improvements = 0;
            Board[0].RentStructure= new int[] { 0};
            Board[0].ImprovementCost = 0;

            Board[1] = new Property();
            Board[1].Owner = null;
            Board[1].Name = "Mediterranean Avenue";
            Board[1].Group = Groups.Brown;
            Board[1].ForSale = true;
            Board[1].Mortgaged = false;
            Board[1].MortgageValue = 30;
            Board[1].Cost = 60;
            Board[1].Rent = RegularRent;
            Board[1].LandAction = a.CollectRent;
            Board[1].PurchaseAction = a.onPurchase;
            Board[1].Improvements = 0;
            Board[1].RentStructure = new int[] { 2, 10, 30, 90, 160, 250 };
            Board[1].ImprovementCost = 50;

            Board[2] = new Property();
            Board[2].Owner = null;
            Board[2].Name = "Community Chest";
            Board[2].Group = Groups.None;
            Board[2].ForSale = false;
            Board[2].Mortgaged = false;
            Board[2].MortgageValue = 0;
            Board[2].Cost = 0;
            Board[2].Rent = NoRent;
            Board[2].LandAction = a.CommunityChest;
            Board[2].PurchaseAction = a.None;
            Board[2].Improvements = 0;
            Board[2].RentStructure = new int[] { 0 };
            Board[2].ImprovementCost = 0;

            Board[3] = new Property();
            Board[3].Owner = null;
            Board[3].Name = "Baltic Avenue";
            Board[3].Group = Groups.Brown;
            Board[3].ForSale = true;
            Board[3].Mortgaged = false;
            Board[3].MortgageValue = 30;
            Board[3].Cost = 60;
            Board[3].Rent = RegularRent;
            Board[3].LandAction = a.CollectRent;
            Board[3].PurchaseAction = a.onPurchase;
            Board[3].Improvements = 0;
            Board[3].RentStructure = new int[] { 4, 20, 60, 180, 320, 450 };
            Board[3].ImprovementCost = 50;

            Board[4] = new Property();
            Board[4].Owner = null;
            Board[4].Name = "Income Tax";
            Board[4].Group = Groups.None;
            Board[4].ForSale = false;
            Board[4].Mortgaged = false;
            Board[4].MortgageValue = 0;
            Board[4].Cost = 0;
            Board[4].Rent = new IncomeTaxRent();
            Board[4].LandAction = a.None;
            Board[4].PurchaseAction = a.None;
            Board[4].Improvements = 0;
            Board[4].RentStructure = new int[] { 0 };
            Board[4].ImprovementCost = 0;

            Board[5] = new Property();
            Board[5].Owner = null;
            Board[5].Name = "Reading Railroad";
            Board[5].Group = Groups.Railroads;
            Board[5].ForSale = true;
            Board[5].Mortgaged = false;
            Board[5].MortgageValue = 100;
            Board[5].Cost = 200;
            Board[5].Rent = RailRoadRent;
            Board[5].LandAction = a.CollectRent;
            Board[5].PurchaseAction = a.onPurchase;
            Board[5].Improvements = 0;
            Board[5].RentStructure = new int[] { 0, 25,50,100,200 };
            Board[5].ImprovementCost = 0;

            Board[6] = new Property();
            Board[6].Owner = null;
            Board[6].Name = "Oriental Avenue";
            Board[6].Group = Groups.LightBlue;
            Board[6].ForSale = true;
            Board[6].Mortgaged = false;
            Board[6].MortgageValue = 50;
            Board[6].Cost = 100;
            Board[6].Rent = RegularRent;
            Board[6].LandAction = a.CollectRent;
            Board[6].PurchaseAction = a.onPurchase;
            Board[6].Improvements = 0;
            Board[6].RentStructure = new int[] { 6, 30, 90, 270, 400, 550 };
            Board[6].ImprovementCost = 50;

            Board[7] = new Property();
            Board[7].Owner = null;
            Board[7].Name = "Chance";
            Board[7].Group = Groups.None;
            Board[7].ForSale = false;
            Board[7].Mortgaged = false;
            Board[7].MortgageValue = 0;
            Board[7].Cost = 0;
            Board[7].Rent = NoRent;
            Board[7].LandAction = a.Chance;
            Board[7].PurchaseAction = a.None;
            Board[7].Improvements = 0;
            Board[7].RentStructure = new int[] { 0 };
            Board[7].ImprovementCost = 0;

            Board[8] = new Property();
            Board[8].Owner = null;
            Board[8].Name = "Vermont Avenue";
            Board[8].Group = Groups.LightBlue;
            Board[8].ForSale = true;
            Board[8].Mortgaged = false;
            Board[8].MortgageValue = 50;
            Board[8].Cost = 100;
            Board[8].Rent = RegularRent;
            Board[8].LandAction = a.CollectRent;
            Board[8].PurchaseAction = a.onPurchase;
            Board[8].Improvements = 0;
            Board[8].RentStructure = new int[] { 6, 30, 90, 270, 400, 550 };
            Board[8].ImprovementCost = 50;

            Board[9] = new Property();
            Board[9].Owner = null;
            Board[9].Name = "Connecticut Avenue";
            Board[9].Group = Groups.LightBlue;
            Board[9].ForSale = true;
            Board[9].Mortgaged = false;
            Board[9].MortgageValue = 60;
            Board[9].Cost = 120;
            Board[9].Rent = RegularRent;
            Board[9].LandAction = a.CollectRent;
            Board[9].PurchaseAction = a.onPurchase;
            Board[9].Improvements = 0;
            Board[9].RentStructure = new int[] { 8, 40, 100, 300, 450, 600 };
            Board[9].ImprovementCost = 50;

            Board[10] = new Property();
            Board[10].Owner = null;
            Board[10].Name = "Jail";
            Board[10].Group = Groups.None;
            Board[10].ForSale = false;
            Board[10].Mortgaged = false;
            Board[10].MortgageValue = 0;
            Board[10].Cost = 0;
            Board[10].Rent = NoRent;
            Board[10].LandAction = a.None;
            Board[10].PurchaseAction = a.None;
            Board[10].Improvements = 0;
            Board[10].RentStructure = new int[] { 0 };
            Board[10].ImprovementCost = 0;

            Board[11] = new Property();
            Board[11].Owner = null;
            Board[11].Name = "St. Charles Place";
            Board[11].Group = Groups.Pink;
            Board[11].ForSale = true;
            Board[11].Mortgaged = false;
            Board[11].MortgageValue = 70;
            Board[11].Cost = 140;
            Board[11].Rent = RegularRent;
            Board[11].LandAction = a.CollectRent;
            Board[11].PurchaseAction = a.onPurchase;
            Board[11].Improvements = 0;
            Board[11].RentStructure = new int[] { 10, 50, 150, 450, 625, 750 };
            Board[11].ImprovementCost = 100;

            Board[12] = new Property();
            Board[12].Owner = null;
            Board[12].Name = "Electric Company";
            Board[12].Group = Groups.Utilities;
            Board[12].ForSale = true;
            Board[12].Mortgaged = false;
            Board[12].MortgageValue = 75;
            Board[12].Cost = 150;
            Board[12].Rent = UtilityRent;
            Board[12].LandAction = a.CollectRent;
            Board[12].PurchaseAction = a.onPurchase;
            Board[12].Improvements = 0;
            Board[12].RentStructure = new int[] { 0};
            Board[12].ImprovementCost = 0;

            Board[13] = new Property();
            Board[13].Owner = null;
            Board[13].Name = "States Avenue";
            Board[13].Group = Groups.Pink;
            Board[13].ForSale = true;
            Board[13].Mortgaged = false;
            Board[13].MortgageValue = 70;
            Board[13].Cost = 140;
            Board[13].Rent = RegularRent;
            Board[13].LandAction = a.CollectRent;
            Board[13].PurchaseAction = a.onPurchase;
            Board[13].Improvements = 0;
            Board[13].RentStructure = new int[] { 10, 50, 150, 450, 625, 750 };
            Board[13].ImprovementCost = 100;

            Board[14] = new Property();
            Board[14].Owner = null;
            Board[14].Name = "Virginia Avenue";
            Board[14].Group = Groups.Pink;
            Board[14].ForSale = true;
            Board[14].Mortgaged = false;
            Board[14].MortgageValue = 80;
            Board[14].Cost = 160;
            Board[14].Rent = RegularRent;
            Board[14].LandAction = a.CollectRent;
            Board[14].PurchaseAction = a.onPurchase;
            Board[14].Improvements = 0;
            Board[14].RentStructure = new int[] { 12, 60, 180, 500, 700, 900 };
            Board[14].ImprovementCost = 100;
            
            //RailRoad 2
            Board[15] = new Property();
            Board[15].Owner = null;
            Board[15].Name = "Pennsylvania Railroad";
            Board[15].Group = Groups.Railroads;
            Board[15].ForSale = true;
            Board[15].Mortgaged = false;
            Board[15].MortgageValue = 100;
            Board[15].Cost = 200;
            Board[15].Rent = RailRoadRent;
            Board[15].LandAction = a.CollectRent;
            Board[15].PurchaseAction = a.onPurchase;
            Board[15].Improvements = 0;
            Board[15].RentStructure = new int[] { 0, 25, 50, 100, 200 };
            Board[15].ImprovementCost = 0;

            //Orange Property 1
            Board[16] = new Property();
            Board[16].Owner = null;
            Board[16].Name = "St. James Place";
            Board[16].Group = Groups.Orange;
            Board[16].ForSale = true;
            Board[16].Mortgaged = false;
            Board[16].MortgageValue = 90;
            Board[16].Cost = 180;
            Board[16].Rent = RegularRent;
            Board[16].LandAction = a.CollectRent;
            Board[16].PurchaseAction = a.onPurchase;
            Board[16].Improvements = 0;
            Board[16].RentStructure = new int[] { 14, 70, 200, 550, 750, 950 };
            Board[16].ImprovementCost = 100;

            //Community Chest 2
            Board[17] = new Property();
            Board[17].Owner = null;
            Board[17].Name = "Community Chest";
            Board[17].Group = Groups.None;
            Board[17].ForSale = false;
            Board[17].Mortgaged = false;
            Board[17].MortgageValue = 0;
            Board[17].Cost = 0;
            Board[17].Rent = NoRent;
            Board[17].LandAction = a.CommunityChest;
            Board[17].PurchaseAction = a.None;
            Board[17].Improvements = 0;
            Board[17].RentStructure = new int[] { 0 };
            Board[17].ImprovementCost = 0;

            //Orange Property 2
            Board[18] = new Property();
            Board[18].Owner = null;
            Board[18].Name = "Tennessee Avenue";
            Board[18].Group = Groups.Orange;
            Board[18].ForSale = true;
            Board[18].Mortgaged = false;
            Board[18].MortgageValue = 90;
            Board[18].Cost = 180;
            Board[18].Rent = RegularRent;
            Board[18].LandAction = a.CollectRent;
            Board[18].PurchaseAction = a.onPurchase;
            Board[18].Improvements = 0;
            Board[18].RentStructure = new int[] { 14, 70, 200, 550, 750, 950 };
            Board[18].ImprovementCost = 100;

            //Orange Property 3
            Board[19] = new Property();
            Board[19].Owner = null;
            Board[19].Name = "New York Avenue";
            Board[19].Group = Groups.Orange;
            Board[19].ForSale = true;
            Board[19].Mortgaged = false;
            Board[19].MortgageValue = 100;
            Board[19].Cost = 200;
            Board[19].Rent = RegularRent;
            Board[19].LandAction = a.CollectRent;
            Board[19].PurchaseAction = a.onPurchase;
            Board[19].Improvements = 0;
            Board[19].RentStructure = new int[] { 16, 80, 220, 600, 800, 1000 };
            Board[19].ImprovementCost = 100;

            //Free Parking
            Board[20] = new Property();
            Board[20].Owner = null;
            Board[20].Name = "Free Parking";
            Board[20].Group = Groups.None;
            Board[20].ForSale = false;
            Board[20].Mortgaged = false;
            Board[20].MortgageValue = 0;
            Board[20].Cost = 0;
            Board[20].Rent = new FreeParkingRent();
            Board[20].LandAction = a.CollectRent;
            Board[20].PurchaseAction = a.None;
            Board[20].Improvements = 0;
            Board[20].RentStructure = new int[] { 0 };
            Board[20].ImprovementCost = 0;

            //Red Property 1
            Board[21] = new Property();
            Board[21].Owner = null;
            Board[21].Name = "Kentucky Avenue";
            Board[21].Group = Groups.Red;
            Board[21].ForSale = true;
            Board[21].Mortgaged = false;
            Board[21].MortgageValue = 110;
            Board[21].Cost = 220;
            Board[21].Rent = RegularRent;
            Board[21].LandAction = a.CollectRent;
            Board[21].PurchaseAction = a.onPurchase;
            Board[21].Improvements = 0;
            Board[21].RentStructure = new int[] { 18, 90, 250, 700, 875, 1050 };
            Board[21].ImprovementCost = 150;

            Board[22] = new Property();
            Board[22].Owner = null;
            Board[22].Name = "Chance";
            Board[22].Group = Groups.None;
            Board[22].ForSale = false;
            Board[22].Mortgaged = false;
            Board[22].MortgageValue = 0;
            Board[22].Cost = 0;
            Board[22].Rent = NoRent;
            Board[22].LandAction = a.Chance;
            Board[22].PurchaseAction = a.None;
            Board[22].Improvements = 0;
            Board[22].RentStructure = new int[] { 0 };
            Board[22].ImprovementCost = 0;

            //Red Property 2
            Board[23] = new Property();
            Board[23].Owner = null;
            Board[23].Name = "Indiana Avenue";
            Board[23].Group = Groups.Red;
            Board[23].ForSale = true;
            Board[23].Mortgaged = false;
            Board[23].MortgageValue = 110;
            Board[23].Cost = 220;
            Board[23].Rent = RegularRent;
            Board[23].LandAction = a.CollectRent;
            Board[23].PurchaseAction = a.onPurchase;
            Board[23].Improvements = 0;
            Board[23].RentStructure = new int[] { 18, 90, 250, 700, 875, 1050 };
            Board[23].ImprovementCost = 150;

            //Red Property 1
            Board[24] = new Property();
            Board[24].Owner = null;
            Board[24].Name = "Illinois Avenue";
            Board[24].Group = Groups.Red;
            Board[24].ForSale = true;
            Board[24].Mortgaged = false;
            Board[24].MortgageValue = 110;
            Board[24].Cost = 240;
            Board[24].Rent = RegularRent;
            Board[24].LandAction = a.CollectRent;
            Board[24].PurchaseAction = a.onPurchase;
            Board[24].Improvements = 0;
            Board[24].RentStructure = new int[] { 20, 100, 300, 750, 925, 1100 };
            Board[24].ImprovementCost = 150;

            //RailRoad 3
            Board[25] = new Property();
            Board[25].Owner = null;
            Board[25].Name = "B & O Railroad";
            Board[25].Group = Groups.Railroads;
            Board[25].ForSale = true;
            Board[25].Mortgaged = false;
            Board[25].MortgageValue = 100;
            Board[25].Cost = 200;
            Board[25].Rent = RailRoadRent;
            Board[25].LandAction = a.CollectRent;
            Board[25].PurchaseAction = a.onPurchase;
            Board[25].Improvements = 0;
            Board[25].RentStructure = new int[] { 0, 25, 50, 100, 200 };
            Board[25].ImprovementCost = 0;

            //Yellow Property 1
            Board[26] = new Property();
            Board[26].Owner = null;
            Board[26].Name = "Atlantic Avenue";
            Board[26].Group = Groups.Yellow;
            Board[26].ForSale = true;
            Board[26].Mortgaged = false;
            Board[26].MortgageValue = 130;
            Board[26].Cost = 260;
            Board[26].Rent = RegularRent;
            Board[26].LandAction = a.CollectRent;
            Board[26].PurchaseAction = a.onPurchase;
            Board[26].Improvements = 0;
            Board[26].RentStructure = new int[] { 22, 110, 330, 800, 975, 1150 };
            Board[26].ImprovementCost = 150;
            //Yellow Property 2
            Board[27] = new Property();
            Board[27].Owner = null;
            Board[27].Name = "Ventnor Avenue";
            Board[27].Group = Groups.Yellow;
            Board[27].ForSale = true;
            Board[27].Mortgaged = false;
            Board[27].MortgageValue = 130;
            Board[27].Cost = 260;
            Board[27].Rent = RegularRent;
            Board[27].LandAction = a.CollectRent;
            Board[27].PurchaseAction = a.onPurchase;
            Board[27].Improvements = 0;
            Board[27].RentStructure = new int[] { 22, 110, 330, 800, 975, 1150 };
            Board[27].ImprovementCost = 150;

            Board[28] = new Property();
            Board[28].Owner = null;
            Board[28].Name = "Water Works";
            Board[28].Group = Groups.Utilities;
            Board[28].ForSale = true;
            Board[28].Mortgaged = false;
            Board[28].MortgageValue = 75;
            Board[28].Cost = 150;
            Board[28].Rent = UtilityRent;
            Board[28].LandAction = a.CollectRent;
            Board[28].PurchaseAction = a.onPurchase;
            Board[28].Improvements = 0;
            Board[28].RentStructure = new int[] { 0 };
            Board[28].ImprovementCost = 0;

            //Yellow Property 3
            Board[29] = new Property();
            Board[29].Owner = null;
            Board[29].Name = "Marvin Gardens";
            Board[29].Group = Groups.Yellow;
            Board[29].ForSale = true;
            Board[29].Mortgaged = false;
            Board[29].MortgageValue = 140;
            Board[29].Cost = 280;
            Board[29].Rent = RegularRent;
            Board[29].LandAction = a.CollectRent;
            Board[29].PurchaseAction = a.onPurchase;
            Board[29].Improvements = 0;
            Board[29].RentStructure = new int[] { 24, 120, 360, 850, 1025, 1200 };
            Board[29].ImprovementCost = 150;

            Board[30] = new Property();
            Board[30].Owner = null;
            Board[30].Name = "Goto Jail";
            Board[30].Group = Groups.None;
            Board[30].ForSale = false;
            Board[30].Mortgaged = false;
            Board[30].MortgageValue = 0;
            Board[30].Cost = 0;
            Board[30].Rent = new GotoJailRent();
            Board[30].LandAction = a.CollectRent;
            Board[30].PurchaseAction = a.None;
            Board[30].Improvements = 0;
            Board[30].RentStructure = new int[] { 0 };
            Board[30].ImprovementCost = 0;

            Board[31] = new Property();
            Board[31].Owner = null;
            Board[31].Name = "Pacific Avenue";
            Board[31].Group = Groups.Green;
            Board[31].ForSale = true;
            Board[31].Mortgaged = false;
            Board[31].MortgageValue = 150;
            Board[31].Cost = 300;
            Board[31].Rent = RegularRent;
            Board[31].LandAction = a.CollectRent;
            Board[31].PurchaseAction = a.onPurchase;
            Board[31].Improvements = 0;
            Board[31].RentStructure = new int[] { 26, 130, 390, 900, 1100, 1275 };
            Board[31].ImprovementCost = 200;

            Board[32] = new Property();
            Board[32].Owner = null;
            Board[32].Name = "North Carolina Avenue";
            Board[32].Group = Groups.Green;
            Board[32].ForSale = true;
            Board[32].Mortgaged = false;
            Board[32].MortgageValue = 150;
            Board[32].Cost = 300;
            Board[32].Rent = RegularRent;
            Board[32].LandAction = a.CollectRent;
            Board[32].PurchaseAction = a.onPurchase;
            Board[32].Improvements = 0;
            Board[32].RentStructure = new int[] { 26, 130, 390, 900, 1100, 1275 };
            Board[32].ImprovementCost = 200;

            Board[33] = new Property();
            Board[33].Owner = null;
            Board[33].Name = "Community Chest";
            Board[33].Group = Groups.None;
            Board[33].ForSale = false;
            Board[33].Mortgaged = false;
            Board[33].MortgageValue = 0;
            Board[33].Cost = 0;
            Board[33].Rent = NoRent;
            Board[33].LandAction = a.CommunityChest;
            Board[33].PurchaseAction = a.None;
            Board[33].Improvements = 0;
            Board[33].RentStructure = new int[] { 0 };
            Board[33].ImprovementCost = 0;

            Board[34] = new Property();
            Board[34].Owner = null;
            Board[34].Name = "Pennsylvania Avenue";
            Board[34].Group = Groups.Green;
            Board[34].ForSale = true;
            Board[34].Mortgaged = false;
            Board[34].MortgageValue = 160;
            Board[34].Cost = 320;
            Board[34].Rent = RegularRent;
            Board[34].LandAction = a.CollectRent;
            Board[34].PurchaseAction = a.onPurchase;
            Board[34].Improvements = 0;
            Board[34].RentStructure = new int[] { 28, 150, 450, 1000, 1200, 1400 };
            Board[34].ImprovementCost = 200;

            //RailRoad 3
            Board[35] = new Property();
            Board[35].Owner = null;
            Board[35].Name = "Short Line";
            Board[35].Group = Groups.Railroads;
            Board[35].ForSale = true;
            Board[35].Mortgaged = false;
            Board[35].MortgageValue = 100;
            Board[35].Cost = 200;
            Board[35].Rent = RailRoadRent;
            Board[35].LandAction = a.CollectRent;
            Board[35].PurchaseAction = a.onPurchase;
            Board[35].Improvements = 0;
            Board[35].RentStructure = new int[] { 0, 25, 50, 100, 200 };
            Board[35].ImprovementCost = 0;

            Board[36] = new Property();
            Board[36].Owner = null;
            Board[36].Name = "Chance";
            Board[36].Group = Groups.None;
            Board[36].ForSale = false;
            Board[36].Mortgaged = false;
            Board[36].MortgageValue = 0;
            Board[36].Cost = 0;
            Board[36].Rent = NoRent;
            Board[36].LandAction = a.Chance;
            Board[36].PurchaseAction = a.None;
            Board[36].Improvements = 0;
            Board[36].RentStructure = new int[] { 0 };
            Board[36].ImprovementCost = 0;

            Board[37] = new Property();
            Board[37].Owner = null;
            Board[37].Name = "Park Place";
            Board[37].Group = Groups.DarkBlue;
            Board[37].ForSale = true;
            Board[37].Mortgaged = false;
            Board[37].MortgageValue = 175;
            Board[37].Cost = 350;
            Board[37].Rent = RegularRent;
            Board[37].LandAction = a.CollectRent;
            Board[37].PurchaseAction = a.onPurchase;
            Board[37].Improvements = 0;
            Board[37].RentStructure = new int[] { 35, 175, 500, 1100, 1300, 1500 };
            Board[37].ImprovementCost = 200;

            Board[38] = new Property();
            Board[38].Owner = null;
            Board[38].Name = "Luxury Tax";
            Board[38].Group = Groups.None;
            Board[38].ForSale = false;
            Board[38].Mortgaged = false;
            Board[38].MortgageValue = 0;
            Board[38].Cost = 0;
            Board[38].Rent = new LuxuryTaxRent();
            Board[38].LandAction = a.CollectRent;
            Board[38].PurchaseAction = a.None;
            Board[38].Improvements = 0;
            Board[38].RentStructure = new int[] { 0 };
            Board[38].ImprovementCost = 0;

            Board[39] = new Property();
            Board[39].Owner = null;
            Board[39].Name = "Boardwalk";
            Board[39].Group = Groups.DarkBlue;
            Board[39].ForSale = true;
            Board[39].Mortgaged = false;
            Board[39].MortgageValue = 200;
            Board[39].Cost = 400;
            Board[39].Rent = RegularRent;
            Board[39].LandAction = a.CollectRent;
            Board[39].PurchaseAction = a.onPurchase;
            Board[39].Improvements = 0;
            Board[39].RentStructure = new int[] { 55, 200, 600, 1400, 1700, 2000 };
            Board[39].ImprovementCost = 200;



            //for (int i = 26; i < 40; i++)
            //{
            //    Board[i] = new Property();
            //    Board[i].Owner = null;
            //    Board[i].Name = "North Carolina Avenue";
            //    Board[i].Group = Groups.None;
            //    Board[i].ForSale = false;
            //    Board[i].Mortgaged = false;
            //    Board[i].MortgageValue = 0;
            //    Board[i].Cost = 0;
            //    Board[i].Rent = NoRent;
            //    Board[i].LandAction = a.None;
            //    Board[i].PurchaseAction = a.None;
            //    Board[i].Improvements = 0;
            //    Board[i].RentStructure = new int[] { 0 };
            //    Board[i].ImprovementCost = 0;
            //}

        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Die.Tex = Content.Load<Texture2D>("Die");
            Player.Tex = Content.Load<Texture2D>("Die");
            Card.CardTex = Content.Load<Texture2D>("Card");
            Card.SquareTex = Content.Load<Texture2D>("Square");
            Card.BigSf = Content.Load<SpriteFont>("Big");
            Card.SmallSf = Content.Load<SpriteFont>("Small");
            BoardTex = Content.Load<Texture2D>("Board");
            die[0] = new Die(64, 768);
            die[1] = new Die(97, 768);

            Setup();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            oms = ms;
            ms = Mouse.GetState();
            

            if (ms.RightButton != ButtonState.Pressed &&
                ((count++ & 15) == 0
                || ms.LeftButton != oms.LeftButton
                && ms.LeftButton == ButtonState.Pressed))
            {
                Turn++;
                Turn %= PLAYERCOUNT;
                while (Players[Turn].isDead)
                {
                    Turn++;
                    Turn %= PLAYERCOUNT;
                }
                Players[Turn].Roll();
            }
            if (ms.MiddleButton == ButtonState.Pressed) count = 0;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(BoardTex, BoardRect, Color.White);

            die[0].Draw(spriteBatch);
            die[1].Draw(spriteBatch);
            for (int i = 0; i < PLAYERCOUNT; i++)
            {
                Players[i].Draw(spriteBatch);
                Stats[i].Draw(spriteBatch);
                Logs[i].Draw(spriteBatch);
            }
           
            Card.Draw(spriteBatch);
            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
