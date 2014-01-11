PairRateHandler
===============

I created this library for use in .NET programs to handle Foreign Exchange pairs and rates.

Some common issues in FX revolve around using the correct convention of a pair, getting the rate in the correct convention
and being able to create rates between cross currencies.  I wanted to create a library that could support all those annoying thing
and then some for my own personal use.  

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
