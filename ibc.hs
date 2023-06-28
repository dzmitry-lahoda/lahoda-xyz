#!/usr/bin/env nix-shell
#!nix-shell --pure -i runghc -p "haskellPackages.ghcWithPackages (pkgs: [ pkgs.turtle ])"

data ConsensusState = ConsensusState { height:: Integer } deriving (Show)

data Connection = Connection { id :: String } deriving (Show, Eq)


type Channel = String
type ChannelId = Int
type Port = String

data Prefix = Prefix { channel :: Channel, port :: String } deriving (Show, Read)

type Path = [Prefix]

data PrefixedDenom = PrefixedDenom { path :: Path, denom :: String } deriving (Show, Read)

validate :: ConsensusState -> Bool
validate (ConsensusState height) = height == 42

data ProofTree a = EmptyTree |  Node a (ProofTree a) (ProofTree a) deriving(Show, Read,Eq)
leaf a  = Node a EmptyTree EmptyTree

treeInsert x EmptyTree = leaf x
treeInsert x (Node a left right) 
  | x == a = Node a left right
  | x < a  = Node a (treeInsert x left) right  
  | x > a  = Node a left (treeInsert x right)  

instance Functor ProofTree where
  fmap f EmptyTree = EmptyTree
  fmap f (Node a left rigth) = Node (f a) (fmap f left) (fmap f rigth)

-- if less then insert left, 

-- ics001

data ClientMessage = ClientMessage {}

-- authenticateCapability
-- hash

-- Identifier, get, set, delete, getCurrentHeight, 

data Packet = Packet { src :: Prefix, dst:: Prefix  }


data ChannelOrder = 
    Ordered 
    -- An unordered channel is a channel where packets can be delivered in any order, which may differ from the order in which they were sent.
    | Unordered 
    | OrderedAllowTimeout
data Identifier = Identifier String

data ChannelState = 
  -- state has just started the opening handshake.
  Init 
  -- state has acknowledged the handshake step on the counterparty chain
  | TryOpen 
  -- state has completed the handshake and is ready to send and receive packets
  | Open 
  -- state has been closed and can no longer be used to send or receive packets
  | Closed

data ChannelEnd = ChannelEnd 
  { 
    -- the current state of the channel end
    state:: ChannelState, 
    ordering:: ChannelOrder,
    --  identifies the port on the counterparty chain which owns the other end of the channel
    counterPartyPortIdentifier :: Identifier,  
    -- identifies the channel end on the counterparty chain
    counterpartyChannelIdentifier :: Identifier,
    -- stores the list of connection identifiers, in order, along which packets sent on this channel will travel
    connectionHops :: [Identifier],
    -- an opaque channel version, which is agreed upon during the handshake. This can determine module-level configuration such as which packet encoding is used for the channel. This version is not used by the core IBC protocol
    version :: String,
    -- tracks the sequence number for the next packet to be sent
    nextSequenceSend :: Int,
    -- tracks the sequence number for the next packet to be received
    nextSequenceRecv :: Int,
    -- tracks the sequence number for the next packet to be acknowledged
    nextSequenceAck :: Int
    }


main = do
  putStrLn (show (fmap (*2) (foldr treeInsert EmptyTree [1,2,3,4]) ))
  putStrLn "Hello world from a distributable Haskell script!"



--  digraph {
--      Consensus -> ConsensusState [label = "generates";];
--      ConsensusState -> Height [label = "has";];
--      Height -> PartialOrder
--      Height -> Zero
--      ConsensusState -> CommitmentRoot [label = "has";];
--      ConsensusState -> Timestamp [label = "has";];
--      ConsensusState -> ClientState [label = "initialized from";];
--      ConsensusState -> Height [label = "has";];
--      ConsensusState -> Pos [label="may contain"]
--      Pos -> Signatures 
--      Pos -> ValidatorSetMetadata
--      Consensus -> SourceChain [label="located at"]
--      ClientState -> ConsensusState [label = "has";];
--      CommitmentRoot -> "non/-inclusion prove of value at particular path at particular Height" [label = "allows";];
--      ClientMessage -> StateUpdate [label = "can be";];
--      ClientMessage -> Misbehaviour [label = "can be";];
--      ValidityPredicate -> ClientMessage [label = "validates";];
--      ValidityPredicate -> ConsensusState [label = "updates";];
--      ValidityPredicate -> Consensus [label = "faster than";];
--      ValidityPredicates -> StateUpdate [label="accepts or rejects"]
--      "ConsensusState[Height]" -> "ConsensusState[Height+1]";
--      Counterparty -> "ConsensusState[Height]" [label = "can introspect";];
--      ClientState -> "∃ Verify(height/path/value)";
--      MisbehaviourPredicate -> Consensus [label = "checks rules of";];
--      MisbehaviourPredicate -> ClientState [label = "freeze";];
--      Misbehaviour -> Proof [label="is"]
--      MisbehaviourPredicate -> Misbehaviour [label="uses"]
--      Misbehaviour -> Equivocation [label="can be"]
--      LightClient -> ConsensusState [label="verifies"]
--      LightClient -> SubState [label="verify"]
--      ClientType -> ValidityPredicate
--      ClientType -> Misbehaviour
--      ClientType -> LightClient
--      ClientType -> InitializationLogic
--      ClientType -> Height
--      ClientType -> ConsensusState
--      ClientType -> ClientState
--      ClientState -> VerifiedRoots [label="tracks"]
--      ClientState -> PastMismihaviorus [label="tracks"]
--      ClientMessage -> Finalized [label="is"]
--      CommitmentProof -> CommitmentRoot [label ="is assosiated"]
--      CommitmentProof -> Height [label ="is assosiated"]
--      CommitmentProof -> CommitmentPath [label ="has"]
--      CommitmentPrefix
--      LightClient -> CommitmentPath [label = "Verify membership"]
--      LightClient -> CommitmentPath [label = "Verify non-membership"]
--  }
-- --  InitialiClientState(ConsensusState,Idenfitier,ClientState)
-- --  ClientState => Height
--  ClientState => Height => Timestamp
-- --  CommitmentRootA == CommitmentRootB <=> HeightA == HeightA
-- -- --  ConsenssuState => Timestamp
-- --  Path => ConsenssuState
-- --  ClientId => ConsensusState
--  ClientId => ClientState
-- -- -- -- -- -- --  ClientMessage -> Client : verifyClientMessage
--  ClientMessage ->  Client : checkForMisbehavior
--  ClientMessage ->  Client : updatState
--  ClientMessage ->  Client : updateStateOnMisbihavriour
-- --  ```