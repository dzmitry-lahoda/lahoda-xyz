

Initial margin fraction to open position is leverage multiplier. 

Maintenance margin triggers liquidation.

Each perpetual has its own tick size, minimum order size.

Perpetual price to track underlying. 
So if price of perpetual above index price, long pay shorts via funding rate every hour.


Example,

I have 1000 USDC collateral I have sold(shorted) 1 ETH for 2000 USD. 
So I owe protocol 1 ETH. And have 3000 USDC.

`Initial margin = (1000+2000 - 1*2000) / 2000`.


So if price of 1 ETH become 2800, than now `maintenance fraction = (1000 + 2000 - 1 * 2800) / 1 * 2800`.  

Both margins compared to `required` fractions.