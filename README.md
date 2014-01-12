PairRateHandler
===============

I created this library for use in .NET programs to handle Foreign Exchange pairs and rates.

Some common issues in FX revolve around using the correct convention of a pair, getting the rate in the correct convention
and being able to create rates between cross currencies.  I wanted to create a library that could support all those annoying thing
and then some for my own personal use.  I tried to write this up to be a clear as I can.  However, there are plenty of comments in the code for each class and the associated methods that I hope will help.  Furthermoe, feel free to let me know about any issues with the code and I'll fix them.  This is simple, but hugely useful if you are dealing with FX transactions.  

##NOTES - All currency codes should be the standard three digit codes, like EUR or USD.  

Everything revolves around a 
default US-based ranking file.  This ranks currencies for standard displays and standard rate formats.  For example,
EURUSD is the standard quote, with a rate of 1.37 on Jan 11, 2014.  This shows EUR per USD.  No one in the FX industry or 
will ever quote EURUSD as USDEUR of .73.  The ranking is a static list of strings in the 'RateTools' class called 'Rankings.'

## Pair Class
The base class that pretty much everything revolves around is the 'Pair' class.  It has 
two constructors and two functions and 5 properties:
- CCY1Rank (int)
- CCY2Rank (int)
- CCY1 (string)
- CCY2 (string)
- PairString (string)

### Constructors
One takes two two currency codes as two strings. 
The other takes a currency pair string, like (USDJPY or USD/JPY), as an input.  Each pair class has two currency codes, and 
the constructor will run two methods to get the rank of the currencies and populate the 'Pairstring' property with the 
correctly formatted string based on the rank.

### Functions
- GetRanks: Populates ranks of the supplied currencies.  CCY1 is always the higher ranked
- GetPair: Populates the 'Pairstring' poperty.  Accepts an optional boolean arg called 'NoSlash' that will populate the 'Pairstring' without a slash between the currencies (USDJPY vs USD/JPY).


## PairRate Class
This extends the 'Pair' class with support for rates and amounts around a FX transaction.  It has two constructors, three functions, and three new properties:
- Amt1 (double)
- Amt2 (double)
- Rate (double)

### Constructors
On takes two currency codes (string) and two respective amounts for those currency codes (double).  This will populate the Pair subclass and also the rate using the 'GetRate' function.  The other creates an empty PairRate taking a singular 'Pair' class.

### Functions
- GetRate: This returns a rate depending on the Amt1 and Amt2 properties and bases the display of the rate on the currency ranks.
- GetCounterAmt: Accepts a double 'givenAmt' argument and utilizes the 'FindRate' function to get a rate and counter amount for a FX transaction.  Requires the 'RateTools.PairRateList' to be popualted.
- FindRate:  Utilizes the 'RateTools.PairRateList' to find a rate for a given pair or, optionally, between two specified pairs via the optional argument of 'specPair1' and 'specPair2'.  By specifying the pairs, it will create a rate by crossing the two specified.  EX) EURUSD & USDJPY to get EURJPY.  Withouth specifying currencies it will find the first available pairs in the list that match or if a native rate is available it will get that from the list.  


##RateMaker Class
Utilizing two Pair classes and two associated rates, this returns a new PairRate class with a new Pair string and rate.  Requires PairRateList to be populated. It has four properties
- Pair1 (Pair)
- Pair2 (Pair)
- Rate1 (double)
- Rate2 (double)

### Functions
- CalcNewRate:  Utilizes all the prooperties to calculate a rate based on rates fround in the 'RateTools.PairRateList'.  If it cannot find a common currency between the two provided pairs, it will throw an InvalidOperationException.  Returns a PairRate class. 


##RateTools Class
Contains many static functions for calculation or providing functionality to the other classes.  There are two main properties:
- Rankings: A static list of string of all currency codes.  This is based on US perspective and is read only. 
- PairRateList: A dictionary of the Pair class and a double for storing pairs and associated rates.

### Functions
- GetRate: Returns a double that is the rate between two currencies and two amounts.
- GetRate: Returns a PairRate based on an input of two Pair classes.
- RateGetter: Returns a PairRate based ont two Pair inputs.  Requires the PairRateList to be populated as the cross rate needs to be calculated off a source.
- GetPair: Returns a string of the correct currency pair string taking two currency codes as an input.  Also supports an optional NoSlash boolean to return the pair without a '/' between the currencies.
- PopulatePairRateList: Takes an IEnumberable of string called pairs and a a IEnumerable of double called rates.  This populates the pair rate list by creating Pair classes for each passed in value.  Note: pairs and rates must be the same length.  
- GetPairCounterRate: Returns a PairRate and takes a string for the given currency code, a double for the given amount, and a string for the counter currency.  The pair rate will have the and the counter amount populated.  

