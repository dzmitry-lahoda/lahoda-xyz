# Overview

Flow on Syntetix.

Stake and reward token is different.

Stake token does not inflates with time.

So reward tokend does.

## Stake

```python
total_supply += amount
balance[sender] += amount
staking_token[contract] += amount
staking_token[sender] -= amount
```

`Stake` invokes `Reward`, so rewards are continuos.


## Reward

Reward balances are accounted by contract and there is `reward token withdraw.

```python
rewards[sender] += balance[sender]*(reward_per_token-rewarded[sender])
rewarded[sender] = reward_per_token
```

User can continuously claim and withdraw balance and reward.

User can claim reward for period immediately, but adding and removing will not increase hist reward for period.

## Withdraw

```python
total_supply -= amount
balance[sender] -= amount
staking_token[contract] -= amount
staking_token[sender] += amount
```


## Duration

- Reward period is same for all users.
- Reward period is 7 days by default, can be updated
- Reward period cannot be updated until previous period finished.
- Rewards are brought by external system.
- Each reward invocation finishes period and starts new period.
- Each time reward is brought, it takes remaining reward + new rewards, and rates it along new period.
