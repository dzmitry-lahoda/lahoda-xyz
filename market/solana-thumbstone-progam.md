# Overview

Purpose of this program is allow transaction to fail if thumbstone already exists.

## Example

You transfers SPL tokens to account. 
But transactions fails with unknown state.
Next time you send new transaction with same amount and making double pay.
If to prefix transaction with thumbstone instruction, than transaction will fail before transfer if thumbstone detected.

## Instructions

### CreateThumbstone

Creates emtpy thumbstone account.

Account address is PDA derived from arbitrary 32 bit byte stream and from account.

Amount of SOL passed to account should be enough to it exists for period of time passed as input.
Examples, enough seconds to fit 24 hours.

If account exists, transaction fails with wellknown error code.

## CreateEvent

Could have root Event account. Make it an owner. Any thumbstone is derived from that event account too.
Owner of event account delete thumbstone anytime.

## CreateEventThumbstone

Thumbsonte aligned with event

## DeleteEventThumbstone

## Implementation

- Rust instructions documentation
- Positive end to end test
- TS SDK
- Rust CLI exampl
- Program is deployed on main net
- Code is documented and deploy process too
- Program is deployed via DAO and build verifiably via Anchor

## Price

Will 500 SOL for this program.
