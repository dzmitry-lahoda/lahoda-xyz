#!/usr/bin/env python3
"""
MARKET model with simple numerical examples.
Target audience are auditors to get onto what to expect,
people who just want quickly play with MARKET numerics and plots.

In memory Python mode usually 50X less lines than Rust to read out behavior
(Rust is complicated with error handling, deep stack, overflows, serde/persist, types).
"""

from dataclasses import dataclass
from decimal import Decimal as price, Decimal as fraction, Decimal as size
from builtins import str as account_id
from numbers import Number as timestamp
from enum import Enum

## Market

### Parameters


# as in current master code
def settle(
    symbol,
    base_decimals,
    quote_decimals,
    market_size_decimals,
    market_price_decimals,
    price_mantissa,
    intent_size_mantissa,
):
    """
    If user wants to exchange 2 ETH next assert will be true `assert(intent_size_mantissa == 2*10**market_size_decimals)`
    """
    print(
        "\nbase_decimals, quote_decimals, market_size_decimals, market_price_decimals, price_mantissa, intent_size_mantissa"
    )
    print(
        base_decimals,
        quote_decimals,
        market_size_decimals,
        market_price_decimals,
        price_mantissa,
        intent_size_mantissa,
    )

    long_user_base_delta = intent_size_mantissa * 10**base_decimals / 10**market_size_decimals
    print(symbol, "long user gets on base balance =", long_user_base_delta)
    print(
        symbol, "long user gets ", symbol, "=", long_user_base_delta / 10**base_decimals
    )

    short_user_quote_delta = (
        intent_size_mantissa
        * price_mantissa
        * 10**quote_decimals
        / 10**market_size_decimals
        / 10**market_price_decimals
    )
    print(symbol, "short user gets on quote balance =", short_user_quote_delta)
    print(
        symbol, "short user gets in USDC =", short_user_quote_delta / 10**quote_decimals
    )


