using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PairRateHandler
{
    class RateMaker
    {

        public Pair Pair1 { get; set; }
        public Pair Pair2 { get; set; }
        public double Rate1 { get; set; }
        public double Rate2 { get; set; }

        /// <summary>
        /// Utilizing two Pair classes and two associated rates, this returns a new PairRate class
        /// with a new Pair string and rate.  Requires PairRateList to be populated.  
        /// </summary>
        /// <returns></returns>
        public PairRate CalcNewRate()
        {
            // find unmatching ccys
            Pair crossPair;
            double crossRate;

            Rate1 = RateTools.PairRateList.Where(p => p.Key.PairString == Pair1.PairString).First().Value;
            Rate2 = RateTools.PairRateList.Where(p => p.Key.PairString == Pair2.PairString).First().Value;

            if (Pair1.CCY1 == Pair2.CCY1)
            {
                // divide
                crossPair = new Pair(Pair1.CCY2, Pair2.CCY2);
                crossRate = crossPair.CCY1Rank < crossPair.CCY2Rank ? Rate2 / Rate1 : Rate1 / Rate2;
            }
            else if ((Pair1.CCY2 == Pair2.CCY1) || (Pair1.CCY1 == Pair2.CCY2))
            {
                crossPair = new Pair(Pair1.CCY1, Pair2.CCY2);
                crossRate = crossPair.CCY1Rank < crossPair.CCY2Rank ? Rate1 * Rate2 : Rate2 * Rate1;
            }
            else if (Pair1.CCY1 == Pair2.CCY2)
            {
                crossPair = new Pair(Pair1.CCY2, Pair2.CCY1);
                crossRate = crossPair.CCY1Rank < crossPair.CCY2Rank ? Rate1 * Rate2 : Rate2 * Rate1;
            }
            else if (Pair1.CCY2 == Pair2.CCY2)
            {
                crossPair = new Pair(Pair1.CCY1, Pair2.CCY1);
                crossRate = crossPair.CCY1Rank < crossPair.CCY2Rank ? Rate1 / Rate2 : Rate1 * (1 / Rate2);
            }
            else
            {
                throw new InvalidOperationException(string.Format("There is no common currency between the two pairs {0} and {1}", Pair1, Pair2));
            }

            PairRate pr = new PairRate(crossPair);
            pr.Rate = crossRate;

            return pr;
        }

    }

}
