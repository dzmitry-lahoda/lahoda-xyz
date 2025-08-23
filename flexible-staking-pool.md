# Overview 


https://docs.google.com/document/d/1WYwuopHodYDtWkgjPQc8fBKvfRKsMj2bk3V_RvKSPuc/edit

General idea there will be 2 SPL tokens account. One account is fully owned by pool and other one pool relies on vesting from.

Owned pool influences prices of xToken, while other pool not.

## Definition of done

- All relevant documentation from this file moved as Rust docs comments into code (specifically low level instructions and states)
- Positive scenario of each functions was autotested
- Clap based CLI to setup pool is provided

## Design decision

We could implement vesting into pool inside this program or using existing vesting program.

Unfortunately, Bonfida Vesting is real vesting - it locks amount. And we decided that pool will be under full control.

SteamFlow Timelock is complicated, and has cancellation. Overall can be used. 

I would mention Vesting as if we used external vesting or custom implementation. We can decide if we can do so. Initially vesting can be just account with some frequency of emission and size of reward.

## Instructions

### InitializeVesting

It can be either SteamFlow Timelock or custom vesting. Here we discuss custom vesting.
  
Pool can transfer amounts from SPL token to main pool as it is allowed.

### ConfigureVesting

- new frequency
- new reward size
- new delegated account

### InitializePool

Creates, configures new pool and relevant accounts

#### Input

- xtoken mint, PDA from pool
- token mint
- token account, PDA from pool
- Vesting schedule account
- owner

#### Process

- stores owner and mints into state
- validates token mint - initialized, rent exempt
- creates xtoken_mint owned by authority derived from pool
- validates vesting schedule to be owned by owner
- creates pool SPL account for token_mint owned by pool authority


### Stake

- Crunks vesting.
- Adds token to pool, dilluting xtokens, mints new xtokens

When user stakes tokens, user is minted with xTokens equal to:
```python
# x = lp token
xtoken_amount = new_base_amount * (xtokens_total_supply / total_base_amount_in_pool)
```
Example,

Stake 100 token and get 100 xTokens as first staker.
Pool will be rewarded with 100 base tokens, it will make 100 xTokens to cost 200 base tokens.
Second staker will add 100 base token,  so will get `100 base * (100 x tokens total) / 200 base total`  = 50 xTokens. 
Total 150 xTokens supply.

### Crunks vesting.

Gives 1/1_000_000(configurable) of reward to crunker. Transfer reward from vesting to pool.

### Stop

Owner stops any operation of pool.

### Start

Crunks vesting.
Owner starts pool after it was stopped

### UnStake 

- Crunks vesting.
- Transfers tokens to requester depending on tokens provided. Burns xtokens provided.

 
When user unstakes xTokens, user receives the number of tokens equal to: 
```
token_amount = amount * (token_amount_in_pool / xtokens_total_supply)
xtokens_total_supply = xtokens_total_supply - amount
```

Starting from stake examples.
User unstakes 100 xTokens.
100 * (300 / 150) = 200. So remains 50 xTokens total supply and 100 tokens in pool.



### Configure

- update owner if was passed
- updates vesting schedule if was passed. Crunk previous vesting schedule. if it fails - log expected amount to be vested.

## States

Describes states of program.

### Pool

- version : Version enum with V1 = 1
- xtoken_mint : Pubkey
- token_mint : Pubkey
- vault : Pubkey of SPL token account for token_mint 
- owner : Pubkey
- vesting : Pubkey
- status : Status - NotActive is default

### Vesting

- Frequency of vesting of reward.
- Size of reward
- SPL token account with delegate of pool authority.
- Last vesting timestamp.
- Owner to be pool authority
- Mint of vesting