# example of decimal parameters with variants of pricing
def decimal_parameters_simulation():
    USDC_CONTRACT_DECIMALS = 6
    USDC_QUOTE_DECIMALS = 6

    assert USDC_CONTRACT_DECIMALS == 6, "cannot change without changing settlement"
    assert USDC_QUOTE_DECIMALS <= USDC_CONTRACT_DECIMALS

    print("======================= ETH =========================")

    ETH_CONTRACT_DECIMALS = 18
    ETH_BASE_DECIMALS = 10
    assert ETH_BASE_DECIMALS <= ETH_CONTRACT_DECIMALS
    ETH_USDC_MARKET_SIZE_DECIMALS = 5
    ETH_USDC_MARKET_PRICE_DECIMALS = 3
    eth_usdc_price_mantissa = 2_500_000  # 2500 is "real" USD price

    settle(
        "ETH",
        ETH_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        ETH_USDC_MARKET_SIZE_DECIMALS,
        ETH_USDC_MARKET_PRICE_DECIMALS,
        eth_usdc_price_mantissa,
        1,
    )

    settle(
        "ONE ETH pet TS SDK",
        ETH_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        ETH_USDC_MARKET_SIZE_DECIMALS,
        ETH_USDC_MARKET_PRICE_DECIMALS,
        eth_usdc_price_mantissa,
        1 * 10**ETH_USDC_MARKET_SIZE_DECIMALS,
    )

    print("======================= SHIB =========================")
    SHIB_CONTRACT_DECIMALS = 18
    assert SHIB_CONTRACT_DECIMALS == 18, "cannot change without changing settlement"

    SHIB_BASE_DECIMALS = 7
    assert SHIB_BASE_DECIMALS <= SHIB_CONTRACT_DECIMALS

    SHIB_USDC_MARKET_SIZE_DECIMALS = 10
    SHIB_USDC_MARKET_PRICE_DECIMALS = 6
    shib_usdc_price_mantissa = 10  # 0.00001 is "real" USD price

    settle(
        "SHIB",
        SHIB_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        SHIB_USDC_MARKET_SIZE_DECIMALS,
        SHIB_USDC_MARKET_PRICE_DECIMALS,
        shib_usdc_price_mantissa,
        1,
    )

    settle(
        "SHIB per TS SDK",
        SHIB_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        SHIB_USDC_MARKET_SIZE_DECIMALS,
        SHIB_USDC_MARKET_PRICE_DECIMALS,
        shib_usdc_price_mantissa,
        1 * 10**SHIB_USDC_MARKET_SIZE_DECIMALS,
    )

    settle(
        "SHIB per (quote = 0)",
        SHIB_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        SHIB_USDC_MARKET_SIZE_DECIMALS,
        SHIB_USDC_MARKET_PRICE_DECIMALS,
        shib_usdc_price_mantissa,
        1 * 10 ** (SHIB_BASE_DECIMALS + 1),
    )

    print("======================= USDT =========================")

    USDT_CONTRACT_DECIMALS = 6
    assert USDT_CONTRACT_DECIMALS == 6, "cannot change without changing settlement"

    USDT_BASE_DECIMALS = 6
    assert USDT_BASE_DECIMALS <= USDC_CONTRACT_DECIMALS
    USDT_USDC_MARKET_SIZE_DECIMALS = 9
    USDT_USDC_MARKET_PRICE_DECIMALS = 12
    usdt_usdc_price_mantissa = (
        10**USDT_USDC_MARKET_PRICE_DECIMALS
    )  # 1 USD is "real price"

    settle(
        "USDT per TS  SDK",
        USDT_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        USDT_USDC_MARKET_SIZE_DECIMALS,
        USDT_USDC_MARKET_PRICE_DECIMALS,
        usdt_usdc_price_mantissa,
        1 * 10**USDT_USDC_MARKET_SIZE_DECIMALS,
    )

    settle(
        "USDT per TS  SDK (half price)",
        USDT_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        USDT_USDC_MARKET_SIZE_DECIMALS,
        USDT_USDC_MARKET_PRICE_DECIMALS,
        usdt_usdc_price_mantissa / 2,
        1 * 10**USDT_USDC_MARKET_SIZE_DECIMALS,
    )

    settle(
        "USDT per TS  SDK (double price)",
        USDT_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        USDT_USDC_MARKET_SIZE_DECIMALS,
        USDT_USDC_MARKET_PRICE_DECIMALS,
        usdt_usdc_price_mantissa * 2,
        1 * 10**USDT_USDC_MARKET_SIZE_DECIMALS,
    )

    settle(
        "USDT",
        USDT_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        USDT_USDC_MARKET_SIZE_DECIMALS,
        USDT_USDC_MARKET_PRICE_DECIMALS,
        usdt_usdc_price_mantissa,
        1,
    )

    settle(
        "USDT (half price)",
        USDT_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        USDT_USDC_MARKET_SIZE_DECIMALS,
        USDT_USDC_MARKET_PRICE_DECIMALS,
        usdt_usdc_price_mantissa / 2,
        1,
    )

    settle(
        "USDT (double price)",
        USDT_BASE_DECIMALS,
        USDC_QUOTE_DECIMALS,
        USDT_USDC_MARKET_SIZE_DECIMALS,
        USDT_USDC_MARKET_PRICE_DECIMALS,
        usdt_usdc_price_mantissa * 2,
        1,
    )


## Margining

#### account value


def position_av(*, position_size, market_index_price, position_open_price):
    return position_size * (market_index_price - position_open_price)


# calculates user's account value on perp markets
def user_markets_av(positions):
    sum(position_av(**p) for p in positions if p.market_type == "perp")


# TV(token value)
def token_av(*, balance, token_weight, token_index_price):
    weight = token_weight if balance >= 0 else 1
    return balance * token_index_price * weight


# calculates user's account value of balance tokens
def user_tokens_av(tokens):
    sum(token_av(**t) for t in tokens)


# calculates user's account value of all positions and tokens
def user_av(positions, tokens):
    return user_markets_av(positions) + user_tokens_av(tokens)


## Margin fractions

#### OMF(Open Margin Fraction)


def user_omf_numerator(*, user_av_value, user_col_value, user_unsettled_funding):
    return min(user_av_value, user_col_value + user_unsettled_funding)


# calculates user's open margin fraction
def user_omf(*, user_av_value, user_col_value, user_unsettled_funding, user_pon_value):
    return (
        user_omf_numerator(
            user_av_value=user_av_value,
            user_col_value=user_col_value,
            user_unsettled_funding=user_unsettled_funding,
        )
        / user_pon_value
    )


