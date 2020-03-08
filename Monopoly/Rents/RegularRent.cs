using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class RegularRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
            if (p.Owner == Current || p.Mortgaged) return;
            int tmp = (p.Improvements == 0 && p.Owner.OwnsAll.Contains(p.Group)) ? 2 : 1;

            p.Owner.Collect(tmp * p.RentStructure[p.Improvements]);
            Game1.Logs[p.Owner.PlayerID].AddLine(
                " for " + p.Name + ((p.Improvements == 0) ? "" : " with " + p.Improvements + ((p.Improvements > 1) ? "Houses" : "House")));
            Game1.Logs[p.Owner.PlayerID].AddLine("Collect $"
                + (tmp * p.RentStructure[p.Improvements]).ToString() + "   from Player " + Current.PlayerID.ToString());

            Current.Pay(tmp * p.RentStructure[p.Improvements]);
            Game1.Logs[Current.PlayerID].AddLine("Pay $"
                + (tmp * p.RentStructure[p.Improvements]).ToString()
                + " to Player " + p.Owner.PlayerID.ToString());

        }
    }
    public class RailRoadRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
            if (p.Owner == Current) return;

            p.Owner.Collect(p.RentStructure[p.Owner.NumberofRailroads]);
            Current.Pay(p.RentStructure[p.Owner.NumberofRailroads]);


        }
    }
    public class UtilityRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
            if (p.Owner == Current) return;
            int rent = (Game1.die[0].Value + Game1.die[1].Value) * 
                ((p.Owner.NumberofUtilities == 1) ? 4 : 10);
            p.Owner.Collect(rent);
            Current.Pay(rent);
            


        }
    }
    public class FreeParkingRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
            Current.Collect(Game1.FreeParking);
            Game1.FreeParking = 500;
        }
    }
    public class NoRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
        }
    }
    public class LuxuryTaxRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
            Current.Pay(75);
        }
    }
    public class IncomeTaxRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
            Current.Pay(200);
        }
    }
    public class GotoJailRent : RentType
    {
        public void Collect(Player Current, Property p)
        {
            Current.GotoJail();
        }
    }
}
