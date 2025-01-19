

- stable coin is used as counter asset
- base asset give in some well know quantity of counter assets (converted into it) on position creation
- so base asset is used to fund initial margin and calcualte PnL


how it works:

1. by N of contracts at price X.
2. if base token worth Y, than you "bougth" Y / (N * X) of that token
3. if token grown by 10%
4. to close position you buy back of N * X contracts
5. and sell "base" amounts to N * X / 1.1Y
6. so profit was base_at_entry - base_at_exit

PnL = (1/ futures_entry_price - 1 / futures_exit_price) * N * Y,

PnL = position_size * direction * (mark_price - entry_price)

ROE = PnL / margin_entry = PnL / (position_amont * contract_multiplier * mark_price * IMR)

IMR = 1/ LEVERAGE

DIRCTION = 1(long) or -1(short)
