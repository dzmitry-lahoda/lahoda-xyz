# Abstract

Cross-chain Virtual Machine (XCVM) is a specification outlining an application-level messaging protocol between state machines and other execution environments. It allows for a more sophisticated mechanism for cross-chain communication compared to message passing, by defining an interpreter-based communication interface between chains.

- Turing-Complete Interactions: Complicated business logic can be dynamically dispatched to other chains, without the need for developers to deploy contracts on the destination chain.

# 1. Overview

## 1.1. Document Structure


* XCVM is a DTCC-like protocol for blockchains.

    - Section 2.1. describes on-chain versioning.

    - Section 2.2. describes the instruction set.

    - Section 2.3. describes general asset amount handling.

    - Section 2.4. specifies the abstract virtual machine.

    - Section 2.5. outlines the execution semantics of a program.

* Encoding programs is primarily done using protobufs.

* Fees are charged at different stages, for bridging and execution.

* Asset Registries provide ways to deal with ERC20, CW20, or native assets across chains.

* Further work outlines planned extensions to the specification.

    - Section 6.1. elaborates on NFTs.

    - Section 6.2. provides a model for abstracting ownership and identities.

* Security considerations to be made by users and implementors.

## 1.2. Terms and Definitions

The keywords "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED", "MAY", and "OPTIONAL" in this document are to be interpreted as described in [RFC 2119](https://www.rfc-editor.org/rfc/rfc2119).

Commonly used terms in this document are described below.

`Transaction`: A (reversible) operation on a chain.

`Transfer`: Changing the ownership of an asset (token, NFT, etc) from one account to another.

`Identity`: An entity that has an address. Note that identities may not have a public/private key, as they can be contracts.

`Cross Chain Transfer`: The bridging of funds between two chains.

`Cross Chain Transaction`: Two or more transactions on two or more chains. Note that a cross-chain transaction is not `transactional`.

`XCVM Transaction`: A cross-chain transaction defined as XCVM instructions, being handled by interpreters. Technically an XCVM transaction can be single chain only, although the use case for that seems non-existent.

`Message Passing`: Sending bytes from one chain to another.

`Event`: A message emitted by a contract/module/pallet during transaction execution.

`XCVM Event`: An event emitted by part of the XCVM contracts.

`Beneficiary`: Recipient of assets.

`Relayer`: Initiator of the destination side transaction, paying for the execution fees.

`Tip`: Tip address for execution. 

`Opaque Contract`: Any smart contract, module, or pallet.

`Chain`: A blockchain with its consensus and execution environment, but may also refer to rollups.

`User`: A third party user of XCVM contracts. Can be another contract, module, pallet or actual human.

`Implementor`: An entity implementing technology according to the XCVM specification.

# 2. XCVM

The `XCVM` refers to both a set of on-chain contracts, orchestrating the bridging operations, ownership, and execution, as well as the interchain system of bridges and relayers. This document mainly specifies the logic within a single chain, and how implementors MUST execute messages and maintain state.

Although execution environments change depending on the chain, the `XCVM` protocol is generic over the differences and provides an abstract target for smart contracts to interact with. We describe components as separate contracts, but implementors MAY be a pallet, Cosmos SDK module, or a single contract as opposed to many. Here the choice is based made on gas optimizations, engineering standards, and security practices.

`XCVM` is bridge agnostic, as long as the underlying bridging protocol is capable of generic message passing. Developers can opt-in to their usages for each interpreter instance. We highly recommend `IBC` if available, and by default only allow communication across trustless bridges.

```mermaid
sequenceDiagram
    participant IBC
    participant Gateway
    participant Router
    participant Interpreter
    IBC->>Gateway: Pass program.
    Gateway->>Router: Add bridging info and transfer funds.
    Router->>Interpreter: Instantiate VM and transfer funds.
    loop Instructions
        Interpreter-->Interpreter: Interact with contracts.
    end
    Interpreter-->>Gateway: Send program.
    Gateway->>IBC: Route through IBC.
```

Interpreter may be also be singleton instance of contract per chain. 

Cross chain(XC) account contract is instantiated for each user which hold funds and proxies calls in this case.

```mermaid
sequenceDiagram
    Router->>Interpreter: Instantiate VM
    Router->>XcAccount: Transfer funds
    loop Instructions
        Interpreter-->XcAccount: Interact with contracts.
    end
    Interpreter-->>Gateway: Send program.
    Gateway->>IBC: Route through IBC.
```


## 2.1. Versioning

`XCVM` protocol versions and implementations use [semantic versioning](https://semver.org/spec/v2.0.0.html) to identify capabilities and backward compatibility.

## 2.2. Instruction Set

Messages executed by the `XCVM` follow the `Program` format.

```typescript
interface Program {
    tag : Tag
    instructions: Instruction[]
}
type Tag = Uint8Array

type Instruction = Transfer | Call | Spawn | Query | Exchange
```

Each instruction is executed by the on-chain interpreter in sequence. The execution semantics are defined in section 2.4.5.

The following sequence shows possible high-level implementations for each instruction.

```mermaid
sequenceDiagram
    Interpreter->>ERC20 or CW20 or Native: Transfer
    Interpreter->>XcAccount: Proxy Call
    XcAccount->>Opaque Contract: Raw Call
    Interpreter->>Gateway: Spawn
    Interpreter->>Gateway: Query
```

### 2.2.1. Transfer

Transfers funds within a chain between accounts.

```
<Transfer>     ::= <Account> <Assets> | <Tip> <Assets>

<Account>      ::= bytes
<Assets>       ::= { <AssetId> : <Balance> }
<AssetId>      ::= <GlobalId> | <LocalId>
<GlobalId>     ::= u128
<LocalId>      ::= bytes
<Balance>      ::= <Ratio> | <Absolute> | <Unit>
<Absolute>     ::= u128
<Unit>         ::= u128 Ratio
<Ratio>        ::= u128 u128
```

### 2.2.2. Call

Executes a payload within the execution context of the chain, such as an extrinsic or smart contract invocation. Call is guaranteed to execute on the specified `Network` of the `Spawn` context.

```
<Call>         ::= <Payload> <Bindings>
<Payload>      ::= bytes
<Bindings>     ::= [ u16 <BindingValue> ]
<AssetAmount>  ::= <AssetId> <Balance>
<BindingValue> ::= <Self> | <Tip> | <Result> | <AssetAmount> | <GlobalId>
```

### 2.2.2.1. Late Bindings

The call instruction supports bindings values on the executing side of the program by specifying the `Bindings`. This allows us to construct a program that uses data only available on the executing side. For example, the swap call of the following smart contract snippet expects a `to` address to receive the funds after a trade.

```rust
fn swap(amount: u256, pair: (u128, u128), to: AccountId) { ... }
```

If the caller wants to swap funds from the interpreter account and receive the funds into the interpreter account, we need to specify the BindingValue `Self`, using the index of the `to` field for the serialized data being passed to the smart contract.

On the executing instance, `BindingValue::Self` will be interpolated at byte index 13  of the payload before being executed, the final payload then becomes `swap(10,(1,2), BindingValue::Self)`, where `BindingValue::Self` is the canonical address of the interpreter on the destination side.

Besides accessing the `Self` register, `BindingValue` allows for lazy lookups of `AssetId` conversions, by using `BindingValue::AssetId(GlobalId)`, or lazily converting `Ratio` to absolute `Balance` type.

Indices in bindings must to be **sorted** in an ascending order and **unique**.

Bindings do not support non-byte aligned encodings.

### 2.2.3. Spawn

Sends a `Program` to another chain to be executed asynchronously. It is only guaranteed to execute on the specified `Network` if its `Program` contains an instruction that is guaranteed to execute on the `Network` of the `Spawn` context.

```
<Network>     ::= u128
<Salt>        ::= bytes
<OriginNonce> ::= bytes

<Spawn>      ::= <Network> <Program> <Salt> <Assets> <Nonce>
```

Where the **salt** is used by the Router while instantiating the interpreter (see section 2.5.2.).

`OriginNonce` is unique number generated once per program execution on originating consensus. Allows unique identify program invocation from origin to all child spawns. Combined with `Network` and `Program` can be considered `cross chain transaction identifier`.

In case of escrow(reserver) transfer, `AssetId` in `Assets` are converted from as on sender to as on receiver network. 

### 2.2.3.1. IBC

Spawned program using IBC based bridges need to be wrapped into packet data before being sent to IBC bridges. Protobuf encoding and decoding is implemented in this case for both sending and receiving packages.
The packet data is defined as follows:

```
<UserOrigin>        ::= Account Network
<InterpreterOrigin> ::= Account

<SpawnPackage>      ::= <InterpreterOrigin> <UserOrigin> <Salt> <Program> <Assets>
```

Where the **interpreter** is used in when the IBC packet execution fail or timeout to return the locked funds.

`Assets` are fungible `ICS-20` assets.

### 2.2.3.1.1. Spawn send

The bridge MUST escrow the **assets** transferred.

Upon successful acknowledgement (see section 2.2.3.1.2.), the bridge MUST burn
the previously escrowed **assets**.

Upon failure acknowledgement (see section 2.2.3.1.2.) or timeout, the bridge
MUST unescrow and return the **assets** to the **interpreter** (using the
`InterpreterOrigin`).

### 2.2.3.1.2. Spawn receive

Upon reception of a `SpawnPackage`, the XCVM execution MUST happen on a
sub-transaction (the transaction MUST not fail even if the execution fails) and
an XCVM-specific acknowledgement must be committed for the packet:
- A single byte, `0x00` if unsuccessful
- A single byte, `0x01` if successful

The bridge MUST deposit the **assets** in the Router before executing the
XCVM program.

Note: Assuming we transfer the assets `[asset1 amount1, ..., assetN amountN]`,
the bridge MUST ensure that the sequence `[mint1, ... mintN, executeProgram]` is
atomically executed within a sub-transaction. If any error occur, the according
acknowledgement byte MUST be committed and the sub-transaction MUST be reverted.

### 2.2.4. Query

Queries register values of an `XCVM` instance across chains. It sets the current `Result Register` to `QueryResult`. See section 3. on the semantics of registers and `RegisterValues`.

```
<Query>        ::= <Network> <Account>
<QueryResult>  ::= {<RegisterValues>}
```

### Exchange

If underlying state machine and configuration of state machine support `Exchange` it can be executed.

```typescript
interface Exchange {
    in: AssetAmount[]
    min_out: AssetAmount[]
}
```

`ResultRegister` is set after execution.

## 2.3. Balances

Amounts of assets can be specified using the `Balance` type. This allows foreign programs to specify sending a part of the total amount of funds using `Ratio`, or express the amounts in the canonical unit of the asset: `Unit`,  or if the caller knows amount of the assets on the destination side: `Absolute`.

## 2.4. Abstract Virtual Machine

Each `XCVM` instance is a bytecode interpreter with a limited set of specialized registers.

### 2.4.1 Registers

Each interpreter keeps track of persistent states during and across executions, which are stored in different registers. Register values are always updated during execution and can be observed by other contracts.

```
<RegisterValues> ::= {<RegisterValue>}
<RegisterValue>  ::= <ResultRegister> | <IPRegister> | <TipRegister> | <SelfRegister> | <VersionRegister>
```

#### 2.4.1.1 Result Register

The result register contains the result of the last executed instruction.

```
<ResultRegister> ::=
    <Error>
    | <ExecutionResult>

<Error ::=
    <CallError>
    | <TransferError>
    | <SpawnError>
    | <QueryError>

<ExecutionResult> ::=
    <Ok> | bytes
<Ok> ::= '0'

<CallError> ::= bytes
<TransferError> ::= bytes
<SpawnError> ::= bytes
<QueryError> ::= bytes
```

If `ResultRegister` was set to `Error` and there is `Restoration` register contains XCVM program it will be executed.

#### 2.4.1.2 IP Register

The instruction pointer register contains the instruction pointer of the last executed program and is updated during program execution. Querying for the `IP` and `Result` can be used to compute the state of the interpreter on another chain.

```
<IPRegister> ::= u32
```

#### 2.4.1.3 Tip Register

The Tip register contains the `Account` of the account triggering the initial execution. This can be the IBC relayer or any other entity. By definition, the tip is the account paying the fees for interpreter execution.

```
<TipRegister> ::= <Account>
```

#### 2.4.1.4 Self Register

The self register contains the `Account` of the interpreter. Most implementations will not need to use storage but have access to special keywords, such as `this` in Solidity.

```
<SelfRegister> ::= <Account>
```

#### 2.4.1.5 Version Register

The version register contains the semantic version of the contract code, which can be used to verify the subset of XCVM functionality supported by the contract. Implementations that support upgradable contracts MUST update the version register. Functionality advertised through the version register MUST be supported by the contract.

### 2.4.5 Program Execution Semantics

Execution of a program is a two-stage process. First, the virtual machine MUST verify that the caller is allowed to execute programs for that specific instance, by verifying that the caller is one of the owners. See section 2.6. for ownership semantics. Second, the TipRegister must be set. Third, the instructions are iterated over and executed. Implementors MUST execute each instruction in the provided order and MUST update the IP register after each instruction is executed. After each instruction is executed, the result register MUST be set to the return value of the instruction. The interpreter SHOULD NOT mangle the return values but store them as returned. Because the return values are chain specific, the actual structure is left *undefined*.

If an error is encountered by executing an instruction, the defined transactional behavior for that instruction should be abided by. All instructions defined in this document require the transaction to be aborted on failure, however, subsequent addendums may define new instructions with different behavior.

After the final instruction has been executed and registers are set, the execution stops and the transaction ends.

See Appendix A for the algorithm.

## 2.5. XCVM Execution Semantics

Each chain within the `XCVM` contains a singleton entity consisting of the Router, and the Gateway. Implementors MAY choose to create a monolithic smart contract or a set of modular contracts.

### 2.5.1. Gateway

Each chain contains a singleton bridge aggregator, the `Gateway`, which abstracts over transports.


Outgoing messages are routed based on bridge identifier, or by specifying the bridge contract directly.

Each XCVM execution has access to its message `MessageOrigin` and can be configured to deny execution depending on the address or security level:

```
<MessageOrigin> ::=
    <IBC>
    | <XCM>
    | <OTP>

<OTP> ::= <BridgeId>
<BridgeId> ::= bytes
```

The `Gateway` allows for third parties to add their bridges as well, using our open transport protocol (`OTP`), although this is a feature that we will only later make public. `OTP` provides the following functionality

- Registration of bridges.
- Deregistration.
- Pausing.

`OTP` will later be extended to handle more granular black/whitelisting of beneficiaries, assets, and message filters.

### 2.5.2. Router

Each program arriving through the `Gateway` is passed to the `Router`, which becomes the initial beneficiary of the provided `Assets` before finding or instantiating an `Interpreter` instance. The router then transfers funds to the `Interpreter` instance.

Subsequent calls by the same `Origin` will not result in an instantiation, but instead in re-use of the `Interpreter` instance. This allows foreign `Origins` to maintain state across different protocols, such as managing LP positions.

If no interpreter instance has been created for a given caller, the call to the `Router` must either come from the `IBC`, `XCM`, `OTP`, or a local origin. After the instance has been created, it can be configured to accept other origins by the caller.

**Example**

For a given XCVM program, its interpreter instance is derived from `Network Account Salt`. This allows users to create different interpreter instances to execute programs against. Note that the `Salt` is not additive and only the composite `Network Account` is forwarded to remote chains as the user origin:
```
Spawn A 0x01 [          // Parent program spawned on A, with 0x01 as salt, the origin for the instructions is (A, AccountOnA, 0x1)
    Call 0x1337,                                     // Call instruction executed on A
    Spawn B 0x02 [] {}, // Sub-program spawned on B, with 0x02 as salt, the origin for the instructions is (A, AccountOnA, 0x2)
] {}
```
Possible usage is to allow one program execution to act on state of other program execution to restore funds. 


In the above XCVM program, the parent program salt `0x01` is not a prefix of the sub-program salt `0x02`. The user is able to make it's interpreter origin using a fine grained mode. The following program is an example on how we can spread a salt:
```
Spawn A 0x01 [             // Parent program spawned on A, with 0x01 as salt, the origin for the instructions is (A, AccountOnA, 0x01)
    Call 0x1337,                                        // Call instruction executed on A
    Spawn B 0x0102 [] {}, // Sub-program spawned on B, with 0x0102 as salt, the origin for the instructions is (A, AccountOnA, 0x0102)
] {}
```

In next program, all spawned instances on all chains share state (including assets):
```
Spawn A 0x01 [
    Call 0x1337,
    Spawn B 0x01 [] {}, // Sub-program spawned on B, with 0x01 as salt, the origin for the instructions is (A, AccountOnA, 0x01) allows to share 
] {}
```

### 2.6. Ownership

interpreter instances maintain a set of owners.

```
<Owners> ::= {<Identity>}
<Identity> ::= <Network> <Account>
```

Programs are only executed by the interpreter if the caller is in the set of owners.

On initial instantiation of the `XCVM` interpreter, the calling `Identity` is the owner. This can be a local or foreign account, depending on the origin. The owning `Identity` has total control of the interpreter instance and the funds held and can make delegate calls from the instance's account.

Oftentimes, multiple `Identities` represent a single real-world entity, such as a cross-chain protocol or a user. To accommodate for shared/global ownership of resources, each interpreter keeps track of a set of `Identities`, which share ownership of the interpreter. Each owning `Identity` has full permissions on the interpreter instance.

Owners may be added by having the interpreter call the appropriate setters. We will consider adding specialized instructions later. Owners may be removed by other owners. An XCVM instance MUST always have at least one owner.

# 3. Encoding

Different chains may choose to accept different encodings as the main entry point for contract calls. Such encodings can include but are not limited to `scale`, `ethabi`, `bors`, `borsh`. Chain-to-chain calls are always in a single encoding: `protobuf`, which is used within the transport.

`protobuf` is generally [not deterministic](https://protobuf.dev/programming-guides/encoding/). XCVM restricts encoders and decoders to a [deterministic subset of protobuf](https://docs.cosmos.network/main/architecture/adr-027-deterministic-protobuf-serialization).

## 3.1. JSON Encoding

The current prototyping of `XCVM` uses JSON encoding, although users SHOULD not rely on this feature.

# 4. Fees

There are three different components to the fees charged for interacting with the `XCVM`:

1. Gas fees on the origin chain, are used to pay for local submission and partial execution.
2. Bridging fees (optional): Some bridges charge a dynamic fee based on the number of assets sent. If possible, fees are folded into 1., otherwise charged during transmission.
3. Execution fees (optional): A reward added by the instruction author to reward the relayer for paying for the execution on the destination chain.

## 4.1. Execution Fees

Gas and Bridging fees are handled during the invocation and at the `Router` level, however, Execution fees are opt-in and paid by the user by using the `Tip` registry value. The following example program performs an operation, and rewards the tip address:

```
<Call> 0x13371337...
<Transfer> <Tip> { USDC: 15000000000000 }
```

This model is very much like Bitcoin's UTXOs, where the difference between inputs and outputs defines the tip. Here we are more explicit with the actual fee, which allows for more fine-grained control. Together with branching (to be implemented later), this fee model can be used to incentivize the relayer to precompute the outcome, and only submit the program if it were to succeed at the current state of the destination chain.

# 5. Asset Registries

Assets can be identified using a global asset identifier.

```
<AssetId> ::= u128
```

Each chain contains data which maps assets to their local representations, such as erc20 addresses. The `Transfer` instruction uses this registry to look up the correct identifiers. Interpreter instances can be reconfigured by the owner to use alternative registries.

Propagating updates across registries is handled by the `XCVM` too. We will go more in-depth on how we bootstrap this system in a later specification.

# 6. Further Work

## 6.1. NFTs

The design specification currently does not take NFTs into account. We have chosen to not (yet) specify NFTs as part of `Assets` due to the complexity of owning and value accruing NFTs. We do however intend to update the specification once the approach has been finalized.

## 6.2. Name Service

The `CNS` provides an abstraction on top of the `Identity` system, allowing developers and users to use a single name across interpreter instances. Each `XCVM` chain contains a `CNS` registry, which maps `Identity` to `Name`. On bridge relays, the calling program can specify to use an associated `Name` instead of its `Identity`. The `XCVM` interpreter has to be configured to accept the `CNS` as an owner.

```
<Name> ::= bytes
```

We will later elaborate on using alternative name registries such as [`ENS`](https://ens.domains/).

# 7. Security Considerations

Ensuring that the caller is an owner is an incredibly important check, as the owner can delegate calls through the interpreter, directly owning all state, funds, and possible (financial) positions associated with the interpreter account. Since each interpreter has their own `Identity`, they might own other accounts as well. Thus the owners control more accounts than just the contract storing the owners.

The `Call` instruction has the same security risks as calling any arbitrary smart contract, such as setting unlimited allowances.

Adding an owner to the set of owners grants them the ability to evict other owners.

Failure to execute an instruction will lead to a transaction being reverted, however, the funds will still be in the interpreter account's control. Ensure that changing ownership is always done atomically (add and remove in the same transaction) to ensure funds are not lost forever.

Using bridges is equivalent to adding them as owners on your interpreter instance.

## Security layers

In general different security can be applied to different programs.

### Anonymous programs

These programs operating only on funds inside program and with limited set of instructions can be executed without sender authentication. 

Specific case is program consist of `Transfer`, `Spawn`, `Exchange` only on assets transferred.


### Cross protocol verification

**Example**

When program needs to transfer assets in IBC use ICS20 protocol.
In order to execute remote  transaction on behalf of account, it can use ICS27.
In both packets same program can be sent as part of batch and verified on other end to be exact same when assembled for execution.
For this if one protocol compromised we still validate via second one.


### Trusted topology

Program can be executed iff these where send only from some subset of of trusted channels.

### Cross chain multisignatures

In this case program can be executed if it was send by several chains.

### Signatures

For operations of high importance EDSCA signature of program can be propagated from sending chain and verified on target chain.  

# 8. Limited instruction support

## No support for arbitrary contracts

Some chains do not support arbitrary contracts, but support limited subset of instructions. 
In this case only programs which use limited subset of instruction will be executed on target chain via virtual spawns.

**Example**

Cosmos Hub has complies with IBC ICS Atomic swap spec, but does not host contract runtime. 

In this case, programs trying to reach Cosmos Hub from other chains, will not spawn full programs on it.

But will send only swaps and handle invocation return on sender chain.  

## No support for contract postconditions

Some chains cannot abort transaction based on arbitrary check after ABI invocation. 
In this case for specific subset of instructions to specific whitelisted contracts list will be allowed.

**Example**

On Near cannot abort Swap transaction if amount less than expected limit.
In this case only trusted Swap contracts will be callable.


# 9. Appendix

## A.
```rust
fn execute(&mut self, sender: Account, tip: Account, caller: Identity, instructions: Vec<u8>) {
    assert_eq!(sender, ROUTER::ACCOUNT);
    assert!(self.owners.contains(&caller))

    // reset the IP from the last execution
    self.IP = 0;
    self.TIP = tip;

    while let Some(instr) = take_next(&mut instructions).unwrap() {
        self.IP += 1;
        self.result = self.execute(instr).unwrap();
    }
}
```

## B.

### Examples

#### Cross-chain borrowing

A concrete example of using the XCVM protocol is to transfer funds to a different chain, use them as collateral in a loan, transmit funds back to the source chain, and use them there. For this example, we'll omit querying for current account `health` and repayments.

Concretely, we want to execute the following operations:

- Transfer funds to chain XYZ.
- Call a smart contract to take out a loan.
- Reward the relayer, to incentivize execution.
- Send funds back.

Since we might not know the current interest rates, we'll use relative values for fund transfers, instead of absolute ones.

For this example, we have the source initiator be a regular user, however, a smart contract is capable of executing the same operations.

```mermaid
sequenceDiagram
    User->>Interpreter ABC: Submit Program
    Interpreter ABC->>Router ABC: Spawn Program
    Router ABC->>Gateway ABC: Submit Program
    Gateway ABC->>Gateway XYZ: Relay Program
    Gateway XYZ->>Router XYZ: Instantiate VM
    Router XYZ->>Interpreter XYZ: Execute Spawn
    Interpreter XYZ->>Lender: Call 0x1337 (Borrow USDC for DOT)
    Lender->>Interpreter XYZ: Transfer USDC
    Interpreter XYZ->>Tip: Transfer USDC fee to Relayer
    Interpreter XYZ->>Router XYZ: Spawn Program
    Router XYZ->>Gateway XYZ: Submit Program
    Gateway XYZ->>Gateway ABC: Relay Program
    Gateway ABC->>Router ABC: Instantiate VM
    Router ABC->>Interpreter ABC: Execute Spawn
    Interpreter ABC->>Tip: Transfer USDC fee to Relayer
    Interpreter ABC->>User: Transfer USDC
```

Although these operations are quite complicated to code by hand, using the XCVM protocol, we can very succinctly express them:

```
Spawn XYZ 0 [
    Call 0x1337,                                 // chain-specific encoding to make a smart contract call.
    Transfer Tip USDC Unit 50,               // 50 bucks for the fee. The relayer earns this if the inner spawn is dispatched.
    Spawn HOME 0 [
        Transfer Tip USDC Unit 50            // Another 50 bucks fee for the operation, but now reverse direction.
        Transfer USER { USDC: Ratio::ALL }       // On ABC, we transfer all USDC to the user.
    ] { USDC: ALL },                             // We send over all our USDC back to ABC.
] { DOT: UNIT 100 },                             // We send over 100 DOT from ABC to XYZ.
```