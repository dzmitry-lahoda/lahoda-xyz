
## Chapter 1

Did Pracice questions 1.11-1.13

$$
\\ derivatives = \{ swap,\ option,\ forward contract,\  }
$$


Derivative requires two parties agree on future transaction.

Parties either agree bilateraly, or after agreement transaction are enforced by 3rd party clearing house.

Restructuring allow to compress underlying principal with retaining same position semantically.

Underlying principal is value controlled by position,
while transaction value is less.

### Forward contract:

- has one long party and one short party.
- is obligation for both parties
 
Neutralizes risk by fixing price.

Long is agreed to buy, and long agreed to sell.
Longs and shorts derivative, not underlying.

Future contract is standard forward.

If more investors want to go long price up, if go short price is down.

$$
\\ {forward contract} = \text{agreement to buy or sell assets at certain date at certain price}
\\ K = \text{future price}
\\ S_k=\text{strike price}
\\ {payoff}_{long} = S_k - K
\\ {payoff}_{short} = K - S_k 
$$


### Options

Call to buy at certain price at certain date.
Put to sell at certain price at certain date.

Participants:

| party  | calls                            | puts                             |
|--------|----------------------------------|----------------------------------|
| seller | profit from underlying goes down | profits from underlying goes up  |
| bayer  | profits from underlying goes up  | profit from underlying goes down |

Insurance to neutralize risk and possibly get profit.


$$
\\ payoff_{call option seller}=min(0, K - S_t)=-max(S_t-K,0)
\\ payoff_{call option buyer}=max(0, K - S_t)
$$


### Comparison

| contract | payment | leverage | loss                        |
|----------|---------|----------|-----------------------------|
| options  | upfront | yes      | buy limited, sell unlimited |
| forwards | no      | yes      | both unlimited              |


### Traders

- Hedgers avoid exposure from adverse movements
- Speculators
- Arbitrageurs

## Closing positions

Means entering opposite trade to the original one.



Initial margin - deposit on contact.

Variation margin - variation of user margin.

Maintaince margin lower than Initial Margin.


Neutralizing contract - seeling contract  if variona margin not covered by owenr of positon to be on top of maintaince margin.