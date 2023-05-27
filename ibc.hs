#!/usr/bin/env nix-shell
#!nix-shell --pure -i runghc -p "haskellPackages.ghcWithPackages (pkgs: [ pkgs.turtle ])"

data ConsensusState = ConsensusState { height:: Integer } deriving (Show)

data Connection = Connection { id :: String } deriving (Show, Eq)

data Port = Port Integer

validate :: ConsensusState -> Bool
validate (ConsensusState height) = height == 42

-- authenticateCapability
-- hash

-- Identifier, get, set, delete, getCurrentHeight, 

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
  putStrLn "Hello world from a distributable Haskell script!"