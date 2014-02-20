using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PairRateHandler
{

    public class PairRate : Pair
    {
        public double Amt1 { get; set; }
        public double Amt2 { get; set; }
        public double Rate { get; set; }

        /// <summary>
        /// Creates new PairRate with rankings, pair string, and optional rate if ammounts are provided
        /// </summary>
        /// <param name="ccy1"></param>
        /// <param name="ccy2"></param>
        /// <param name="amt1"></param>
        /// <param name="amt2"></param>
        public PairRate(string ccy1, string ccy2, double amt1 = 0, double amt2 = 0)
        {
            CCY1 = ccy1;
            CCY2 = ccy2;
            Amt1 = amt1;
            Amt2 = amt2;
            GetRanks();
            if (amt1 != 0 && amt2 != 0)
            {
                GetRate();
            }
            GetPair();
        }

        /// <summary>
        /// Creates new PairRate with rankings and pair string.
        /// </summary>
        /// <param name="p"></param>
        public PairRate(Pair p)
        {
            CCY1 = p.CCY1;
            CCY2 = p.CCY2;
            GetRanks();
            GetPair();
        }
        
        public PairRate()
        {
        }

        /// <summary>
        /// Returns a rate depending on the PairRates Amt1 and Amd2 properties and currency ranks.  
        /// </summary>
        public void GetRate()
        {
            if (CCY1Rank == -1 || CCY2Rank == -1)
            {
                this.Rate = 0;
            }
            else if (CCY1Rank < CCY2Rank)
            {
                this.Rate = (Amt1 / Amt2);
            }
            else
            {
                this.Rate = (Amt2 / Amt1);
            }
        }

        /// <summary>
        /// Populates the PairRate's Amt2 with a a calculated counter amount.  Amt1 and Rate are also populated during calculation.
        /// Requires PairRateList to be populated.
        /// </summary>
        /// <param name="givenAmt">Known amount for calculation</param>
        public void GetCounterAmt(double givenAmt)
        {
            //use this to get amout 2 if not provided
            this.Amt1 = givenAmt;
            this.Amt2 = 0;
            this.FindRate();

            if (CCY1Rank < CCY2Rank)
            {
                this.Amt2 = this.Amt1 * this.Rate;
            }
            else
            {
                this.Amt2 = this.Amt1 / this.Rate;
            }
        }

        /// <summary>
        /// Populates a rate for the PairRate object from the PairRateList.  If no rate exists, a search of the PairRateList
        /// for eligible cross pairs will execute.  Success will create a cross rate.  Requires PairRateList to be populated. 
        /// </summary>
        /// <param name="specPair1">Optional Pair object for first currency cross</param>
        /// <param name="specPair2">Optional Pair object for second currency cross</param>
        public void FindRate(Pair specPair1 = null, Pair specPair2 =null)
        {
            //find existing rate in list if possible
            double appliedRate = RateTools.PairRateList.Where(p => p.Key.PairString == this.PairString).FirstOrDefault().Value;

            if (appliedRate == 0)
            {
                RateMaker rc = new RateMaker();
                //find acceptable cross rates
                if (specPair1 == null || specPair2 == null)
                {
                    //find it based on linq
                    specPair1 = RateTools.PairRateList.Where(p => p.Key.ToString().Contains(this.CCY1)).FirstOrDefault().Key;
                    specPair2 = RateTools.PairRateList.Where(p => p.Key.ToString().Contains(this.CCY2) && !p.Key.ToString().Contains(this.CCY1)).FirstOrDefault().Key;
                }

                //if specific pairs are passed in, use those
                rc.Pair1 = specPair1;
                rc.Pair2 = specPair2;
                this.Rate = rc.CalcNewRate().Rate;
            }
            else
            {
                this.Rate = appliedRate;
            }

        }
    }

}
