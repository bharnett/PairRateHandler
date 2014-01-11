using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PairRateHandler
{
    public class Pair
    {
        public int CCY1Rank { get; set; }
        public int CCY2Rank { get; set; }
        public string CCY1 { get; set; }
        public string CCY2 { get; set; }
        public string PairString { get; set; }

        public Pair() { }

        /// <summary>
        /// New Pair class with ranked currencies and pair string.  
        /// </summary>
        /// <param name="ccy1">Three letter currency code</param>
        /// <param name="ccy2">Three letter currency code</param>
        public Pair(string ccy1, string ccy2)
        {
            this.CCY1 = ccy1.ToUpper();
            this.CCY2 = ccy2.ToUpper();
            GetRanks();
            GetPair();
        }

        /// <summary>
        /// New Pair class with ranked currencies and pair string.  
        /// </summary>
        /// <param name="ccyPair">Currency pair code ex: EURUSD or EUR/USD</param>
        public Pair(string ccyPair)
        {
            this.CCY1 = LeftRightMid.Left(ccyPair.ToUpper(), 3);
            this.CCY2 = LeftRightMid.Right(ccyPair.ToUpper(), 3);
            GetRanks();
            GetPair();
        }

        public override string ToString()
        {
            return this.PairString;
        }

        /// <summary>
        /// Gets the CCY and assocaited ranks properites for the Pair class.  CCY1 and CCY2 are 
        /// changed to reflect the rank with CCY1 always being the higher ranked currency.  
        /// </summary>
        public void GetRanks()
        {
            if (!string.IsNullOrWhiteSpace(this.CCY1) && !string.IsNullOrWhiteSpace(this.CCY2))
            {
                CCY1Rank = RateTools.Rankings.IndexOf(this.CCY1.ToUpper().Trim());
                CCY2Rank = RateTools.Rankings.IndexOf(this.CCY2.ToUpper().Trim());
            }
            // ccy1 is always the higher ranked
            if (CCY1Rank > CCY2Rank)
            {
                string tempCCY = CCY1;
                int tempRank = CCY1Rank;
                CCY1 = CCY2;
                CCY2 = tempCCY;
                CCY1Rank = CCY2Rank;
                CCY2Rank = tempRank;
            }

        }

        /// <summary>
        /// Populates the PairString property with a ranked currency pair string.
        /// </summary>
        /// <param name="NoSlash">Optional boolean to omit "/" from the pair string</param>
        public void GetPair(bool NoSlash = false)
        {
            if (CCY1Rank == -1 || CCY2Rank == -1)
            {
                this.PairString = string.Empty;
            }
            else if (CCY1Rank < CCY2Rank)
            {
                this.PairString = string.Format("{0}/{1}", CCY1, CCY2);
            }
            else
            {
                this.PairString = string.Format("{1}/{0}", CCY1, CCY2);
            }
            if (NoSlash)
            {
                this.PairString = this.PairString.Replace("//", string.Empty);
            }

        }
    }
}