# summarizes total user unsettled funding across perp positions
def user_unsettled_funding(positions):
    sum(p.unsettled_funding for p in positions if p.market_type == "perp")


def user_col(tokens):
    return sum(max(0, t.balance) for t in tokens)


#### initial margin fraction


def user_token_imf_weighted_pon_numerator(user_tokens):
    return sum(token_position_open_notional(**t) * t.token_imf for t in user_tokens)


def user_market_imf_weighted_pon_numerator(user_positions):
    return sum(
        position_open_notional(**p) * p.market_imf
        for p in user_positions
        if p.market_type == "perp"
    )


# calculates user's initial margin fraction numerator
def user_imf_numerator(*, user_tokens, user_positions):
    return user_token_imf_weighted_pon_numerator(
        user_tokens
    ) + user_market_imf_weighted_pon_numerator(user_positions)


# IMF(initial margin fraction)
def user_imf(*, user_tokens, user_positions, user_pon_value):
    return (
        user_imf_numerator(user_tokens=user_tokens, user_positions=user_positions)
        / user_pon_value
    )


# if true than user can open a position
def can_open_orders(*, user_omf_num, user_imf_num):
    return user_omf_num >= user_imf_num


#### position notional and position open notional


# calculates unrealized P&L
def unrealized_pnl(*, position_size, open_price, index_price):
    return position_size * (index_price - open_price)


# calculates PN
def position_notional(position_size, index_price, market_type):
    return abs(position_size) * index_price if market_type == "perp" else 0


# summarizes all user's positions on perp markets
def position_open_size(position_size, position_all_asks, position_all_bid):
    # where `position_all_asks` and `position_all_bid` are the sum of sizes
    return position_size + max(position_all_asks, position_all_bid)


# calculates user's position notional over perp markets
def position_open_notional(
    *,
    position_size,
    market_type,
    position_all_asks,
    position_all_bid,
):
    return (
        position_open_size(position_size, position_all_asks, position_all_bid)
        if market_type == "perp"
        else 0
    )


def token_position_notional(token_balance, index_price_token):
    return +(token_balance <= 0) * -token_balance * index_price_token


token_position_open_notional = token_position_notional


# calculates user's position notional over tokens and perp markets
def user_position_notional(positions, user_tokens):
    positions = sum(position_notional(**p) for p in positions)
    tokens = sum(token_position_notional(**t) for t in user_tokens)
    return positions + tokens


# calculates user's position open notional over tokens and perp markets
def user_position_open_notional(positions, user_tokens):
    positions = sum(position_open_notional(**p) for p in positions)
    tokens = sum(token_position_open_notional(**t) for t in user_tokens)
    return positions + tokens


## Markets

### Perpetual futures


## calculates mark price `p_mark` for perp markets
## in case there are ask and bids, it is just midpoint price
def mark_price(
    *,
    last_mark_price: price,
    best_bid_price: price,
    best_ask_price: price,
    one_sided_multiplier: fraction,
    asks,
    bids,
):
    """_summary_
    `last_mark_price` - previous of perp
    `asks`, `bids` - on perp
    `best_ask_price`, `best_bid_price` - from underlying perp market

    returns `marking-to-market` price
    """
    assert 0 < one_sided_multiplier < 1
    if not asks and not bids:
        return last_mark_price
    if asks and bids:
        return (best_ask_price + best_bid_price) / 2
    if asks:
        return best_bid_price * (1 - one_sided_multiplier)
    else:
        return best_ask_price * (1 + one_sided_multiplier)


def spot_funding_rate(*, p_mark: price, p_index: price):
    """
    `p_index` - aggregated spot prices
    """
    return p_mark / p_index - 1


# calculates time weighted funding rate
def timed_funding_rate(
    *, previous: timestamp, now: timestamp, p_mark: price, p_index: price
):
    assert now > previous
    spot_rate = spot_funding_rate(p_mark=p_mark, p_index=p_index)
    delta_time = now - previous
    return (delta_time, spot_rate * delta_time)


def update_cumulative_funding_rate(
    *, cumulative_time, cumulative_rate, delta_time, delta_rate
):
    return (cumulative_time + delta_time, cumulative_rate + delta_rate)


