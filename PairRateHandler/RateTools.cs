using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PairRateHandler
{
    public class RateTools
    {
        //static ranking file
        /// <summary>
        /// Public list of all currency rankings.  Index in the list determines the rank.  
        /// </summary>
        public static List<string> Rankings
        {
            get
            {
                return new List<string>()
                {
                    "EUR", 
                    "GBP", 
                    "AUD", 
                    "NZD", 
                    "USD", 
                    "TOP", 
                    "WST", 
                    "BWP", 
                    "SBD", 
                    "ARS", 
                    "JOD", 
                    "KWD", 
                    "CAD", 
                    "BRL", 
                    "BHD", 
                    "OMR", 
                    "XDR", 
                    "KYD", 
                    "AZN", 
                    "GHS", 
                    "BSD", 
                    "CUP", 
                    "PAB", 
                    "CHF", 
                    "SGD", 
                    "TRY", 
                    "BAM", 
                    "BND", 
                    "FJD", 
                    "GEL", 
                    "ANG", 
                    "AWG", 
                    "BZD", 
                    "BBD", 
                    "SDG", 
                    "XCD", 
                    "SRD", 
                    "MYR", 
                    "PEN", 
                    "PLN", 
                    "QAR", 
                    "SAR", 
                    "ILS", 
                    "EGP", 
                    "TTD", 
                    "LSL", 
                    "NAD", 
                    "SZL", 
                    "BOB", 
                    "DKK", 
                    "NOK", 
                    "SEK", 
                    "GTQ", 
                    "HKD", 
                    "ZAR", 
                    "SCR", 
                    "MOP", 
                    "SVC", 
                    "ETB", 
                    "MXN", 
                    "CNY", 
                    "CNH", 
                    "MAD", 
                    "ZWD", 
                    "MVR", 
                    "NIO", 
                    "HNL", 
                    "GMD", 
                    "UYU", 
                    "MZN", 
                    "MUR", 
                    "TOF", 
                    "THB", 
                    "PHP", 
                    "TWD", 
                    "CZK", 
                    "SKK", 
                    "DOP", 
                    "KGS", 
                    "HTG", 
                    "BTN", 
                    "INR", 
                    "MKD", 
                    "RSD", 
                    "PKR", 
                    "NPR", 
                    "KES", 
                    "LKR", 
                    "ISK", 
                    "DZD", 
                    "JMD", 
                    "AOA", 
                    "XPF", 
                    "ALL", 
                    "VUV", 
                    "JPY", 
                    "MWK", 
                    "HUF", 
                    "YER", 
                    "MRO", 
                    "KMF", 
                    "XAF", 
                    "XOF", 
                    "CRC", 
                    "CLP", 
                    "CLF", 
                    "RWF", 
                    "CDF", 
                    "KRW", 
                    "BIF", 
                    "MNT", 
                    "UZS", 
                    "UGX", 
                    "MGA", 
                    "COP", 
                    "LBP", 
                    "VEF", 
                    "BYR", 
                    "SLL", 
                    "GNF", 
                    "PYG", 
                    "IDR", 
                    "RUB", 
                    "TND", 
                    "LVL", 
                    "LTL", 
                    "UAH", 
                    "EEK", 
                    "BGN", 
                    "BDT", 
                    "RON", 
                    "NGN", 
                    "HRK", 
                    "KZT", 
                    "AED", 
                    "IRR", 
                    "VND", 
                    "ZDN", 
                    "BMD", 
                    "PGK", 
                    "TZS"
                };

            }
        }

        //for getting rate between ccys and amounts
        /// <summary>
        /// Returns a rate between two currencies and two relative amounts.  Utilizes Rankings to calculate the correct rate. 
        /// </summary>
        /// <param name="ccy1">First currency in rate calculation</param>
        /// <param name="amt1">First amount in rate calculation</param>
        /// <param name="ccy2">Second currency in rate calculation</param>
        /// <param name="amt2">Second amount in rate calculation</param>
        /// <returns></returns>
        public static double GetRate(string ccy1, double amt1, string ccy2, double amt2)
        {
            PairRate pt = new PairRate(ccy1, ccy2, amt1, amt2);
            return pt.Rate;
        }

        /// <summary>
        /// Returns a PairRate object with rate populated between the two provided Pair objects.  Requires PairRateList to be populated.  
        /// </summary>
        /// <param name="pair1">First pair of the calculated cross</param>
        /// <param name="pair2">Second pair of the calculated cross</param>
        /// <returns></returns>
        public static PairRate GetRate(Pair pair1, Pair pair2)
        {
            return RateGetter(pair1, pair2);
        }

        /// <summary>
        /// Returns a PairRate object with a calculated cross rate based on two provided pair strings.  Requires PairRateList to be populated. 
        /// </summary>
        /// <param name="pairString1">First pair string of the calculated cross ex: EURUSD</param>
        /// <param name="pairString2">Second pair string of the calculate cross ex: GBPUSD</param>
        /// <returns></returns>
        public static PairRate GetRate(string pairString1, string pairString2)
        {
            Pair p1 = new Pair(pairString1);
            Pair p2 = new Pair(pairString2);

            return RateGetter(p1, p2);
        }
        
        /// <summary>
        /// Returns a PairRate object by searching the PairRateList and trying to find a match.  Returns null if no matching cross pairs found.
        /// </summary>
        /// <param name="pairString">A string the represent the currency pair to create a synthetic rate for.</param>
        /// <returns></returns>
        public static PairRate GetRate(string pairString)
        {
            Pair requestedPair = new Pair(pairString);
            PairRate newPair = new PairRate();

            //get all eligble currency pairs
            var ccy1PairRates = RateTools.PairRateList.Where(r => r.Key.CCY1 == requestedPair.CCY1 || r.Key.CCY2 == requestedPair.CCY1);
            var ccy2PairRates = RateTools.PairRateList.Where(r => r.Key.CCY1 == requestedPair.CCY2 || r.Key.CCY2 == requestedPair.CCY2);


            foreach (var ccy1Pair in ccy1PairRates)
            {
                foreach (var ccy2Pair in ccy2PairRates)
                {
                    try
                    {
                        newPair = RateGetter(ccy1Pair.Key, ccy2Pair.Key);
                    }
                    catch (Exception)
                    {
                        newPair = null;
                    }

                }
            }

            return newPair;
        }

        /// <summary>
        /// Returns a PairRate object with rate populated between the two provided Pair objects.  Requires PairRateList to be populated.  
        /// </summary>
        /// <param name="pair1">First pair of the calculated cross</param>
        /// <param name="pair2">Second pair of the calculated cross</param>
        /// <returns></returns>
        private static PairRate RateGetter(Pair pair1, Pair pair2)
        {
            if (RateTools.PairRateList == null || RateTools.PairRateList.Count == 0)
            {
                throw new InvalidOperationException("No items in pair rate list to calculate new rate.");
            }
            else if (RateTools.PairRateList.Keys.Where(p => p.PairString == pair1.PairString || p.PairString == pair2.PairString).Count() < 2)
            {
                throw new InvalidOperationException("Supplied pairs not available in rate list.");
            }
            else
            {
                RateMaker rc = new RateMaker();
                rc.Pair1 = pair1;
                rc.Pair2 = pair2;
                return rc.CalcNewRate();

            }
        }

        //for getting the pair string
        /// <summary>
        /// Returns a string of the ranked CCY pair
        /// </summary>
        /// <param name="ccy1">First currency string ex: EUR</param>
        /// <param name="ccy2">Second currency string ex: USD</param>
        /// <param name="NoSlash">Optional argument if the returned string should not include a slash (/) between the currencies</param>
        /// <returns></returns>
        public static string GetPair(string ccy1, string ccy2, bool NoSlash = false)
        {
            Pair p = new Pair(ccy1, ccy2);
            p.GetPair(NoSlash);
            return p.ToString();
        }

        //dictionary for your pricing source
        /// <summary>
        /// Dictionary for all pricing/cross calculation
        /// </summary>
        public static Dictionary<Pair, double> PairRateList { get; set; }

        //function for adding IEnumberables to list
        /// <summary>
        /// Takes two enumerables (array, list, collection) of currency pairs and associated rates.  They have to be passed in
        /// relative to each other - each index must correspond between the two enumerables.
        /// </summary>
        /// <param name="pairs">Enumerable of pairs in single string ex: EURUSD</param>
        /// <param name="rates">Enumerable of rates for their respective pair in pairs argument </param>
        public static void PopulatePairRateList(IEnumerable<string> pairs, IEnumerable<double> rates)
        {
            //ienumerables must be sorted and same length
            if (pairs.Count() != rates.Count())
            {
                throw new InvalidOperationException("Enumerable variables must have the same amount items");
            }
            else
            {
                RateTools.PairRateList = new Dictionary<Pair, double>();
                int i = 0;
                foreach (string p in pairs)
                {
                    Pair addPair = new Pair(p);
                    RateTools.PairRateList.Add(addPair, rates.Skip(i).Take(1).FirstOrDefault());
                    i++;
                }
            }
        }


        //for getting counter amt and rate
        /// <summary>
        /// Returns a PairRate object with the ranked pair, provided given ammount, and counter amount.  Requires PairRateList to 
        /// be populated and it will search for a valid cross currency within the PairRateList if the specified pair doesn't
        /// natively exist.
        /// </summary>
        /// <param name="ccyGiven">Known amount currency</param>
        /// <param name="amtGiven">Known amount</param>
        /// <param name="ccyCounter">Currency of unknown amount</param>
        /// <returns></returns>
        public static PairRate GetPairCounterRate(string ccyGiven, double amtGiven, string ccyCounter)
        {
            PairRate pr = new PairRate(ccyGiven, ccyCounter);
            pr.GetCounterAmt(amtGiven);
            return pr;
        }

    }
}
