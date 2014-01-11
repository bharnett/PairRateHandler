using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PairRateHandler;
namespace Debugger
{
    class Program
    {
        static void Main(string[] args)
        {

            RateTools.PopulatePairRateList(new string[] { "EURUSD", "GBPUSD", "USDCAD", "USDCHF", "USDJPY" },
                new double[] { 1.34, 1.55, 1.04, .92, 96.12 });

            //Pair pEURUSD = new Pair("EURUSD");
            //Pair pGBPUSD = new Pair("GBPUSD");
            //Pair pUSDCAD = new Pair("USDCAD");
            //Pair pUSDCHF = new Pair("USDCHF");
            //Pair pUSDJPY = new Pair("USDJPY");

            ////populate rates in 
            //RateTools.PairRateList = new Dictionary<Pair, double>();
            //RateTools.PairRateList.Add(pEURUSD, 1.34);
            //RateTools.PairRateList.Add(pGBPUSD, 1.55);
            //RateTools.PairRateList.Add(pUSDCAD, 1.04);
            //RateTools.PairRateList.Add(pUSDCHF, .92);
            //RateTools.PairRateList.Add(pUSDJPY, 96.12);


            PairRate pr = RateTools.GetRate("EURUSD", "GBPUSD");
            var amt = RateTools.GetPairCounterRate("EUR", 1000000, "GBP");

            
        }
    }
}