# time weighted averaged funding rate
def twap_funding_rate(
    *,
    cumulative_start_time,
    cumulative_start_rate,
    cumulative_end_time,
    cumulative_end_rate,
):
    return (cumulative_end_rate - cumulative_start_rate) / (
        cumulative_end_time - cumulative_start_time
    )


def funding_payment(*, position_size: size, twap_funding_rate, p_index: price):
    """
    negative means position must be reduced,
    positive means position must be increased
    """
    return position_size * twap_funding_rate * p_index


class Position:
    open_price: price
    current_funding_rate: fraction
    # positive for long, negative for short
    size: size


class MarketType(Enum):
    PERP = "perp"
    SPOT = "spot"


class Order:
    account_id: str
    # only exact price is allowed
    limit: price
    # positive for buy, negative for sell
    size: size


# Maximally simple book for shortest and clear code possible because:
# - it is slow and not scalable
# - limit only fill single match fill
# - no partial fills
# - no edge case handling
# - no ticks, entity decimals, rounding
class Book:
    orders: list[Order] = []
    positions: map[account_id, Position] = {}
    # negative balance is borrow
    balances: map[account_id, size] = {}

    #### Fills
    def fill(self, account_id: str, size: size, open_price: price, fill_price: price):
        position = self.positions[account_id]
        if position * size < 0:
            self.balances[account_id] -= size * (open_price - fill_price)
        else:
            position.open_price = (
                position.open_price * position.size + price * size
            ) / (position.size + size)
            position.size += size

    def execute(self):
        buys = iter(
            sorted(
                [o for o in self.orders if o.size > 0],
                key=lambda o: (o.limit, o.size),
                reverse=True,
            )
        )
        sells = iter(
            sorted(
                [o for o in self.orders if o.size < 0],
                key=lambda o: (o.limit, -o.size),
                reverse=False,
            )
        )
        buy = next(buys, None)
        sell = next(sells, None)
        while buy and sell and buy.limit >= sell.limit:
            mid_price = (buy.limit + sell.limit) / 2
            if buy.size == -sell.size:
                self.orders.remove(buy)
                self.orders.remove(sell)
                if not self.positions[buy.account_id]:
                    self.positions[buy.account_id] = Position(
                        open_price=mid_price, open_price_funding_rate=0, size=0
                    )
                if not self.positions[sell.account_id]:
                    self.positions[sell.account_id] = Position(
                        open_price=mid_price, open_price_funding_rate=0, size=0
                    )
                self.fill(buy.account_id, sell.size, mid_price)
                self.fill(sell.account_id, buy.size, mid_price)
            elif buy.size > -sell.size:
                buy = next(buys)
            else:
                sell = next(sells)


if __name__ == "__main__":
    # simulate TWAP of FR
    observations = [
        timed_funding_rate(previous=0, now=1, p_mark=price(0.8), p_index=price(1)),
        timed_funding_rate(previous=1, now=2, p_mark=price(0.8), p_index=price(1)),
        timed_funding_rate(previous=2, now=3, p_mark=price(1), p_index=price(1)),
    ]
    cumulative_time, cumulative_rate = 0, 0
    for time, rate in observations:
        cumulative_time, cumulative_rate = update_cumulative_funding_rate(
            cumulative_time=cumulative_time,
            cumulative_rate=cumulative_rate,
            delta_time=time,
            delta_rate=rate,
        )

    # twap for 3 times periods
    twap_3 = twap_funding_rate(
        cumulative_start_time=observations[0][0],
        cumulative_start_rate=observations[0][1],
        cumulative_end_time=cumulative_time,
        cumulative_end_rate=cumulative_rate,
    )
    assert -0.1 < twap_3 < -0.0998


def test_funding_rate():
    # mark price above index
    assert spot_funding_rate(p_mark=1.1, p_index=1) == -0.111111


def test_pnl_perp():
    book = Book()
    buy = Order(user="alice", limit=1.0, size=100)
    sell = Order(user="bob", limit=1.0, size=-100)
    book.orders.append(buy)
    book.orders.append(sell)
    book.execute()

# see https://docs.pyth.network/price-feeds/best-practices
@dataclass
class Price:
    price_mantissa: int
    decimals: int
    confidence: int
    
    # constuctor that checks that confidence is less than price mantissa
    def __post_init__(self):
        assert abs(self.confidence) < abs(self.price_mantissa)